using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CGHClientServer1.WPF.Infrastructure;
using CGHClientServer1.WPF.Model;
using CGHClientServer1.WPF.Service;
using Microsoft.Extensions.Logging;

namespace CGHClientServer1.WPF.ViewModel
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
        private ICommand _refreshCommand;
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

        public ICommand RefreshCommand
        {
            get
            {
                _refreshCommand ??= new RelayCommand(param => { RefreshDropdowns(); }, param => true);

                return _refreshCommand;
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
                var items = AsyncHelper.RunSync<IList<City>>(() => CitySelectorService.
                    GetCities(SelectedStateId.Value));
                CityList = items.OrderBy(x => x.Name).ToList();
            }
        }

        private void GetCountriesList()
        {
            CountryList?.Clear();
            var items = AsyncHelper.RunSync<IList<Country>>(() => CitySelectorService.
                GetCountries());
            CountryList = items.OrderBy(x => x.Name).ToList();
        }

        private void GetStatesList()
        {
            StateList?.Clear();

            if (SelectedCountryId.HasValue)
            {
                var items = AsyncHelper.RunSync<IList<State>>(() => CitySelectorService.
                    GetStates(SelectedCountryId.Value));
                StateList = items.OrderBy(x => x.Name).ToList();
            }
        }

        private void ProcessSelectionsAndExit()
        {
            string selectedIds = $"{SelectedCountryId}:{SelectedStateId}:{SelectedCityId}";
            Console.Out.WriteLine(selectedIds);
            Clipboard.SetText(selectedIds);
            Environment.Exit(1);
        }

        private void RefreshDropdowns()
        {
            GetCountriesList();
            GetStatesList();
            GetCitiesList();
        }

        #endregion Private Methods
    }
}