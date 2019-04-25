using System;
using System.Threading.Tasks;
using Agrotutor.Core.Persistence;
using Microsoft.Extensions.Localization;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Services;

namespace Agrotutor.Core.Camera
{
    public class CameraService : ICameraService
    {
        private readonly IMedia media;

        private readonly IPageDialogService pageDialogService;

        private readonly IStringLocalizer<CameraService> stringLocalizer;

        private bool TakePictureActive;

        public CameraService(IPageDialogService pageDialogService, IStringLocalizer<CameraService> stringLocalizer)
        {
            media = CrossMedia.Current;
            media.Initialize();
            this.pageDialogService = pageDialogService;
            TakePictureActive = false;
        }


        public async Task<string> TakePicture()
        {
            if (TakePictureActive) return string.Empty;
            TakePictureActive = true;
            if (!media.IsCameraAvailable || !media.IsTakePhotoSupported)
            {
                await pageDialogService.DisplayAlertAsync(
                    stringLocalizer.GetString("no_camera_title"), 
                    stringLocalizer.GetString("no_camera_message"),
                    stringLocalizer.GetString("no_camera_ok"));
                return string.Empty;
            }

            var fileName = $"{Guid.NewGuid().ToString()}.jpg";
            var file = await media.TakePhotoAsync(
                new StoreCameraMediaOptions { SaveToAlbum = true, Name = fileName, AllowCropping = false, CompressionQuality = 20 });
            TakePictureActive = false;
            return file == null ? string.Empty : file.Path;
        }


        public async Task<string> PickPicture()
        {
            if (!media.IsPickPhotoSupported)
            {
                await pageDialogService.DisplayAlertAsync(
                    stringLocalizer.GetString("pick_photo_not_supported_title"), 
                    stringLocalizer.GetString("pick_photo_not_supported_message"), 
                    stringLocalizer.GetString("pick_photo_not_supported_ok"));
                return string.Empty;
            }

            var file = await media.PickPhotoAsync();

            return file == null ? string.Empty : file.Path;
        }

        public async Task<string> TakeVideo()
        {
            if (!media.IsCameraAvailable || !media.IsTakeVideoSupported)
            {
                await pageDialogService.DisplayAlertAsync(
                    stringLocalizer.GetString("no_video_camera_title"),
                    stringLocalizer.GetString("no_video_camera_message"),
                    stringLocalizer.GetString("no_video_camera_ok"));
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
