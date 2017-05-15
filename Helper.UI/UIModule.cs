using Prism.Modularity;
using Prism.Regions;
using System;

namespace Helper.UI
{
    public class UIModule : IModule
    {
        IRegionManager _regionManager;

        public UIModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}