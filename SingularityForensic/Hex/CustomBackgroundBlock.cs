﻿using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SingularityForensic.Hex {
    public class CustomBackgroundBlock : ICustomBackgroundBlock {
        public long StartOffset { get ; set ; }
        public long Length { get ; set ; }
        public Brush Background { get ; set; }
    }
}