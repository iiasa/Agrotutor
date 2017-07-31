﻿namespace CimmytApp.Parcel
{
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;
    using System;

    using Views;

    public class ParcelModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ParcelModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<AddParcelInformationPage>();
            _unityContainer.RegisterTypeForNavigation<AddParcelPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelsOverviewPage>();
            _unityContainer.RegisterTypeForNavigation<ViewParcelInformationPage>();
        }
    }
}