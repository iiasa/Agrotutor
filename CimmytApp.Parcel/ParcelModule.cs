namespace CimmytApp.Parcel
{
    using CimmytApp.Parcel.Views;
    using Microsoft.Practices.Unity;
    using Prism.Modularity;
    using Prism.Unity;

    /// <inheritdoc />
    /// <summary>
    ///     Defines the <see cref="T:CimmytApp.Parcel.ParcelModule" />
    /// </summary>
    public class ParcelModule : IModule
    {
        /// <summary>
        ///     Defines the _unityContainer
        /// </summary>
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParcelModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The <see cref="IUnityContainer" /></param>
        public ParcelModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        ///     The Initialize
        /// </summary>
        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<ActivityPage>();
            _unityContainer.RegisterTypeForNavigation<ViewActivitiesPage>();
            _unityContainer.RegisterTypeForNavigation<ActivityDetail>();
            _unityContainer.RegisterTypeForNavigation<AddParcelPage>();
            _unityContainer.RegisterTypeForNavigation<DeleteParcelPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelsOverviewPage>();
            _unityContainer.RegisterTypeForNavigation<ViewParcelInformationPage>();
            _unityContainer.RegisterTypeForNavigation<ParcelPage>();
            _unityContainer.RegisterTypeForNavigation<SelectTechnologiesPage>();
        }
    }
}