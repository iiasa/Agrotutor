namespace CimmytApp.Core.Parcel.ViewModels
{
    using System.Collections.Generic;
    using CimmytApp.DTO.Parcel;
    using CimmytApp.Parcel;
    using CimmytApp.ViewModels;
    using Microsoft.Extensions.Localization;
    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Navigation;

    public class SelectTechnologiesPageViewModel : ViewModelBase, INavigatedAware
    {
        private const string Technology1 =
                "Cambio a variedades mejoradas, nuevas y adaptadas a las zonas con potencial para incrementar el rendimiento "
            ;

        private const string Technology2 = "Interpretación y uso del análisis de suelo";

        private const string Technology3 = "Uso del sensor infrarrojo para fertilización óptima";

        private const string Technology4 = "Uso de biofertilizantes";

        private const string Technology5 = "Mejoradores de suelo para complementar fertilización";

        private const string Technology6 = "Mínimo movimiento de suelo, retención de residuos y rotación de cultivos";

        private const string Technology7 =
            "Introducción de nuevos cultivos en la rotación (ejemplo: cultivos de forraje)";

        private const string Technology8 = "Tecnología para mejorar el almacenamiento del grano";

        private readonly INavigationService _navigationService;

        private bool _selectEnabled;

        private bool _tech1Checked;

        private bool _tech2Checked;

        private bool _tech3Checked;

        private bool _tech4Checked;

        private bool _tech5Checked;

        private bool _tech6Checked;

        private bool _tech7Checked;

        private bool _tech8Checked;

        private List<Technology> _technologies;

        public SelectTechnologiesPageViewModel(INavigationService navigationService,
            IStringLocalizer<SelectTechnologiesPageViewModel> localizer) : base(localizer)
        {
            SaveChangesCommand = new DelegateCommand(Save);
            _navigationService = navigationService;
        }

        public bool Initializing { get; set; } = true;

        public DelegateCommand SaveChangesCommand { get; set; }

        public bool SelectEnabled
        {
            get => _selectEnabled;
            set => SetProperty(ref _selectEnabled, value);
        }

        public bool Tech1Checked
        {
            get => _tech1Checked;
            set
            {
                SetProperty(ref _tech1Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech2Checked
        {
            get => _tech2Checked;
            set
            {
                SetProperty(ref _tech2Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech3Checked
        {
            get => _tech3Checked;
            set
            {
                SetProperty(ref _tech3Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech4Checked
        {
            get => _tech4Checked;
            set
            {
                SetProperty(ref _tech4Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech5Checked
        {
            get => _tech5Checked;
            set
            {
                SetProperty(ref _tech5Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech6Checked
        {
            get => _tech6Checked;
            set
            {
                SetProperty(ref _tech6Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech7Checked
        {
            get => _tech7Checked;
            set
            {
                SetProperty(ref _tech7Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public bool Tech8Checked
        {
            get => _tech8Checked;
            set
            {
                SetProperty(ref _tech8Checked, value);
                if (!Initializing)
                {
                    UpdateTechChecked();
                }
            }
        }

        public List<Technology> Technologies
        {
            get => _technologies;
            set
            {
                SetProperty(ref _technologies, value);
                if (Initializing)
                {
                    UpdateTechCheckedUI();
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParcelConstants.TechnologiesParameterName))
            {
                Technologies = (List<Technology>)parameters[ParcelConstants.TechnologiesParameterName];
            }
            if (parameters.ContainsKey("ViewOnly"))
            {
                parameters.TryGetValue<bool>("ViewOnly", out var viewOnly);
                SelectEnabled = !viewOnly;
            }
            else
            {
                SelectEnabled = true;
            }
            Initializing = false;
        }

        private void Save()
        {
            var navigationParameters = new NavigationParameters
            {
                { ParcelConstants.TechnologiesParameterName, Technologies }
            };
            _navigationService.GoBackAsync(navigationParameters);
        }

        private void UpdateTechChecked()
        {
            var technologies = new List<Technology>();
            if (_tech1Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology1
                });
            }
            if (_tech2Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology2
                });
            }
            if (_tech3Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology3
                });
            }
            if (_tech4Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology4
                });
            }
            if (_tech5Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology5
                });
            }
            if (_tech6Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology6
                });
            }
            if (_tech7Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology7
                });
            }
            if (_tech8Checked)
            {
                technologies.Add(new Technology
                {
                    Name = SelectTechnologiesPageViewModel.Technology8
                });
            }
            Technologies = technologies;
        }

        private void UpdateTechCheckedUI()
        {
            if (Technologies == null)
            {
                return;
            }

            foreach (var technology in Technologies)
            {
                switch (technology.Name)
                {
                    case SelectTechnologiesPageViewModel.Technology1:
                        Tech1Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology2:
                        Tech2Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology3:
                        Tech3Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology4:
                        Tech4Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology5:
                        Tech5Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology6:
                        Tech6Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology7:
                        Tech7Checked = true;
                        break;

                    case SelectTechnologiesPageViewModel.Technology8:
                        Tech8Checked = true;
                        break;
                }
            }
        }
    }
}