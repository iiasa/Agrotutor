namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Mvvm;

    using DTO.BEM;

    public class LocalBenchmarkingPageViewModel : BindableBase
    {
        private BemData bemData;

        public LocalBenchmarkingPageViewModel()
        {
            bemData = new BemData();
        }
    }
}