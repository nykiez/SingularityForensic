﻿using SingularityForensic.App.ViewModels;
using SingularityForensic.App.Views;
using System;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.App.MessageBoxes {
    public static class CDFCMessageBox {
        public static MessageBoxResult Show(string msgText) {
            return Show(msgText, FindResourceString("Tip"), MessageBoxButton.OK);
        }
        public static MessageBoxResult Show(string msgText, MessageBoxButton button) {
            return Show(msgText, FindResourceString("Tip"), button);
        }

        public static MessageBoxResult Show(string msgText, string caption, MessageBoxButton button) {
            var vm = new CDFCMessageBoxViewModel(button, msgText, caption);
            var msg = new CDFCMessageBoxWindow(vm);
            msg.Owner = Application.Current.MainWindow;
            msg.ShowInTaskbar = false;
            var res = msg.ShowDialog();
            switch (vm.DialogResult) {
                case null:
                    if (button == MessageBoxButton.YesNoCancel)
                        return MessageBoxResult.Cancel;
                    return MessageBoxResult.None;
                case false:
                    return MessageBoxResult.No;
                case true:
                    if (button == MessageBoxButton.OK)
                        return MessageBoxResult.OK;
                    return MessageBoxResult.Yes;
                default:
                    return MessageBoxResult.None;
            }
        }

        public static void Show(object findresourcestring, string v) {
            throw new NotImplementedException();
        }
    }
}
