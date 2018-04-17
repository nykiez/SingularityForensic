using SingularityForensic.Previewers.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Prism.Commands;
using Prism.Mvvm;
using System.Data.SQLite;
using System.Data;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.TreeView;
using CDFCCultures.Helpers;

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
                    LoggerService.WriteCallerLine($"{nameof(SqlitePreviewerModel)}->{nameof(SqlitePreviewerModel)}:{ex.Message}");
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
            if (!File.Exists(DbPath)) {
                return;
            }

            var tablesUnit = TreeUnitFactory.CreateNew(Constants.DBUnitType_Content);
            tablesUnit.Label = LanguageService.FindResourceString(Constants.UnitLabel_SqliteTable);

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
                        var tableUnit = TreeUnitFactory.CreateNew(Constants.DBUnitType_Table);
                        tableUnit.Label = name;
                        tablesUnit.Children.Add(tableUnit);
                        tableCount++;
                    }
                    catch {

                    }
                }
                reader.Close();
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine($"{nameof(SqlitePreviewerModel)}->{nameof(LoadUnits)}:{ex.Message}");
            }
            finally {
                tablesUnit.Label += $"({tableCount})";
                DBUnits.Add(tablesUnit);
            }
                
            
        }
        public string DbPath { get; }
        public ObservableCollection<ITreeUnit> DBUnits { get; set; } = new ObservableCollection<ITreeUnit>();
        private DataTable _browserTableSource;
        public DataTable BrowseTableSource {
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
                    LoggerService.WriteCallerLine($"{nameof(SqlitePreviewerModel)}->{nameof(NotifySelectedUnitChanged)}:{ex.Message}");
                    MsgBoxService.Current?.Show($"{ex.Message}");
                }
            }

            return null;
        }
        public void NotifySelectedUnitChanged(ITreeUnit unit) {
            if(unit.TypeGuid == Constants.DBUnitType_Table) {
                var dt = GetDataTable($"Select* from { unit.Label}");

                BrowseTableSource?.Dispose();
                BrowseTableSource = dt;
                
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
        private DataTable _executedSource;
        public DataTable ExecutedSource {
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
                        ExecutedSource?.Dispose();
                        ExecutedSource = dt;
                    }
                    else {
                        MsgBoxService.Show("IncorrectSql");
                    }
                }
            ));

    }
}
