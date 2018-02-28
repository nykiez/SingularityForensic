﻿using System.Windows.Controls;
using System.Windows.Data;
using WpfHexaEditor;

namespace SingularityForensic.Hex.Views {
    /// <summary>
    /// Interaction logic for InterceptHex.xaml
    /// </summary>
    public partial class InterceptHex : UserControl {
        public InterceptHex() {
            InitializeComponent();
            HexEditor.SetBinding(DrawedHexEditor.CustomBackgroundBlocksProperty, new Binding(nameof(HexEditor.CustomBackgroundBlocks)));
        }
    }
}