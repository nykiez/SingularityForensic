using SingularityForensic.Contracts.Hex;
using System;

namespace SingularityForensic.Hex.Models {
    class BytesToCharEncodingWrapper : WpfHexaEditor.Core.Interfaces.IBytesToCharEncoding{
        public BytesToCharEncodingWrapper(IBytesToCharEncoding encoding) {
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public IBytesToCharEncoding Encoding { get; }
        public int BytePerChar => Encoding.BytePerChar;

        public char ConvertToChar(byte[] bytesToConvert) => Encoding.Convert(bytesToConvert);
        
    }
}
