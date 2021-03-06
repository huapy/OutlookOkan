﻿using OutlookOkan.Types;
using OutlookOkan.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace OutlookOkan.Views
{
    public partial class ConfirmationWindow : Window
    {
        private readonly Outlook._MailItem _mailItem;

        public ConfirmationWindow(CheckList checkList, Outlook._MailItem mailItem)
        {
            DataContext = new ConfirmationWindowViewModel(checkList);
            _mailItem = mailItem;

            InitializeComponent();

            //送信遅延時間を表示(設定)欄に入れる。
            DeferredDeliveryMinutesBox.Text = checkList.DeferredMinutes.ToString();

        }

        /// <summary>
        /// DialogResultをBindしずらいので、コードビハインドで。
        /// </summary>
        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            //送信時刻の設定
            int.TryParse(DeferredDeliveryMinutesBox.Text, out var deferredDeliveryMinutes);
            _mailItem.DeferredDeliveryTime = deferredDeliveryMinutes == 0 ? _mailItem.CreationTime : DateTime.Now.AddMinutes(deferredDeliveryMinutes);

            DialogResult = true;
        }

        /// <summary>
        /// DialogResultをBindしずらいので、コードビハインドで。
        /// </summary>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// チェックボックスのイベント処理がやりづらかったので、コードビハインド側からViewModelのメソッドを呼び出す。
        /// </summary>
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ConfirmationWindowViewModel;
            viewModel?.ToggleSendButton();
        }

        /// <summary>
        /// チェックボックスのイベント処理がやりづらかったので、コードビハインド側からViewModelのメソッドを呼び出す。
        /// </summary>
        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ConfirmationWindowViewModel;
            viewModel?.ToggleSendButton();
        }

        /// <summary>
        /// 送信遅延時間の入力ボックスを数値のみ入力に制限する。
        /// </summary>
        private void DeferredDeliveryMinutesBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+$");

            if (!regex.IsMatch(DeferredDeliveryMinutesBox.Text + e.Text)) return;
            DeferredDeliveryMinutesBox.Text = "0";
            e.Handled = true;
        }

        /// <summary>
        /// 送信遅延時間の入力ボックスへのペーストを無視する。(全角数字がペーストされる恐れがあるため)
        /// </summary>
        private void DeferredDeliveryMinutesBox_OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}