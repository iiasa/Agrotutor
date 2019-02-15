namespace Agrotutor.Core.Camera
{
    public class AssetsHelper
    {
        public const string AssetsNamespace = "Agrotutor.Core.Camera";

        public static string GetImageNamespace(string fileName)
        {
            return string.Format("{0}.Images.{1}", AssetsNamespace, fileName);
        }
    }
}
