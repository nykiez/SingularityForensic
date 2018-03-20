using SingularityForensic.Previewers.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using EventLogger;
using CDFCMessageBoxes.MessageBoxes;
using CDFCCultures.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System.Data.SQLite;
using System.Data;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Previewers.ViewModels {
    public partial class SqlitePreviewerModel:BindableBase {
        public string ConnString => $"Data Source={DbPath};Version=3;New=true";
        public SQLiteConnection Conn { get; }
        public SqlitePreviewerModel(string dbPath) {
            DbPath = dbPath;
            if (File.Exists(DbPath)) {
                try {
                    Conn = new SQLiteConnection(ConnString);
                }
                catch(Exception ex) {
                    Logger.WriteLine($"{nameof(SqlitePreviewerModel)}->{nameof(SqlitePreviewerModel)}:{ex.Message}");
                }
            }
            LoadUnits();
        }
        private void CheckOpen() {
            if(Conn != null) {
                if(Conn.State == ConnectionState.Closed) {
                    Conn.Open();
                }
            }
        }
        private void LoadUnits() {
            if (File.Exists(DbPath)) {
                var tablesUnit = new DBUnit(DBUnitType.Content) { Title = "Tables" };
                var tableCount = 0;
                try {
                    CheckOpen();
                    var comm = Conn.CreateCommand();
                    comm.CommandText = "select * from sqlite_master where type = 'table'";
                    comm.CommandType = CommandType.Text;
                    
                    var reader = comm.ExecuteReader();
                    while (reader.Read()) {
                        try {
                            var name = reader["name"].ToString();
                            var tableUnit = new DBUnit(DBUnitType.Table) { Title = name };
                            tablesUnit.Children.Add(tableUnit);
                            tableCount++;
                        }
                        catch {

                        }
                    }
                    reader.Close();
                }
                catch(Exception ex) {
                    Logger.WriteLine($"{nameof(SqlitePreviewerModel)}->{nameof(LoadUnits)}:{ex.Message}");
                }
                finally {
                    tablesUnit.Title += $"({tableCount})";
                    DBUnits.Add(tablesUnit);
                }
                
            }
        }
        public string DbPath { get; }
        public ObservableCollection<DBUnit> DBUnits { get; set; } = new ObservableCollection<DBUnit>();
        private IEnumerable _browserTableSource;
        public IEnumerable BrowseTableSource {
            get {
                return _browserTableSource;
            }
            set {
                SetProperty(ref _browserTableSource, value);
            }
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
                                        newRow[p] = $"{LanguageService.FindResourceString("BinaryData")}({LanguageService.FindResourceString("BinaryLength")}:{bs.Length})";
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
                    Logger.WriteLine($"{nameof(SqlitePreviewerModel)}->{nameof(NotifySelectedUnitChanged)}:{ex.Message}");
                    CDFCMessageBox.Show($"{ex.Message}");
                }
            }

            return null;
        }
        public void NotifySelectedUnitChanged(DBUnit unit) {
            if(unit.UnitType == DBUnitType.Table) {
                var dt = GetDataTable($"Select* from { unit.Title}");
                if(dt != null) {
                    BrowseTableSource = dt.DefaultView;
                }
            }
        }

        public void Close() {
            if(Conn?.State == ConnectionState.Open) {
                Conn.Close();
            }
            Conn?.Dispose();
        }

        //执行的SQL命令语句;
        private string _sqlCommand;
        public string SqlCommand {
            get {
                return _sqlCommand;
            }
            set {
                SetProperty(ref _sqlCommand, value);
            }
        }

        //执行后的结果;
        private IEnumerable _executedSource;
        public IEnumerable ExecutedSource {
            get {
                return _executedSource;
            }
            set {
                SetProperty(ref _executedSource, value);
            }
        }
    }

    public partial class SqlitePreviewerModel {
        //执行语句命令;
        private DelegateCommand _executeSqlCommand;
        public DelegateCommand ExecuteSqlCommand =>
            _executeSqlCommand ?? (_executeSqlCommand = new DelegateCommand(
                () => {
                    if (SqlCommand?.ToUpper().StartsWith("SELECT") == true) {
                        var dt = GetDataTable(SqlCommand);
                        if(dt != null) {
                            ExecutedSource = dt.DefaultView;
                        }
                    }
                    else {
                        CDFCMessageBox.Show("IncorrectSql");
                    }
                }
            ));

    }
}
