namespace CimmytApp.DTO.Parcel.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class AddParcelPageViewModel : BindableBase, INavigationAware
    {
        private List<string> agriculturalCycles = new List<string> { "Spring-Summer", "Autumn-Winter" };
        public List<string> AgriculturalCycles => agriculturalCycles;

        public bool Test { get; set; }

        public ICommand AgriculturalCycleChangedCommand;
        public ICommand Moo;

        public Parcel Parcel { get; set; }

        public AddParcelPageViewModel()
        {
            AgriculturalCycleChangedCommand = new DelegateCommand(AgriculturalCycleChanged);
        }

        private void AgriculturalCycleChanged()
        {
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}