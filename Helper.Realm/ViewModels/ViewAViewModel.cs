namespace Helper.Realm.ViewModels
{
    using Prism.Mvvm;

    public class ViewAViewModel : BindableBase
    {
        private string _title;

        public ViewAViewModel()
        {
            Title = "View A";
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}