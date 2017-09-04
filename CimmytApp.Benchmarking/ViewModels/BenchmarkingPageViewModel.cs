using System;
using System.Windows.Input;
using Prism.Commands;

namespace CimmytApp.Benchmarking.ViewModels
{
    public class BenchmarkingPageViewModel
    {
        public ICommand LoadDataCommand { get; set;}

        public BenchmarkingPageViewModel()
        {
            LoadDataCommand = new DelegateCommand(LoadData);
        }

        private void LoadData(){
            
        }
    }
}
