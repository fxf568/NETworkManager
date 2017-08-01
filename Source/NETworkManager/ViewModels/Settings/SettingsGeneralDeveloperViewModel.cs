﻿using NETworkManager.Models.Settings;

namespace NETworkManager.ViewModels.Settings
{
    public class SettingsGeneralDeveloperViewModel : ViewModelBase
    {
        #region Variables
        private bool _isLoading = true;

        private bool _developerMode;
        public bool DeveloperMode
        {
            get { return _developerMode; }
            set
            {
                if (value == _developerMode)
                    return;

                if (!_isLoading)
                    SettingsManager.Current.DeveloperMode = value;

                _developerMode = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor, LoadSettings
        public SettingsGeneralDeveloperViewModel()
        {
            LoadSettings();

            _isLoading = false;
        }

        private void LoadSettings()
        {
            DeveloperMode = SettingsManager.Current.DeveloperMode;
        }
        #endregion
    }
}