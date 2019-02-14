using System.Threading.Tasks;

namespace Agrotutor.Core.Camera
{
    public interface ICameraService
    {
        Task<string> TakePicture();

        Task<string> PickPicture();

        Task<string> TakeVideo();
    }
}
