using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IUnknownDeviceParsingProvider))]
    public class UnknownDeviceStreamParsingProvider : IUnknownDeviceParsingProvider {
        public int Order => int.MaxValue;

        public string GUID => Constants.StreamParser_Unknown;

        public bool CheckIsValidStream(Stream stream) {
            if(stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            return true;
        }
        
        public IDevice ParseStream(Stream stream, string name, XElement xElem) {
            var device = FileFactory.CreateDevice(Constants.DeviceKey_Unknown);
            var stoken = device.GetStoken(Constants.DeviceKey_Unknown);
            stoken.BaseStream = stream;
            stoken.PartsType = LanguageService.Current?.FindResourceString(Constants.PartsType_Unknown);
            stoken.Name = name;
            stoken.TypeGuids = new string[] {
                    Constants.DeviceType_Unknown
                };
            stoken.BlockSize = 0;
            stoken.Size = stream.Length;

            return device;
        }
    }

    [Export(typeof(IUnknownPartitionParsingProvider))]
    public class UnknownPartStreamParsingProvider : IUnknownPartitionParsingProvider {
      
        public IPartition ParseStream(Stream stream, string name, XElement xElem) {
            var part = FileFactory.CreatePartition(Constants.PartitionKey_Unknown);
            var partStoken = part.GetStoken(Constants.PartitionKey_Unknown);
            partStoken.BaseStream = stream;
            partStoken.Name = name;
            partStoken.TypeGuids = new string[] {
                Constants.PartitionType_Unknown
            };
            partStoken.BlockSize = 0;
            partStoken.Size = stream.Length;

            return part;
        }
    }

}
