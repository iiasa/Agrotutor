namespace CimmytApp.Core.Parcel.ViewModels
{
    using CimmytApp.Core.Persistence.Entities;
 
    public class PlotViewModel
    {
        public bool IsOptionsVisible { get; set; }
        public Plot Plot { get; set; }
    }
}