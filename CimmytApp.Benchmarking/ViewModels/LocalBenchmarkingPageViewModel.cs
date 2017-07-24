namespace CimmytApp.Benchmarking.ViewModels
{
    using Prism.Mvvm;

    using DTO.BemData;

    public class LocalBenchmarkingPageViewModel : BindableBase
    {
        BemData bemData;

        public LocalBenchmarkingPageViewModel()
        {
            bemData = new BemData();
        }
    }
}