using CimmytApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageAndroid))]

namespace CimmytApp.Droid
{
    using Android.App;
    using Android.Widget;
    using CimmytApp.Messaging;

    /// <summary>
    ///     Defines the <see cref="MessageAndroid" />
    /// </summary>
    public class MessageAndroid : IMessage
    {
        /// <summary>
        ///     The LongAlert
        /// </summary>
        /// <param name="message">The <see cref="string" /></param>
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        /// <summary>
        ///     The ShortAlert
        /// </summary>
        /// <param name="message">The <see cref="string" /></param>
        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}