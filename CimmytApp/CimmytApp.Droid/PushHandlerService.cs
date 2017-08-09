namespace CimmytApp.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WindowsAzure.Messaging;
    using Android.App;
    using Android.Content;
    using Android.Util;
    using Gcm.Client;

    [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationId { get; private set; }
        private NotificationHub Hub { get; set; }
        public static Context Context;

        public PushHandlerService() : base(Constants.SenderId)
        {
            Log.Info(AzurePushBroadcastReceiver.Tag, "PushHandlerService() constructor");
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(AzurePushBroadcastReceiver.Tag, "GCM Registered: " + registrationId);
            RegistrationId = registrationId;
            // TODO store registrationID
            /*CreateNotification("PushHandlerService-GCM Registered...",
                                "The device has been Registered!");*/

            Hub = new NotificationHub(Constants.NotificationHubName, Constants.ListenConnectionString,
                                        context);

            //TODO : don't unregister - load registrationID here if available
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(AzurePushBroadcastReceiver.Tag, ex.Message);
            }

            //var tags = new List<string>() { "falcons" }; // create tags if you want
            var tags = new List<string>() { };

            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(AzurePushBroadcastReceiver.Tag, ex.Message);
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if (intent?.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));

                var messageText = intent.Extras.GetString("message");
                if (!string.IsNullOrEmpty(messageText))
                {
                    CreateNotification("New hub message!", messageText);
                    // TODO save data in DB for app to use
                }
                else
                {
                    //CreateNotification("Unknown message details", msg.ToString());
                }
            }
        }

        private void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;

            //Create an intent to show UI
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Create the notification
            var notification =
                new Notification(Android.Resource.Drawable.SymActionEmail, title)
                {
                    Flags = NotificationFlags.AutoCancel
                };

            //Auto-cancel will remove the notification once the user touches it

            //Set the notification info
            //we use the pending intent, passing our ui intent over, which will get called
            //when the notification is tapped.
            notification.SetLatestEventInfo(this, title, desc, PendingIntent.GetActivity(this, 0, uiIntent, PendingIntentFlags.OneShot));

            //Show the notification
            notificationManager.Notify(0, notification);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(AzurePushBroadcastReceiver.Tag, "GCM Unregistered: " + registrationId);

            //CreateNotification("GCM Unregistered...", "The device has been unregistered!");
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(AzurePushBroadcastReceiver.Tag, "Recoverable Error: " + errorId);

            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(AzurePushBroadcastReceiver.Tag, "GCM Error: " + errorId);
        }
    }
}