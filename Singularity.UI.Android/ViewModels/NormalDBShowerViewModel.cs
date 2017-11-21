using EventLogger;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using static CDFCCultures.Managers.ManagerLocator;
using static CDFCCultures.Helpers.ByteConverterHelper;
using CDFCMessageBoxes.MessageBoxes;

namespace Singularity.UI.Info.Android.ViewModels {
    public class NormalDBShowerViewModel:BindableBase {
        public string ConnString => $"Data Source={DbPath};Version=3;New=true";
        public SQLiteConnection Conn { get; }
        public NormalDBShowerViewModel(IEnumerable browseTableSource) {
            this.BrowserTableSource = browseTableSource;
            
        }

        private string tableName;

        public string DbPath { get; }

        public IEnumerable BrowserTableSource {
            get;
        }

        private DataTable GetDataTable(string sqlCommand) {
            if (Conn != null) {
                try {
                    var ad = new SQLiteDataAdapter(sqlCommand, Conn);
                    try {
                        var ds = new DataSet();
                        ad.Fill(ds);
                        var tbOri = ds.Tables[0];
                        var tbNew = tbOri.Clone();
                        //以两个list记录列数据类型是/否byte[]的序列号;
                        var bsIndex = new List<int>();
                        var nbsIndex = new List<int>();

                        for (int i = 0; i < tbNew.Columns.Count; i++) {
                            if (tbNew.Columns[i].DataType == typeof(byte[])) {
                                tbNew.Columns[i].DataType = typeof(string);
                                bsIndex.Add(i);
                            }
                            else {
                                nbsIndex.Add(i);
                            }
                        }

                        for (int ri = 0; ri < tbOri.Rows.Count; ri++) {
                            var newRow = tbNew.NewRow();
                            bsIndex.ForEach(p => {
                                var bs = tbOri.Rows[ri][p] as byte[];
                                if (bs != null) {
                                    if (bs.Length < 24) {
                                        newRow[p] = bs.ConvertToHexFormat();
                                    }
                                    else {
                                        newRow[p] = $"{FindResourceString("BinaryData")}({FindResourceString("BinaryLength")}:{bs.Length})";
                                    }
                                }
                            });
                            nbsIndex.ForEach(p => {
                                newRow[p] = tbOri.Rows[ri][p];
                            });

                            tbNew.Rows.Add(newRow);
                        }
                        return tbNew;
                    }
                    catch {
                        throw;
                    }
                    finally {
                        ad.Dispose();
                    }
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(NormalDBShowerViewModel)}");
                    CDFCMessageBox.Show($"{ex.Message}");
                }
            }

            return null;
        }
    }
}
