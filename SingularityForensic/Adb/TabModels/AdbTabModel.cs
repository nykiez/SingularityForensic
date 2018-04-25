namespace SingularityForensic.Adb.TabModels {
    //public class AdbTabModel : IDocumentTab {
    //    public AdbTabModel(AdbTabViewModel vm) {
    //        _adbTab = new AdbTab { DataContext = vm };
    //        this.AdbTabViewModel = vm;

    //        Title = $"{AdbTabViewModel.Device?.Disply}-{MInfoTypeHelper.GetInfoTypeWord(AdbTabViewModel.Container.InfoType)}"; 
    //    }

    //    public AdbTabViewModel AdbTabViewModel { get; }

    //    public string Title { get; set; }

    //    public IList<CommandItem> CustomCommands => null;

    //    private AdbTab _adbTab;
    //    public object UIObject => _adbTab ;

    //    public object Tag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    //    public void Dispose() {

    //    }
    //}

    //public class AdbBasicTabModel : IDocumentTab {
    //    public AdbBasicTabModel(Basic basic) {
    //        var vm = new InfoBasicViewModel();
    //        try {
    //            var sb = new StringBuilder();
    //            foreach (var prop in basic.GetType().GetProperties()) {
    //                sb.AppendLine(LanguageService.FindResourceString($"AdbBasic{prop.Name}") +
    //                    $":{ prop.GetValue(basic)}");
    //            }
    //            vm.BasicText = sb.ToString();
    //        }
    //        catch (Exception ex) {
    //            Logger.WriteLine($"{nameof(AdbBasicTabModel)}:{ex.Message}");
    //            vm.BasicText = ex.Message;
    //        }
    //        _view = new InfoBasicView() {
    //            DataContext = vm
    //        };
    //        this.Basic = basic;
    //    }

    //    public Basic Basic { get; }

    //    public string Title { get; set; }

    //    public List<CommandItem> CustomCommands => null;

    //    private InfoBasicView _view;
    //    public object UIObject => _view;

    //    IList<CommandItem> IDocumentTab.CustomCommands => throw new NotImplementedException();

    //    public object Tag { get ; set ; }

    //    public void Dispose() {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class AdbMainTabModel : IDocumentTab {
    //    public AdbMainTabModel(InfoMainViewModel vm) {
    //        UIObject = new InfoMain {
    //            DataContext = vm
    //        };
    //    }

    //    public string Title => string.Empty;

    //    public IList<CommandItem> CustomCommands => null;

    //    public object UIObject { get; }

    //    public void Dispose() {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class AdbGridTabModel : IDocumentTab {
    //    public AdbGridTabModel(AdbGridViewModel vm) {
    //        UIObject = new AdbGrid {
    //            DataContext = vm
    //        };
    //    }

    //    public string Title => string.Empty;

    //    public List<CommandItem> CustomCommands => null;

    //    public object UIObject { get; }

    //    IList<CommandItem> IDocumentTab.CustomCommands => throw new NotImplementedException();

    //    public void Dispose() {
    //        throw new NotImplementedException();
    //    }
    //}
}
