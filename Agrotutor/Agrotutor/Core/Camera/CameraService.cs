using System;
using System.Threading.Tasks;
using Agrotutor.Core.Persistence;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;

namespace Agrotutor.Core.Camera
{
    public class CameraService : ICameraService
    {
        private readonly IMedia media;

        private readonly IPageDialogService pageDialogService;

        public CameraService(IPageDialogService pageDialogService)
        {
            media = CrossMedia.Current;
            media.Initialize();
            this.pageDialogService = pageDialogService;
        }


        public async Task<string> TakePicture()
        {
            if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
            {
                await pageDialogService.DisplayAlertAsync("No Camera", "No camera available", "ok");
                return string.Empty;
            }

            var fileName = $"{Guid.NewGuid().ToString()}.jpg";
            var file = await media.TakePhotoAsync(
                new StoreCameraMediaOptions { SaveToAlbum = true, Name = fileName, AllowCropping = false, CompressionQuality = 20 });

            if (file == null) return string.Empty;

            return file.Path;
        }


        public async Task<string> PickPicture()
        {
            if (!media.IsPickPhotoSupported)
            {
                await pageDialogService.DisplayAlertAsync("No Upload", "Picking photo not supported", "ok");
                return string.Empty;
            }

            var file = await media.PickPhotoAsync();

            if (file == null) return string.Empty;

            return file.Path;
        }

        public async Task<string> TakeVideo()
        {
            if (!media.IsCameraAvailable || !media.IsTakeVideoSupported)
            {
                await pageDialogService.DisplayAlertAsync("No Camera", ":( No camera available.", "OK");
                return string.Empty;
            }

            var fileName = $"{Guid.NewGuid().ToString()}.mp4";
            var file = await media.TakeVideoAsync(
                new StoreVideoOptions { Directory = "DefaultVideos", Name = fileName, CompressionQuality = 30 });

            if (file == null) return string.Empty;
            var filePath = file.Path;

            file.Dispose();

            return filePath;
        }
    }
}
