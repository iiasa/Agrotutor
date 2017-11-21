namespace CimmytApp.AgronomicalRecommendations.ViewModels
{
    using CimmytApp.DTO.Parcel;
    using Prism.Mvvm;

    public class LocalAgronomicalRecommendationsPageViewModel : BindableBase
    {
        public Parcel Parcel { get; set; }
    }
}