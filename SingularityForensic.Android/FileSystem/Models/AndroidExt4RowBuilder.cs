using SingularityForensic.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDFC.Parse.Contracts;
using CDFC.Util;

namespace SingularityForensic.Android.FileSystem.Models {
    public class AndroidExt4RowBuilder : GenericStaticInstance<AndroidExt4RowBuilder>,IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            if(file != null) {
                return new AndroidFileRow(file);
            }
            return null;
        }
    }
}
