﻿using OutlookOkan.CsvTools;
using OutlookOkan.Services;
using OutlookOkan.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OutlookOkan.ViewModels
{
    public sealed class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel()
        {
            //Add button command
            ImportWhiteList = new RelayCommand(ImportWhiteListFromCsv);
            ExportWhiteList = new RelayCommand(ExportWhiteListToCsv);

            ImportNameAndDomainsList = new RelayCommand(ImportNameAndDomainsFromCsv);
            ExportNameAndDomainsList = new RelayCommand(ExportNameAndDomainsToCsv);

            ImportAlertKeywordAndMessagesList = new RelayCommand(ImportAlertKeywordAndMessagesFromCsv);
            ExportAlertKeywordAndMessagesList = new RelayCommand(ExportAlertKeywordAndMessagesToCsv);

            ImportAlertAddressesList = new RelayCommand(ImportAlertAddressesFromCsv);
            ExportAlertAddressesList = new RelayCommand(ExportAlertAddressesToCsv);

            ImportAutoCcBccKeywordsList = new RelayCommand(ImportAutoCcBccKeywordsFromCsv);
            ExportAutoCcBccKeywordsList = new RelayCommand(ExportAutoCcBccKeywordsToCsv);

            ImportAutoCcBccRecipientsList = new RelayCommand(ImportAutoCcBccRecipientsFromCsv);
            ExportAutoCcBccRecipientsList = new RelayCommand(ExportAutoCcBccRecipientsToCsv);

            ImportAutoCcBccAttachedFilesList = new RelayCommand(ImportAutoCcBccAttachedFilesFromCsv);
            ExportAutoCcBccAttachedFilesList = new RelayCommand(ExportAutoCcBccAttachedFilesToCsv);

            ImportDeferredDeliveryMinutesList = new RelayCommand(ImportDeferredDeliveryMinutesFromCsv);
            ExportDeferredDeliveryMinutesList = new RelayCommand(ExportDeferredDeliveryMinutesToCsv);

            //言語コードと名称をロード
            var languages = new Languages();
            Languages = languages.Language;

            //Load settings from csv.
            LoadGeneralSettingData();
            LoadWhitelistData();
            LoadNameAndDomainsData();
            LoadAlertKeywordAndMessagesData();
            LoadAlertAddressesData();
            LoadAutoCcBccKeywordsData();
            LoadAutoCcBccRecipientsData();
            LoadAutoCcBccAttachedFilesData();
            LoadDeferredDeliveryMinutesData();
        }

        public async Task SaveSettings()
        {
            IEnumerable<Task> saveTasks = new[]
                {
                    SaveGeneralSettingToCsv(),
                    SaveWhiteListToCsv(),
                    SaveNameAndDomainsToCsv(),
                    SaveAlertKeywordAndMessageToCsv(),
                    SaveAutoCcBccKeywordsToCsv(),
                    SaveAlertAddressesToCsv(),
                    SaveAutoCcBccRecipientsToCsv(),
                    SaveAutoCcBccAttachedFilesToCsv(),
                    SaveDeferredDeliveryMinutesToCsv()
                };

            await Task.WhenAll(saveTasks);
        }

        #region Whitelist

        public ICommand ImportWhiteList { get; }
        public ICommand ExportWhiteList { get; }

        private void LoadWhitelistData()
        {
            var readCsv = new ReadAndWriteCsv("Whitelist.csv");
            var whitelist = readCsv.GetCsvRecords<Whitelist>(readCsv.LoadCsv<WhitelistMap>());

            foreach (var data in whitelist)
            {
                Whitelist.Add(data);
            }
        }

        private async Task SaveWhiteListToCsv()
        {
            var list = Whitelist.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("Whitelist.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<WhitelistMap>(list));
        }

        private void ImportWhiteListFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<Whitelist>(importAction.GetCsvRecords<Whitelist>(importAction.LoadCsv<WhitelistMap>(filePath)));
                foreach (var data in importData)
                {
                    Whitelist.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportWhiteListToCsv()
        {
            var list = Whitelist.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<WhitelistMap>(list, "Whitelist.csv");
        }

        private ObservableCollection<Whitelist> _whitelist = new ObservableCollection<Whitelist>();
        public ObservableCollection<Whitelist> Whitelist
        {
            get => _whitelist;
            set
            {
                _whitelist = value;
                OnPropertyChanged("Whitelist");
            }
        }

        #endregion

        #region NameAndDomains

        public ICommand ImportNameAndDomainsList { get; }
        public ICommand ExportNameAndDomainsList { get; }

        private void LoadNameAndDomainsData()
        {
            var readCsv = new ReadAndWriteCsv("NameAndDomains.csv");
            var nameAndDomains = readCsv.GetCsvRecords<NameAndDomains>(readCsv.LoadCsv<NameAndDomainsMap>());

            foreach (var data in nameAndDomains)
            {
                NameAndDomains.Add(data);
            }
        }

        private async Task SaveNameAndDomainsToCsv()
        {
            var list = NameAndDomains.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("NameAndDomains.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<NameAndDomainsMap>(list));
        }

        private void ImportNameAndDomainsFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<NameAndDomains>(importAction.GetCsvRecords<NameAndDomains>(importAction.LoadCsv<NameAndDomainsMap>(filePath)));
                foreach (var data in importData)
                {
                    NameAndDomains.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportNameAndDomainsToCsv()
        {
            var list = NameAndDomains.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<NameAndDomainsMap>(list, "NameAndDomains.csv");
        }

        private ObservableCollection<NameAndDomains> _nameAndDomains = new ObservableCollection<NameAndDomains>();
        public ObservableCollection<NameAndDomains> NameAndDomains
        {
            get => _nameAndDomains;
            set
            {
                _nameAndDomains = value;
                OnPropertyChanged("NameAndDomains");
            }
        }

        #endregion

        #region AlertKeywordAndMessages

        public ICommand ImportAlertKeywordAndMessagesList { get; }
        public ICommand ExportAlertKeywordAndMessagesList { get; }

        private void LoadAlertKeywordAndMessagesData()
        {
            var readCsv = new ReadAndWriteCsv("AlertKeywordAndMessageList.csv");
            var alertKeywordAndMessages = readCsv.GetCsvRecords<AlertKeywordAndMessage>(readCsv.LoadCsv<AlertKeywordAndMessageMap>());

            foreach (var data in alertKeywordAndMessages)
            {
                AlertKeywordAndMessages.Add(data);
            }
        }

        private async Task SaveAlertKeywordAndMessageToCsv()
        {
            var list = AlertKeywordAndMessages.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("AlertKeywordAndMessageList.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<AlertKeywordAndMessageMap>(list));
        }

        private void ImportAlertKeywordAndMessagesFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<AlertKeywordAndMessage>(importAction.GetCsvRecords<AlertKeywordAndMessage>(importAction.LoadCsv<AlertKeywordAndMessageMap>(filePath)));
                foreach (var data in importData)
                {
                    AlertKeywordAndMessages.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportAlertKeywordAndMessagesToCsv()
        {
            var list = AlertKeywordAndMessages.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<AlertKeywordAndMessageMap>(list, "AlertKeywordAndMessageList.csv");
        }

        private ObservableCollection<AlertKeywordAndMessage> _alertKeywordAndMessages = new ObservableCollection<AlertKeywordAndMessage>();
        public ObservableCollection<AlertKeywordAndMessage> AlertKeywordAndMessages
        {
            get => _alertKeywordAndMessages;
            set
            {
                _alertKeywordAndMessages = value;
                OnPropertyChanged("AlertKeywordAndMessages");
            }
        }

        #endregion

        #region AlertAddresses

        public ICommand ImportAlertAddressesList { get; }
        public ICommand ExportAlertAddressesList { get; }

        private void LoadAlertAddressesData()
        {
            var readCsv = new ReadAndWriteCsv("AlertAddressList.csv");
            var alertAddresses = readCsv.GetCsvRecords<AlertAddress>(readCsv.LoadCsv<AlertAddressMap>());

            foreach (var data in alertAddresses)
            {
                AlertAddresses.Add(data);
            }
        }

        private async Task SaveAlertAddressesToCsv()
        {
            var list = AlertAddresses.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("AlertAddressList.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<AlertAddressMap>(list));
        }

        private void ImportAlertAddressesFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<AlertAddress>(importAction.GetCsvRecords<AlertAddress>(importAction.LoadCsv<AlertAddressMap>(filePath)));
                foreach (var data in importData)
                {
                    AlertAddresses.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportAlertAddressesToCsv()
        {
            var list = AlertAddresses.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<AlertAddressMap>(list, "AlertAddressList.csv");
        }

        private ObservableCollection<AlertAddress> _alertAddresses = new ObservableCollection<AlertAddress>();
        public ObservableCollection<AlertAddress> AlertAddresses
        {
            get => _alertAddresses;
            set
            {
                _alertAddresses = value;
                OnPropertyChanged("AlertAddresses");
            }
        }

        #endregion

        #region  AutoCcBccKeywords

        public ICommand ImportAutoCcBccKeywordsList { get; }
        public ICommand ExportAutoCcBccKeywordsList { get; }

        private void LoadAutoCcBccKeywordsData()
        {
            var readCsv = new ReadAndWriteCsv("AutoCcBccKeywordList.csv");
            var autoCcBccKeywords = readCsv.GetCsvRecords<AutoCcBccKeyword>(readCsv.LoadCsv<AutoCcBccKeywordMap>());

            foreach (var data in autoCcBccKeywords)
            {
                AutoCcBccKeywords.Add(data);
            }
        }

        private async Task SaveAutoCcBccKeywordsToCsv()
        {
            var list = AutoCcBccKeywords.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("AutoCcBccKeywordList.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<AutoCcBccKeywordMap>(list));
        }

        private void ImportAutoCcBccKeywordsFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<AutoCcBccKeyword>(importAction.GetCsvRecords<AutoCcBccKeyword>(importAction.LoadCsv<AutoCcBccKeywordMap>(filePath)));
                foreach (var data in importData)
                {
                    AutoCcBccKeywords.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportAutoCcBccKeywordsToCsv()
        {
            var list = AutoCcBccKeywords.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<AutoCcBccKeywordMap>(list, "AutoCcBccKeywordList.csv");
        }

        private ObservableCollection<AutoCcBccKeyword> _autoCcBccKeywords = new ObservableCollection<AutoCcBccKeyword>();
        public ObservableCollection<AutoCcBccKeyword> AutoCcBccKeywords
        {
            get => _autoCcBccKeywords;
            set
            {
                _autoCcBccKeywords = value;
                OnPropertyChanged("AutoCcBccKeywords");
            }
        }

        #endregion

        #region AutoCcBccRecipient

        public ICommand ImportAutoCcBccRecipientsList { get; }
        public ICommand ExportAutoCcBccRecipientsList { get; }

        private void LoadAutoCcBccRecipientsData()
        {
            var readCsv = new ReadAndWriteCsv("AutoCcBccRecipientList.csv");
            var autoCcBccRecipient = readCsv.GetCsvRecords<AutoCcBccRecipient>(readCsv.LoadCsv<AutoCcBccRecipientMap>());

            foreach (var data in autoCcBccRecipient)
            {
                AutoCcBccRecipients.Add(data);
            }
        }

        private async Task SaveAutoCcBccRecipientsToCsv()
        {
            var list = AutoCcBccRecipients.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("AutoCcBccRecipientList.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<AutoCcBccRecipientMap>(list));
        }

        private void ImportAutoCcBccRecipientsFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<AutoCcBccRecipient>(importAction.GetCsvRecords<AutoCcBccRecipient>(importAction.LoadCsv<AutoCcBccRecipientMap>(filePath)));
                foreach (var data in importData)
                {
                    AutoCcBccRecipients.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportAutoCcBccRecipientsToCsv()
        {
            var list = AutoCcBccRecipients.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<AutoCcBccRecipientMap>(list, "AutoCcBccRecipientList.csv");
        }

        private ObservableCollection<AutoCcBccRecipient> _autoCcBccRecipients = new ObservableCollection<AutoCcBccRecipient>();
        public ObservableCollection<AutoCcBccRecipient> AutoCcBccRecipients
        {
            get => _autoCcBccRecipients;
            set
            {
                _autoCcBccRecipients = value;
                OnPropertyChanged("AutoCcBccRecipients");
            }
        }

        #endregion

        #region AutoCcBccAttachedFile

        public ICommand ImportAutoCcBccAttachedFilesList { get; }
        public ICommand ExportAutoCcBccAttachedFilesList { get; }

        private void LoadAutoCcBccAttachedFilesData()
        {
            var readCsv = new ReadAndWriteCsv("AutoCcBccAttachedFileList.csv");
            var autoCcBccAttachedFile = readCsv.GetCsvRecords<AutoCcBccAttachedFile>(readCsv.LoadCsv<AutoCcBccAttachedFileMap>());

            foreach (var data in autoCcBccAttachedFile)
            {
                AutoCcBccAttachedFiles.Add(data);
            }
        }

        private async Task SaveAutoCcBccAttachedFilesToCsv()
        {
            var list = AutoCcBccAttachedFiles.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("AutoCcBccAttachedFileList.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<AutoCcBccAttachedFileMap>(list));
        }

        private void ImportAutoCcBccAttachedFilesFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<AutoCcBccAttachedFile>(importAction.GetCsvRecords<AutoCcBccAttachedFile>(importAction.LoadCsv<AutoCcBccAttachedFileMap>(filePath)));
                foreach (var data in importData)
                {
                    AutoCcBccAttachedFiles.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportAutoCcBccAttachedFilesToCsv()
        {
            var list = AutoCcBccAttachedFiles.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<AutoCcBccAttachedFileMap>(list, "AutoCcBccAttachedFileList.csv");
        }

        private ObservableCollection<AutoCcBccAttachedFile> _autoCcBccAttachedFiles = new ObservableCollection<AutoCcBccAttachedFile>();
        public ObservableCollection<AutoCcBccAttachedFile> AutoCcBccAttachedFiles
        {
            get => _autoCcBccAttachedFiles;
            set
            {
                _autoCcBccAttachedFiles = value;
                OnPropertyChanged("AutoCcBccAttachedFiles");
            }
        }

        #endregion

        #region DeferredDelivery

        public ICommand ImportDeferredDeliveryMinutesList { get; }
        public ICommand ExportDeferredDeliveryMinutesList { get; }

        private void LoadDeferredDeliveryMinutesData()
        {
            var readCsv = new ReadAndWriteCsv("DeferredDeliveryMinutes.csv");
            var deferredDeliveryMinutes = readCsv.GetCsvRecords<DeferredDeliveryMinutes>(readCsv.LoadCsv<DeferredDeliveryMinutesMap>());

            foreach (var data in deferredDeliveryMinutes)
            {
                DeferredDeliveryMinutes.Add(data);
            }
        }

        private async Task SaveDeferredDeliveryMinutesToCsv()
        {
            var list = DeferredDeliveryMinutes.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("DeferredDeliveryMinutes.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<DeferredDeliveryMinutesMap>(list));
        }

        private void ImportDeferredDeliveryMinutesFromCsv()
        {
            var importAction = new CsvImportAndExport();
            var filePath = importAction.ImportCsv();

            if (filePath is null) return;

            try
            {
                var importData = new List<DeferredDeliveryMinutes>(importAction.GetCsvRecords<DeferredDeliveryMinutes>(importAction.LoadCsv<DeferredDeliveryMinutesMap>(filePath)));
                foreach (var data in importData)
                {
                    DeferredDeliveryMinutes.Add(data);
                }

                MessageBox.Show(Properties.Resources.SuccessfulImport, Properties.Resources.AppName, MessageBoxButton.OK);
            }
            catch (Exception)
            {
                MessageBox.Show(Properties.Resources.ImportFailed, Properties.Resources.AppName, MessageBoxButton.OK);
            }
        }

        private void ExportDeferredDeliveryMinutesToCsv()
        {
            var list = DeferredDeliveryMinutes.Cast<object>().ToList();
            var exportAction = new CsvImportAndExport();
            exportAction.CsvExport<DeferredDeliveryMinutesMap>(list, "DeferredDeliveryMinutes.csv");
        }

        private ObservableCollection<DeferredDeliveryMinutes> _deferredDeliveryMinutes = new ObservableCollection<DeferredDeliveryMinutes>();
        public ObservableCollection<DeferredDeliveryMinutes> DeferredDeliveryMinutes
        {
            get => _deferredDeliveryMinutes;
            set
            {
                _deferredDeliveryMinutes = value;
                OnPropertyChanged("DeferredDeliveryMinutes");
            }
        }

        #endregion

        #region GeneralSetting

        private void LoadGeneralSettingData()
        {
            var readCsv = new ReadAndWriteCsv("GeneralSetting.csv");
            //1行しかないはずだが、何かの間違いで2行以上あるとまずいので、全行ロードする。
            foreach (var data in readCsv.GetCsvRecords<GeneralSetting>(readCsv.LoadCsv<GeneralSettingMap>()))
            {
                _generalSetting.Add((data));
            }

            if (_generalSetting.Count == 0) return;

            //実際に使用するのは1行目の設定のみ
            IsDoNotConfirmationIfAllRecipientsAreSameDomain = _generalSetting[0].IsDoNotConfirmationIfAllRecipientsAreSameDomain;
            IsDoDoNotConfirmationIfAllWhite = _generalSetting[0].IsDoDoNotConfirmationIfAllWhite;
            IsAutoCheckIfAllRecipientsAreSameDomain = _generalSetting[0].IsAutoCheckIfAllRecipientsAreSameDomain;
            IsShowConfirmationToMultipleDomain = _generalSetting[0].IsShowConfirmationToMultipleDomain;
            EnableForgottenToAttachAlert = _generalSetting[0].EnableForgottenToAttachAlert;
            EnableGetContactGroupMembers = _generalSetting[0].EnableGetContactGroupMembers;
            EnableGetExchangeDistributionListMembers = _generalSetting[0].EnableGetExchangeDistributionListMembers;
            ContactGroupMembersAreWhite = _generalSetting[0].ContactGroupMembersAreWhite;
            ExchangeDistributionListMembersAreWhite = _generalSetting[0].ExchangeDistributionListMembersAreWhite;
            IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles = _generalSetting[0].IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles;

            if (_generalSetting[0].LanguageCode is null) return;

            //設定ファイル内に言語設定があればそれをロードする。
            Language.LanguageCode = _generalSetting[0].LanguageCode;
            foreach (var lang in Languages)
            {
                if (lang.LanguageCode == Language.LanguageCode)
                {
                    LanguageNumber = lang.LanguageNumber;
                }
            }
        }

        private async Task SaveGeneralSettingToCsv()
        {
            var languageCode = Language.LanguageCode ?? CultureInfo.CurrentUICulture.Name;
            if (Language.LanguageCode != null)
            {
                ResourceService.Instance.ChangeCulture(Language.LanguageCode);
            }

            var tempGeneralSetting = new List<GeneralSetting>
            {
                new GeneralSetting
                {
                    IsDoNotConfirmationIfAllRecipientsAreSameDomain = IsDoNotConfirmationIfAllRecipientsAreSameDomain,
                    IsDoDoNotConfirmationIfAllWhite = IsDoDoNotConfirmationIfAllWhite,
                    IsAutoCheckIfAllRecipientsAreSameDomain = IsAutoCheckIfAllRecipientsAreSameDomain,
                    LanguageCode = languageCode,
                    IsShowConfirmationToMultipleDomain = IsShowConfirmationToMultipleDomain,
                    EnableForgottenToAttachAlert = EnableForgottenToAttachAlert,
                    EnableGetContactGroupMembers = EnableGetContactGroupMembers,
                    EnableGetExchangeDistributionListMembers = EnableGetExchangeDistributionListMembers,
                    ContactGroupMembersAreWhite = ContactGroupMembersAreWhite,
                    ExchangeDistributionListMembersAreWhite = ExchangeDistributionListMembersAreWhite,
                    IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles = IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles
                }
            };

            var list = tempGeneralSetting.Cast<object>().ToList();
            var writeCsv = new ReadAndWriteCsv("GeneralSetting.csv");
            await Task.Run(() => writeCsv.WriteRecordsToCsv<GeneralSettingMap>(list));
        }

        private readonly List<GeneralSetting> _generalSetting = new List<GeneralSetting>();

        private bool _isDoNotConfirmationIfAllRecipientsAreSameDomain;
        public bool IsDoNotConfirmationIfAllRecipientsAreSameDomain
        {
            get => _isDoNotConfirmationIfAllRecipientsAreSameDomain;
            set
            {
                _isDoNotConfirmationIfAllRecipientsAreSameDomain = value;
                OnPropertyChanged("IsDoNotConfirmationIfAllRecipientsAreSameDomain");
            }
        }

        private bool _isDoDoNotConfirmationIfAllWhite;
        public bool IsDoDoNotConfirmationIfAllWhite
        {
            get => _isDoDoNotConfirmationIfAllWhite;
            set
            {
                _isDoDoNotConfirmationIfAllWhite = value;
                OnPropertyChanged("IsDoDoNotConfirmationIfAllWhite");
            }
        }

        private bool _isAutoCheckIfAllRecipientsAreSameDomain;
        public bool IsAutoCheckIfAllRecipientsAreSameDomain
        {
            get => _isAutoCheckIfAllRecipientsAreSameDomain;
            set
            {
                _isAutoCheckIfAllRecipientsAreSameDomain = value;
                OnPropertyChanged("IsAutoCheckIfAllRecipientsAreSameDomain");
            }
        }

        private bool _isShowConfirmationToMultipleDomain;
        public bool IsShowConfirmationToMultipleDomain
        {
            get => _isShowConfirmationToMultipleDomain;
            set
            {
                _isShowConfirmationToMultipleDomain = value;
                OnPropertyChanged("IsShowConfirmationToMultipleDomain");
            }
        }

        private bool _enableForgottenToAttachAlert = true;
        public bool EnableForgottenToAttachAlert
        {
            get => _enableForgottenToAttachAlert;
            set
            {
                _enableForgottenToAttachAlert = value;
                OnPropertyChanged("EnableForgottenToAttachAlert");
            }
        }

        private bool _enableGetContactGroupMembers;
        public bool EnableGetContactGroupMembers
        {
            get => _enableGetContactGroupMembers;
            set
            {
                _enableGetContactGroupMembers = value;
                OnPropertyChanged("EnableGetContactGroupMembers");
            }
        }

        private bool _enableGetExchangeDistributionListMembers;
        public bool EnableGetExchangeDistributionListMembers
        {
            get => _enableGetExchangeDistributionListMembers;
            set
            {
                _enableGetExchangeDistributionListMembers = value;
                OnPropertyChanged("EnableGetExchangeDistributionListMembers");
            }
        }

        private bool _contactGroupMembersAreWhite = true;
        public bool ContactGroupMembersAreWhite
        {
            get => _contactGroupMembersAreWhite;
            set
            {
                _contactGroupMembersAreWhite = value;
                OnPropertyChanged("ContactGroupMembersAreWhite");
            }
        }

        private bool _exchangeDistributionListMembersAreWhite = true;
        public bool ExchangeDistributionListMembersAreWhite
        {
            get => _exchangeDistributionListMembersAreWhite;
            set
            {
                _exchangeDistributionListMembersAreWhite = value;
                OnPropertyChanged("ExchangeDistributionListMembersAreWhite");
            }
        }

        private bool _isNotTreatedAsAttachmentsAtHtmlEmbeddedFiles;

        public bool IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles
        {
            get => _isNotTreatedAsAttachmentsAtHtmlEmbeddedFiles;
            set
            {
                _isNotTreatedAsAttachmentsAtHtmlEmbeddedFiles = value;
                OnPropertyChanged("IsNotTreatedAsAttachmentsAtHtmlEmbeddedFiles");
            }
        }

        private LanguageCodeAndName _language = new LanguageCodeAndName();
        public LanguageCodeAndName Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged("Language");
            }
        }

        private List<LanguageCodeAndName> _languages;
        public List<LanguageCodeAndName> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                OnPropertyChanged("Languages");
            }
        }

        private int _languageNumber = -1;
        public int LanguageNumber
        {
            get => _languageNumber;
            set
            {
                _languageNumber = value;
                OnPropertyChanged("LanguageNumber");
            }
        }

        #endregion
    }
}