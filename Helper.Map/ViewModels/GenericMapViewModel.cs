using Prism.Mvvm;

namespace Helper.Map.ViewModels
{
    public class GenericMapViewModel : BindableBase
    {
        public string Title { get; set; }

        public GenericMapViewModel()
        {
            Title = "Map";
        }
    }
}