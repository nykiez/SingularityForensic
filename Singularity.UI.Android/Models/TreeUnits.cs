using CDFCUIContracts.Models;
using Singularity.UI.Info.Models;
using SingularityForensic.Modules.MainPage.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Singularity.UI.Android.Models {
    //单独种类容器集合(未分拣)
    public class MultiDbModelsUnit<TDbModel>:ExtTreeUnit<IEnumerable<TDbModel>>
        where TDbModel:ForensicInfoDbModel {
        /// <summary>
        /// 单独种类容器集合(未分拣)构造方法;
        /// </summary>
        /// <param name="dbModels">模型集合</param>
        /// <param name="pinKind">节点类型</param>
        /// <param name="parent"></param>
        /// <param name="idFunc">标识分类器</param>
        /// <param name="childKind"></param>
        public MultiDbModelsUnit(IEnumerable<TDbModel> dbModels,string pinKind,TreeUnit parent, 
            Func<TDbModel,(string id,string name)> idFunc = null) :
            base(dbModels,parent,pinKind){
            this.idFunc = idFunc;
        }
        
        //标识委托;
        protected Func<TDbModel, (string id, string name)> idFunc;

        private ObservableCollection<ITreeUnit> _children;
        public override ObservableCollection<ITreeUnit> Children {
            get {
                if(_children == null && idFunc != null) {
                    var childrenUnits = new ObservableCollection<ITreeUnit>();
                    //短信将按照对方号码进行分拣;
                    var groups = Data.GroupBy(p => idFunc(p).id);
                    foreach (var group in groups) {
                        var relName = idFunc(group.First()).name;
                        var singleUnit = new MultiDbModelsUnit<TDbModel>(group.ToList(), $"{ContentId}{PinSpliter}id={group.Key}", this);
                        if (relName != null) {
                            singleUnit.Label = $"{relName}-{group.Key}({group.Count()})";
                        }
                        else {
                            singleUnit.Label = $"{group.Key??"未知"}({group.Count()})";
                        }
                        childrenUnits.Add(singleUnit);
                    }
                    _children = new ObservableCollection<ITreeUnit>(childrenUnits.OrderBy(p => p.Label));
                }
                return _children;
            }
            set => _children = value;
        }
    }
    
}
