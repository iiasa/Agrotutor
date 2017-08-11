namespace CimmytApp.Views
{
    using Xamarin.Forms;

    public partial class ParcelPage : TabbedPage
    {
        public ParcelPage()
        {
            InitializeComponent();
            var pages = Children.GetEnumerator();
            pages.MoveNext(); // First page
            pages.MoveNext(); // Second page
            CurrentPage = pages.Current;
            pages.Dispose();
        }
    }
}