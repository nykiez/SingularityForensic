using CDFC.Util.IO;
using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    public static class FileExplorerTreeUnitCommandItemFactory {
        public static ICommandItem CreateCustomSignSearchCommandItem(IBlockedStream blockedStream) {
            if(blockedStream == null) {
                throw new ArgumentNullException(nameof(blockedStream));
            }

            var cmi = CommandItemFactory.CreateNew(CreateCustomSignSearchCommand(blockedStream));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CustomSignSearch);
            return cmi;
        }

        private static DelegateCommand CreateCustomSignSearchCommand(IBlockedStream blockedStream) {
            if (blockedStream == null) {
                throw new ArgumentNullException(nameof(blockedStream));
            }
            
            var comm = new DelegateCommand(
                () => {
                    //blockedStream.BaseStream
                }
            );
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            
            return comm;
        }
    }
}
