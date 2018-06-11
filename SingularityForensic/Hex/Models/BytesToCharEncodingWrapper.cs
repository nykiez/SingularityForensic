using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hex.Models {
    class BytesToCharEncodingWrapper : WpfHexaEditor.Core.Interfaces.IBytesToCharEncoding {
        public BytesToCharEncodingWrapper(IBytesToCharEncoding encoding) {
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public IBytesToCharEncoding Encoding { get; }
        public int BytePerChar => Encoding.BytePerChar;

        public char Convert(byte[] bytesToConvert) => Encoding.Convert(bytesToConvert);
    }
}
