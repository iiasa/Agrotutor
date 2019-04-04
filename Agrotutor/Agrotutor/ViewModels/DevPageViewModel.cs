using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agrotutor.ViewModels
{
    public class DevPageViewModel : BindableBase
    {
        public DevPageViewModel()
        {

        }

        public DelegateCommand Weather = new DelegateCommand(()=> 
        {
             
        });
    }
}
