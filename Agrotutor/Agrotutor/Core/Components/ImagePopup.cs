using System;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Agrotutor.Core.Components
{
    public class ImagePopup : PopupPage, IDisposable
    {
        private string _imageFile;
        private bool _disposed;

        public event EventHandler ImageClosed;

        public ImagePopup()
        {

        }


        protected virtual void OnImageClosed(EventArgs e)
        {
            ImageClosed?.Invoke(this, e);
        }

        public bool IsRunning { get; private set; }

        public void Dispose()
        {
            Dispose(true);
        }

        private void SetContent()
        {
            var imageSource = ImageSource.FromFile(_imageFile);
            var closeImage = new CachedImage
            {
                DownsampleToViewSize = true,
                CacheType = CacheType.All,
                Source = "close_cross.png"
            };

            closeImage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(Close)
            });

            if (Uri.IsWellFormedUriString(_imageFile, UriKind.Absolute))
            {
                imageSource = ImageSource.FromUri(new Uri(_imageFile));
            }

            var cachedImage = new CachedImage
            {
                Source = imageSource,
                CacheType = CacheType.All,
            };

            var layout = new AbsoluteLayout
            {
                Margin = 20,
            };
            AbsoluteLayout.SetLayoutBounds(cachedImage, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(cachedImage, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(closeImage, new Rectangle(1, 0, .1, .05));
            AbsoluteLayout.SetLayoutFlags(closeImage, AbsoluteLayoutFlags.All);
            layout.Children.Add(cachedImage);
            layout.Children.Add(closeImage);
            Content = layout;
        }

        public void Show(string imageFile)
        {
            _imageFile = imageFile;
            SetContent();
            IsRunning = true;
            PopupNavigation.Instance.PushAsync(this);
        }

        public void Close()
        {
            PopupNavigation.Instance.PopAsync();
            IsRunning = false;
            OnImageClosed(EventArgs.Empty);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) Close();

                _disposed = true;
            }
        }
    }
}