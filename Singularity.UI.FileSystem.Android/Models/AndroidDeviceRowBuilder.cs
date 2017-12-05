using Singularity.Contracts.FileExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDFC.Parse.Contracts;

namespace Singularity.Android.Models {
    public class AndroidDeviceRowBuilder : IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            if(file != null) {
                return new AndroidFileRow(file);
            }
            return null;
        }
    }
}
