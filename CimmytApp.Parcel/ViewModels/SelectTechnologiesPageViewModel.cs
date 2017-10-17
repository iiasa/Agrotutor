namespace CimmytApp.Parcel.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SelectTechnologiesPageViewModel" />
    /// </summary>
    public class SelectTechnologiesPageViewModel : BindableBase, INavigationAware
    {
        /// <summary>
        /// Defines the Technology1
        /// </summary>
        private const string Technology1 = "Cambio a variedades mejoradas, nuevas y adaptadas a las zonas con potencial para incrementar el rendimiento ";

        /// <summary>
        /// Defines the Technology2
        /// </summary>
        private const string Technology2 = "Interpretación y uso del análisis de suelo";

        /// <summary>
        /// Defines the Technology3
        /// </summary>
        private const string Technology3 = "Uso del sensor infrarrojo para fertilización óptima";

        /// <summary>
        /// Defines the Technology4
        /// </summary>
        private const string Technology4 = "Uso de biofertilizantes";

        /// <summary>
        /// Defines the Technology5
        /// </summary>
        private const string Technology5 = "Mejoradores de suelo para complementar fertilización";

        /// <summary>
        /// Defines the Technology6
        /// </summary>
        private const string Technology6 = "Mínimo movimiento de suelo, retención de residuos y rotación de cultivos";

        /// <summary>
        /// Defines the Technology7
        /// </summary>
        private const string Technology7 = "Introducción de nuevos cultivos en la rotación (ejemplo: cultivos de forraje)";

        /// <summary>
        /// Defines the Technology8
        /// </summary>
        private const string Technology8 = "Tecnología para mejorar el almacenamiento del grano";

        /// <summary>
        /// Defines the _tech1Checked
        /// </summary>
        private bool _tech1Checked;

        /// <summary>
        /// Defines the _tech2Checked
        /// </summary>
        private bool _tech2Checked;

        /// <summary>
        /// Defines the _tech3Checked
        /// </summary>
        private bool _tech3Checked;

        /// <summary>
        /// Defines the _tech4Checked
        /// </summary>
        private bool _tech4Checked;

        /// <summary>
        /// Defines the _tech5Checked
        /// </summary>
        private bool _tech5Checked;

        /// <summary>
        /// Defines the _tech6Checked
        /// </summary>
        private bool _tech6Checked;

        /// <summary>
        /// Defines the _tech7Checked
        /// </summary>
        private bool _tech7Checked;

        /// <summary>
        /// Defines the _tech8Checked
        /// </summary>
        private bool _tech8Checked;

        /// <summary>
        /// Gets or sets a value indicating whether Tech1Checked
        /// </summary>
        public bool Tech1Checked
        {
            get => _tech1Checked;
            set
            {
                _tech1Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech2Checked
        /// </summary>
        public bool Tech2Checked
        {
            get => _tech2Checked;
            set
            {
                _tech2Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech3Checked
        /// </summary>
        public bool Tech3Checked
        {
            get => _tech3Checked;
            set
            {
                _tech3Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech4Checked
        /// </summary>
        public bool Tech4Checked
        {
            get => _tech4Checked;
            set
            {
                _tech4Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech5Checked
        /// </summary>
        public bool Tech5Checked
        {
            get => _tech5Checked;
            set
            {
                _tech5Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech6Checked
        /// </summary>
        public bool Tech6Checked
        {
            get => _tech6Checked;
            set
            {
                _tech6Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech7Checked
        /// </summary>
        public bool Tech7Checked
        {
            get => _tech7Checked;
            set
            {
                _tech7Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Tech8Checked
        /// </summary>
        public bool Tech8Checked
        {
            get => _tech8Checked;
            set
            {
                _tech8Checked = value;
                UpdateTechChecked();
            }
        }

        /// <summary>
        /// The UpdateTechChecked
        /// </summary>
        private void UpdateTechChecked()
        {
            var technologies = new List<string>();
            if (_tech1Checked) technologies.Add(Technology1);
            if (_tech2Checked) technologies.Add(Technology2);
            if (_tech3Checked) technologies.Add(Technology3);
            if (_tech4Checked) technologies.Add(Technology4);
            if (_tech5Checked) technologies.Add(Technology5);
            if (_tech6Checked) technologies.Add(Technology6);
            if (_tech7Checked) technologies.Add(Technology7);
            if (_tech8Checked) technologies.Add(Technology8);
            Technologies = technologies;
        }

        /// <summary>
        /// The UpdateTechCheckedUI
        /// </summary>
        private void UpdateTechCheckedUI()
        {
            if (Technologies == null) return;
            foreach (var technology in Technologies)
            {
                switch (technology)
                {
                    case Technology1:
                        Tech1Checked = true;
                        break;

                    case Technology2:
                        Tech2Checked = true;
                        break;

                    case Technology3:
                        Tech3Checked = true;
                        break;

                    case Technology4:
                        Tech4Checked = true;
                        break;

                    case Technology5:
                        Tech5Checked = true;
                        break;

                    case Technology6:
                        Tech6Checked = true;
                        break;

                    case Technology7:
                        Tech7Checked = true;
                        break;

                    case Technology8:
                        Tech8Checked = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Technologies
        /// </summary>
        public List<string> Technologies
        {
            get => _technologies;
            set
            {
                UpdateTechCheckedUI();
                SetProperty(ref _technologies, value);
            }
        }

        /// <summary>
        /// Defines the _navigationService
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Defines the _technologies
        /// </summary>
        private List<string> _technologies;

        /// <summary>
        /// Gets or sets the SaveChangesCommand
        /// </summary>
        public DelegateCommand SaveChangesCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectTechnologiesPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The <see cref="INavigationService"/></param>
        public SelectTechnologiesPageViewModel(INavigationService navigationService)
        {
            SaveChangesCommand = new DelegateCommand(Save);
            _navigationService = navigationService;
        }

        /// <summary>
        /// The Save
        /// </summary>
        private void Save()
        {
            var navigationParameters = new NavigationParameters { { ParcelConstants.TechnologiesParameterName, Technologies } };
            _navigationService.GoBackAsync(navigationParameters);
        }

        /// <summary>
        /// The OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// The OnNavigatedTo
        /// </summary>
        /// <param name="parameters">The <see cref="NavigationParameters"/></param>
        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParcelConstants.TechnologiesParameterName))
            {
                Technologies = (List<string>)parameters[ParcelConstants.TechnologiesParameterName];
            }
        }
    }
}