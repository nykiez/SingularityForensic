using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hex {
    [Export(typeof(IBufferToCodeFormatter))]
    class CSharpBufferToCodeFormatter : BufferToCodeFormatterBase {
        public override string GUID => Constants.BufferToProCodeFormatterGUID_CSharp;

        public override string CodeLanguageName => "C#";

        public override int Sort => 0;

        protected override void DealWithStringBuilder(StringBuilder sb,byte[] buffer) {
            sb.Append($"string sData =\"{ByteExtensions.BytesToString(buffer)}\";");
            sb.AppendLine();
            sb.Append($"string sDataHex =\"{ByteExtensions.BytesToHexString(buffer)}\";");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("byte[] rawData = {");
            sb.AppendLine();
            sb.Append("\t");
        }
    }

    [Export(typeof(IBufferToCodeFormatter))]
    class FSharpBufferToCodeFormatter : BufferToCodeFormatterBase {
        public override string GUID => Constants.BufferToProCodeFormatterGUID_FSharp;

        public override string CodeLanguageName => "F#";

        public override int Sort => 4;

        protected override void DealWithStringBuilder(StringBuilder sb, byte[] buffer) {
            sb.Append($"let sData = @\"{ByteExtensions.BytesToString(buffer)}\";");
            sb.AppendLine();
            sb.Append($"let sDataHex = @\"{ByteExtensions.BytesToHexString(buffer)}\";");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("let bytes = [|");
            sb.AppendLine();
            sb.Append("    ");
        }
    }

    [Export(typeof(IBufferToCodeFormatter))]
    class JAVABufferToCodeFormatter : BufferToCodeFormatterBase {
        public override string GUID => Constants.BufferToProCodeFormatterGUID_JAVA;

        public override string CodeLanguageName => "JAVA";

        public override int Sort => 8;

        protected override void DealWithStringBuilder(StringBuilder sb, byte[] buffer) {
            sb.Append($"String sData =\"{ByteExtensions.BytesToString(buffer)}\";");
            sb.AppendLine();
            sb.Append($"String sDataHex =\"{ByteExtensions.BytesToHexString(buffer)}\";");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("byte rawData[] = {");
            sb.AppendLine();
            sb.Append("\t");

            var i = 0;
            foreach (byte b in buffer) {
                i++;
                sb.Append("(byte)");
                if (i == 6) {
                    i = 0;
                    sb.AppendLine();
                    sb.Append("\t");
                }
            }

            sb.AppendLine();
            sb.Append("};");
        }
    }
    
    [Export(typeof(IBufferToCodeFormatter))]
    class CBufferToCodeFormatter : BufferToCodeFormatterBase {
        public override string GUID => Constants.BufferToProCodeFormatterGUID_C;

        public override string CodeLanguageName => "C#";

        public override int Sort => 12;

        protected override void DealWithStringBuilder(StringBuilder sb, byte[] buffer) {
            sb.Append($"char sData[] =\"{ByteExtensions.BytesToString(buffer)}\";");
            sb.AppendLine();
            sb.Append($"char sDataHex[] =\"{ByteExtensions.BytesToHexString(buffer)}\";");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append($"unsigned char rawData[{buffer.Length}]{{ ");
            sb.AppendLine();
            sb.Append("\t");
        }
    }

    [Export(typeof(IBufferToCodeFormatter))]
    class VBNETBufferToCodeFormatter : BufferToCodeFormatterBase {
        public override string GUID => Constants.BufferToProCodeFormatterGUID_VBNET;

        public override string CodeLanguageName => "VB.NET";

        public override int Sort => 16;

        protected override void DealWithStringBuilder(StringBuilder sb, byte[] buffer) {
            sb.Append($"Dim sData as String =\"{ByteExtensions.BytesToString(buffer)}\";");
            sb.AppendLine();
            sb.Append($"Dim sDataHex as String =\"{ByteExtensions.BytesToHexString(buffer)}\";");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("Dim rawData As Byte() = { _");
            sb.AppendLine();
            sb.Append("\t");
        }
    }

    abstract class BufferToCodeFormatterBase : IBufferToCodeFormatter {
        public abstract string GUID { get; }

        public abstract string CodeLanguageName { get; }

        public abstract int Sort { get; }

        public virtual string FormatAsCode(byte[] buffer) {
            if (buffer == null) {
                throw new ArgumentNullException(nameof(buffer));
            }

            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine();
            DealWithStringBuilder(sb, buffer);
            sb.AppendLine();
            return sb.ToString();
        }

        protected abstract void DealWithStringBuilder(StringBuilder sb,byte[] buffer);
    }
}
