using System;
using Agrotutor.Core.Components;
using Android.Content;
using Xamarin.Forms;

namespace Agrotutor.Droid
{
    public class DocumentViewer_Droid : IDocumentViewer
    {
        public void ShowDocumentFile(string filepath, string mimeType)
        {
            var uri = Android.Net.Uri.Parse("file://" + filepath);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, mimeType);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                Forms.Context.StartActivity(Intent.CreateChooser(intent, "Select App"));
            }
            catch (Exception ex)
            {
                //Let the user know when something went wrong
            }
        }
    }
}