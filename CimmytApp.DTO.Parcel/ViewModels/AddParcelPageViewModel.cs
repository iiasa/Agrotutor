namespace CimmytApp.DTO.Parcel.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using Prism.Commands;
    using Prism.Mvvm;

    public class AddParcelPageViewModel : BindableBase
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
    }
}