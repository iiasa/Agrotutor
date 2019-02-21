using System;
using System.IO;
using Agrotutor.Core.Components;
using Foundation;
using QuickLook;
using UIKit;

namespace Agrotutor.iOS
{
    public class DocumentViewer_iOS : IDocumentViewer
    {
        public void ShowDocumentFile(string filepath, string mimeType)
        {
            var fileinfo = new FileInfo(filepath);
            var previewController = new QLPreviewController();
            previewController.DataSource = new PreviewControllerDataSource(fileinfo.FullName, fileinfo.Name);

            var controller = FindNavigationController();

            if (controller != null) controller.PresentViewController(previewController, true, null);
        }

        private UINavigationController FindNavigationController()
        {
            foreach (var window in UIApplication.SharedApplication.Windows)
                if (window.RootViewController.NavigationController != null)
                {
                    return window.RootViewController.NavigationController;
                }
                else
                {
                    var value = CheckSubs(window.RootViewController.ChildViewControllers);
                    if (value != null) return value;
                }

            return null;
        }

        private UINavigationController CheckSubs(UIViewController[] controllers)
        {
            foreach (var controller in controllers)
            {
                if (controller.NavigationController != null) return controller.NavigationController;

                var value = CheckSubs(controller.ChildViewControllers);
                if (value != null) return value;

                return null;
            }

            return null;
        }
    }

    public class DocumentItem : QLPreviewItem
    {
        private readonly string _uri;

        public DocumentItem(string title, string uri)
        {
            ItemTitle = title;
            _uri = uri;
        }

        public override string ItemTitle { get; }

        public override NSUrl ItemUrl => NSUrl.FromFilename(_uri);
    }

    public class PreviewControllerDataSource : QLPreviewControllerDataSource
    {
        private readonly string _filename;
        private readonly string _url;

        public PreviewControllerDataSource(string url, string filename)
        {
            _url = url;
            _filename = filename;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return new DocumentItem(_filename, _url);
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return 1;
        }
    }
}