﻿using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Contracts.Hex.Events;
using SingularityForensic.Contracts.StatusBar;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;

namespace SingularityForensic.Hex.Events {
    /// <summary>
    /// 十六进制选定位置发生变更时通知状态栏当前位置,以及对应的字符;
    /// </summary>
    [Export(typeof(IHexDataContextLoadedEventHandler))]
    public class OnHexFocusPositionChangedOnStatusBarHandler : IHexDataContextLoadedEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle(IHexDataContext hexContext) {
            //订阅位置变更事件;
            hexContext.FocusPositionChanged += (sender,e) => HandleOnFocusChanged(hexContext);
            //失去焦点时,移除状态栏项;
            //hexContext.LostFocus += (sender, e) => HandleOnLostFocus(hexContext);
        }

        private static void HandleOnLostFocus(IHexDataContext hexContext) {
            var positionItem = StatusBarService.Current.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_Position, _positionChildLength, 4);
            //RemoveItem(positionItem);

            var curCharValueItem = StatusBarService.Current.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_CurCharValue, _curCharValueChildLength, 8);
            //RemoveItem(curCharValueItem);
        }

        private static void RemoveItem(IStatusBarObjectItem item) {
            if (item != null && StatusBarService.Current.Children.Contains(item)) {
                StatusBarService.Current.RemoveStatusBarItem(item);
            }
        }

        private static void HandleOnFocusChanged(IHexDataContext hexContext) {
            if(hexContext == null) {
                return;
            }

            NotifyPosition(hexContext);
            ShowChar(hexContext);
        }

        private static readonly GridChildLength _positionChildLength = new GridChildLength(GridLength.Auto);
        
        /// <summary>
        /// 通知位置;
        /// </summary>
        /// <param name="hexContext"></param>
        private static void NotifyPosition(IHexDataContext hexContext) {
            var positionItem = StatusBarService.Current.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_Position,_positionChildLength,4);

            if (positionItem == null) {
                return;
            }

            try {
                StatusBarService.Report(
                $"{LanguageService.FindResourceString(Constants.StatusBarItemText_Position)} {hexContext.FocusPosition}",
                Constants.StatusBarItemGUID_Position
                );
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            
        }

        private static readonly GridChildLength _curCharValueChildLength = new GridChildLength(GridLength.Auto);
        /// <summary>
        /// 通知当前的字符值;
        /// </summary>
        /// <param name="hexContext"></param>
        private static void ShowChar(IHexDataContext hexContext) {
            var curCharValueItem = StatusBarService.Current.GetOrCreateStatusBarTextItem(Constants.StatusBarItemGUID_CurCharValue, _curCharValueChildLength, 8);
            if (curCharValueItem == null) {
                return;
            }
    
            try {
                var stream = hexContext.Stream;
                if(stream == null) {
                    LoggerService.WriteCallerLine($"{nameof(stream)} can't be null.");
                    return;
                }

                stream.Position = hexContext.FocusPosition;
                StatusBarService.Report(
                    $"{LanguageService.FindResourceString(Constants.StatusBarItemText_CurCharValue)} {stream.ReadByte()}",
                    Constants.StatusBarItemGUID_CurCharValue);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
