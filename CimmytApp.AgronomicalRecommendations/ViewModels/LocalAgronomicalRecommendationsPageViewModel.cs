namespace CimmytApp.AgronomicalRecommendations.ViewModels
{
    using Prism.Mvvm;

    using DTO.Parcel;

    public class LocalAgronomicalRecommendationsPageViewModel : BindableBase
    {
        public Parcel Parcel { get; set; }

        public LocalAgronomicalRecommendationsPageViewModel()
        {
        }
    }
}