using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using $safeprojectname$.Infrastructure;
using $safeprojectname$.Model;
using $safeprojectname$.Service;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ILogger<MainWindowViewModel> _log;

        #region Constructors

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger,
            ICitySelectorService citySelectorService) : base()
        {
            _log = logger;
            CitySelectorService = citySelectorService;
            GetCountriesList();
        }

        #endregion Constructors

        private ICitySelectorService _citySelectorService = null;

        public string CitySelectorServiceType
        {
            get
            {
                string retVal = string.Empty;
                if (CitySelectorService != null)
                {
                    retVal = CitySelectorService.GetType().Name;
                }

                return retVal;
            }
        }

        public string WindowTitle
        {
            get
            {
                return $"City Selector v{GetAssemblyVersion()} ({CitySelectorServiceType.Replace("CitySelector", string.Empty).Replace("Service", string.Empty)})";
            }
        }

        private ICitySelectorService CitySelectorService
        {
            get
            {
                return _citySelectorService;
            }
            set
            {
                if (value != null)
                {
                    _log.LogInformation($"{nameof(CitySelectorService)} set to instance of {value.GetType()}");
                }

                _citySelectorService = value;
                OnPropertyChanged(nameof(CitySelectorServiceType));
            }
        }

        #region Private Fields

        private IList<City> _cityList;
        private IList<Country> _countryList;
        private ICommand _okCommand;
        private Guid? _selectedCityId;
        private Guid? _selectedCountryId;
        private Guid? _selectedStateId;
        private IList<State> _stateList;

        #endregion Private Fields

        #region Public Commands

        public ICommand OkCommand
        {
            get
            {
                _okCommand ??= new RelayCommand(param => { ProcessSelectionsAndExit(); }, OKCommandCanExecute);

                return _okCommand;
            }
        }

        private bool OKCommandCanExecute(object parameter)
        {
            return SelectedCityId.HasValue;
        }

        #endregion Public Commands

        #region Public Properties

        public bool AllowCitySelection
        {
            get { return (SelectedStateId != null && SelectedStateId != Guid.Empty); }
        }

        public bool AllowStateSelection
        {
            get { return (SelectedCountryId != null && SelectedCountryId != Guid.Empty); }
        }

        public IList<City> CityList
        {
            get { return _cityList; }
            set
            {
                _cityList = value;
                OnPropertyChanged();
            }
        }

        public IList<Country> CountryList
        {
            get { return _countryList; }
            set
            {
                _countryList = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelectCityOpen => SelectedStateId.HasValue && !SelectedCityId.HasValue;
        public bool IsSelectCountryOpen => !SelectedCountryId.HasValue;
        public bool IsSelectStateOpen => SelectedCountryId.HasValue && !SelectedStateId.HasValue;

        public Guid? SelectedCityId
        {
            get { return _selectedCityId; }
            set
            {
                _selectedCityId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectCityOpen));

                OnPropertyChanged(nameof(AllowCitySelection));
                OnPropertyChanged(nameof(AllowStateSelection));
            }
        }

        public Guid? SelectedCountryId
        {
            get { return _selectedCountryId; }
            set
            {
                _selectedCountryId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectCountryOpen));

                SelectedStateId = null;
                GetStatesList();
                OnPropertyChanged(nameof(IsSelectStateOpen));
            }
        }

        public Guid? SelectedStateId
        {
            get { return _selectedStateId; }
            set
            {
                _selectedStateId = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectStateOpen));

                SelectedCityId = null;
                GetCitiesList();
                OnPropertyChanged(nameof(IsSelectCityOpen));
            }
        }

        public IList<State> StateList
        {
            get { return _stateList; }
            set
            {
                _stateList = value;
                OnPropertyChanged();
            }
        }

        #endregion Public Properties

        #region Private Methods

        private void GetCitiesList()
        {
            CityList?.Clear();

            if (SelectedStateId.HasValue)
            {
                CityList = AsyncHelper.RunSync<IList<City>>(() => CitySelectorService.
                    GetCities(SelectedStateId.Value));
            }
        }

        private void GetCountriesList()
        {
            CountryList?.Clear();

            CountryList = AsyncHelper.RunSync<IList<Country>>(() => CitySelectorService.
                GetCountries());
        }

        private void GetStatesList()
        {
            StateList?.Clear();

            if (SelectedCountryId.HasValue)
            {
                StateList = AsyncHelper.RunSync<IList<State>>(() => CitySelectorService.
                    GetStates(SelectedCountryId.Value));
            }
        }

        private void ProcessSelectionsAndExit()
        {
            string selectedIds = $"{SelectedCountryId}:{SelectedStateId}:{SelectedCityId}";
            Console.Out.WriteLine(selectedIds);
            Clipboard.SetText(selectedIds);
            Environment.Exit(1);
        }

        #endregion Private Methods
    }
}