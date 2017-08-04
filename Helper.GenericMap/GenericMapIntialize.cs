using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.GenericMap.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace Helper.GenericMap
{
  public class GenericMapIntialize:IModule
    {
        private readonly IUnityContainer _unityContainer;

        public GenericMapIntialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }
        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<GlobalMap>();
        }
    }
}
