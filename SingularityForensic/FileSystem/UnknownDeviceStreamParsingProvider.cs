using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IStreamParsingProvider))]
    public class UnknownDeviceStreamParsingProvider : IStreamParsingProvider {
        public int Order => 128;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            return true;
        }
        
        public IHaveFileCollection ParseStream(Stream stream, string name, 
            XElement xElem, ProgressReporter reporter) {
            DeviceStoken stoken = new DeviceStoken {
                BaseStream = stream,
                PartitionEntries = Enumerable.Empty<PartitionEntry>(),
                PartsType = LanguageService.Current?.FindResourceString(Constants.UnknownPartsType),
                Name = name,
                TypeGuids = new string[] {
                    Constants.UnknownDeviceType
                },
                BlockSize = 0,
                Size = stream.Length
            };
            
            return new Device(Constants.UnknownDeviceKey, stoken);
        }
    }
}
