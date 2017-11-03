using CimmytApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIOS))]

namespace CimmytApp.iOS
{
    using Foundation;
    using UIKit;

    using Messaging;

    /// <summary>
    /// Defines the <see cref="MessageIOS" />
    /// </summary>
    public class MessageIOS : IMessage
    {
        /// <summary>
        /// Defines the LongDelay
        /// </summary>
        private const double LongDelay = 3.5;

        /// <summary>
        /// Defines the ShortDelay
        /// </summary>
        private const double ShortDelay = 2.0;

        /// <summary>
        /// Defines the _alertDelay
        /// </summary>
        private NSTimer _alertDelay;

        /// <summary>
        /// Defines the _alert
        /// </summary>
        private UIAlertController _alert;

        /// <summary>
        /// The LongAlert
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        public void LongAlert(string message)
        {
            ShowAlert(message, LongDelay);
        }

        /// <summary>
        /// The ShortAlert
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        public void ShortAlert(string message)
        {
            ShowAlert(message, ShortDelay);
        }

        /// <summary>
        /// The ShowAlert
        /// </summary>
        /// <param name="message">The <see cref="string"/></param>
        /// <param name="seconds">The <see cref="double"/></param>
        private void ShowAlert(string message, double seconds)
        {
            _alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissMessage();
            });
            _alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(_alert, true, null);
        }

        /// <summary>
        /// The DismissMessage
        /// </summary>
        private void DismissMessage()
        {
            _alert?.DismissViewController(true, null);
            _alertDelay?.Dispose();
        }
    }
}