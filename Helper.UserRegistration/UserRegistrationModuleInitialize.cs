﻿using Helper.UserRegistration.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;

namespace Helper.UserRegistration
{
    public class UserRegistrationModuleInitialize : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public UserRegistrationModuleInitialize(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<RegistrationPage>();
        }
    }
}