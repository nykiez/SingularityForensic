using System.Collections.ObjectModel;

namespace SingularityForensic.Previewers.Models {
    public class DBUnit {
        public string Title { get; set; }
        public DBUnit(DBUnitType unitType) {
            UnitType = unitType;
        }
        public DBUnitType UnitType { get; }
        public ObservableCollection<DBUnit> Children { get; set; } = new ObservableCollection<DBUnit>();
    }
    public enum DBUnitType {
        Content,            //Tables,Master_Table
        Table               //Table
    }
}
