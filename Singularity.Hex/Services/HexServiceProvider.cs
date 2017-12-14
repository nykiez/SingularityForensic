﻿using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using Singularity.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDFCCultures.Managers.ManagerLocator;
using WPFHexaEditor.Core.Bytes;
using CDFC.Parse.IO;

namespace Singularity.UI.Hex.Services {
    [Export(typeof(IHexServiceProvider))]
    public class HexServiceProvider : IHexServiceProvider {
        /// <summary>
        /// 查找匹配的字符串;
        /// </summary>
        /// <param name="findString"></param>
        public void FindNextString(IHexDataContext hex, string findString) {
            FindNextBytes(hex, ByteConverters.StringToByte(findString), FindMethod.Text);
        }

        public void FindNextString(IHexDataContext hex, string findString, bool isBlockSearch, int blockSize, int blockOffset) =>
            FindNextBytes(hex, ByteConverters.StringToByte(findString), FindMethod.Text, isBlockSearch, blockSize, blockOffset);

        public void FindNextBytes(IHexDataContext hex, byte[] findBytes) =>
            FindNextBytes(hex, findBytes, FindMethod.Hex, false, -1, -1);

        public void FindNextBytes(IHexDataContext hex, byte[] findBytes, FindMethod method) =>
            FindNextBytes(hex, findBytes, method, false, -1, -1);
        
        public void FindNextBytes(IHexDataContext hex,byte[] findBytes,
            FindMethod findMethod, bool isBlockSearch, int blockSize, int blockOffset) {
            if (findBytes == null || findBytes.Length == 0) {
                Logger.WriteCallerLine($"{nameof(findBytes)} can't be null or empty.");
                return;
            }

            if (isBlockSearch && (blockSize <= 0 || blockOffset < 0)) {
                throw new ArgumentException($"Invalid Argument(s):{nameof(blockSize)} or {nameof(blockOffset)}");
            }

            long pos = (hex.FocusPosition == -1 ? 0 : hex.FocusPosition) + 1;
            var dialog = new ProgressMessageBox {
                WindowTitle = findMethod == FindMethod.Text ? FindResourceString("SearchingForText") : 
                FindResourceString("SearchingForHex")
            };

            dialog.DoWork += (sender, e) => {
                if (isBlockSearch) {
                    pos = hex.Stream.SearchBlock(pos, blockSize, blockOffset, findBytes, index => {
                        dialog.ReportProgress((int)(index * 100 / hex.Stream.Length));
                    }, () => dialog.CancellationPending);
                }
                else {
                    pos = hex.Stream.Search(pos, findBytes, index => {
                        dialog.ReportProgress((int)(index * 100 / hex.Stream.Length));
                    }, () => dialog.CancellationPending);
                }
            };

            dialog.RunWorkerCompleted += (sender, e) => {
                if (!e.Cancelled) {
                    if (pos != -1) {
                        //SelectionStart = pos;
                        //SelectionStop = pos + findBytes.Length - 1;
                        hex.Position = pos;
                        hex.FocusPosition = pos;
                    }
                    else {
                        CDFCMessageBox.Show(FindResourceString("CannotFindTheContent"));
                    }
                }
            };

            dialog.ShowDialog();
        }
    }
}
