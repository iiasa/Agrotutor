using Xamarin.Forms.Xaml;

namespace CimmytApp.Views
{
    using Xamarin.Forms;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            try
            {

                InitializeComponent();
            
        
            var pages = Children.GetEnumerator();
            pages.MoveNext(); // First page
            pages.MoveNext(); // Second page
            CurrentPage = pages.Current;
            pages.Dispose();
            }
            catch (Exception e)
            {
               
            }
        }
    }
}