using System.IO;
using Agrotutor.Core.DependencyServices;
using Agrotutor.iOS;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(HtmlBaseUrl))]
namespace Agrotutor.iOS
{
    public class HtmlBaseUrl : IHtmlBaseUrl
    {
        public string Get()
        {
            return Path.Combine(NSBundle.MainBundle.BundlePath, "html");
        }
    }
}