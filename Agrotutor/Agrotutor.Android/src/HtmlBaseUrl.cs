using Agrotutor.Core.DependencyServices;
using Agrotutor.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(HtmlBaseUrl))]
namespace Agrotutor.Droid
{
    public class HtmlBaseUrl : IHtmlBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/html/";
        }
    }
}