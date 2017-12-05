﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.Android.Models {
    //树形存储节点;
    public class FSTreeUnit {
        public byte UnitLevel { get; set; }
        public string Label { get; set; }
        public string InfoWord { get; set; }

        public ObservableCollection<FSTreeUnit> Children { get; set; } = new ObservableCollection<FSTreeUnit>();

        public int? DirectoryLevel { get; set; }
    }

    //树形节点类型;
    public enum FSTreeUnitType {
        Unknown,                //未知类型;
        Info,                   //信息类型;
        Device,                 //设备/镜像类型;
        Partition,              //分区类型;
        Directory               //目录类型;
    }
}