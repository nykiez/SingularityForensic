using SingularityForensic.Contracts.Common;
using System;

namespace SingularityForensic.NTFS {
    public class BiosParameterBlock {
        [PropDescrible(0x24)]
        public byte BiosDriveNumber{ get; private set; } // Value: 0x80 (first hard disk)
        [PropDescrible(0x0B)]
        public ushort BytesPerSector { get; private set; }

        public byte ChkDskFlags { get; private set; }  // Value: 0x00
        [PropDescrible(0x10)]
        public ushort FatRootEntriesCount { get; private set; }  // Must be 0
        public ushort FatSize16 { get; private set; }  // Must be 0
        public uint HiddenSectors { get; private set; }  // Value: 0x3F 0x00 0x00 0x00
        [PropDescrible(0x15)]
        public byte Media { get; private set; }  // Must be 0xF8
        public long MftCluster { get; private set; }
        public long MftMirrorCluster { get; private set; }
        [PropDescrible(0x11)]
        public byte NumFats { get; private set; }  // Must be 0
        public ushort NumHeads { get; private set; }  // Value: 0xFF 0x00
        public string OemId { get; private set; }
        public byte PaddingByte { get; private set; } // Value: 0x00
        public byte RawIndexBufferSize { get; private set; }
        public byte RawMftRecordSize { get; private set; }
        [PropDescrible(0x0E)]
        public ushort ReservedSectors { get; private set; }  // Must be 0
        [PropDescrible(0x0D)]
        public byte SectorsPerCluster { get; private set; }
        [PropDescrible(0x18)]
        public ushort SectorsPerTrack { get; private set; } // Value: 0x3F 0x00
        public byte SignatureByte { get; private set; } // Value: 0x80
        [PropDescrible(0x16)]
        public ushort TotalSectors16 { get; private set; } // Must be 0
        public uint TotalSectors32 { get; private set; }  // Must be 0
        public long TotalSectors64 { get; private set; }
        
        public ulong VolumeSerialNumber { get; private set; }

        public int BytesPerCluster {
            get { return BytesPerSector * SectorsPerCluster; }
        }

        public int IndexBufferSize {
            get { return CalcRecordSize(RawIndexBufferSize); }
        }

        public int MftRecordSize {
            get { return CalcRecordSize(RawMftRecordSize); }
        }

        //public void Dump(TextWriter writer, string linePrefix) {
        //    writer.WriteLine(linePrefix + "BIOS PARAMETER BLOCK (BPB)");
        //    writer.WriteLine(linePrefix + "                OEM ID: " + OemId);
        //    writer.WriteLine(linePrefix + "      Bytes per Sector: " + BytesPerSector);
        //    writer.WriteLine(linePrefix + "   Sectors per Cluster: " + SectorsPerCluster);
        //    writer.WriteLine(linePrefix + "      Reserved Sectors: " + ReservedSectors);
        //    writer.WriteLine(linePrefix + "                # FATs: " + NumFats);
        //    writer.WriteLine(linePrefix + "    # FAT Root Entries: " + FatRootEntriesCount);
        //    writer.WriteLine(linePrefix + "   Total Sectors (16b): " + TotalSectors16);
        //    writer.WriteLine(linePrefix + "                 Media: " + Media.ToString("X", CultureInfo.InvariantCulture) +
        //                     "h");
        //    writer.WriteLine(linePrefix + "        FAT size (16b): " + FatSize16);
        //    writer.WriteLine(linePrefix + "     Sectors per Track: " + SectorsPerTrack);
        //    writer.WriteLine(linePrefix + "               # Heads: " + NumHeads);
        //    writer.WriteLine(linePrefix + "        Hidden Sectors: " + HiddenSectors);
        //    writer.WriteLine(linePrefix + "   Total Sectors (32b): " + TotalSectors32);
        //    writer.WriteLine(linePrefix + "     BIOS Drive Number: " + BiosDriveNumber);
        //    writer.WriteLine(linePrefix + "          Chkdsk Flags: " + ChkDskFlags);
        //    writer.WriteLine(linePrefix + "        Signature Byte: " + SignatureByte);
        //    writer.WriteLine(linePrefix + "   Total Sectors (64b): " + TotalSectors64);
        //    writer.WriteLine(linePrefix + "       MFT Record Size: " + RawMftRecordSize);
        //    writer.WriteLine(linePrefix + "     Index Buffer Size: " + RawIndexBufferSize);
        //    writer.WriteLine(linePrefix + "  Volume Serial Number: " + VolumeSerialNumber);
        //}

        //internal static BiosParameterBlock Initialized(Geometry diskGeometry, int clusterSize, uint partitionStartLba,
        //                                               long partitionSizeLba, int mftRecordSize, int indexBufferSize) {
        //    BiosParameterBlock bpb = new BiosParameterBlock();
        //    bpb.OemId = "NTFS    ";
        //    bpb.BytesPerSector = Sizes.Sector;
        //    bpb.SectorsPerCluster = (byte)(clusterSize / bpb.BytesPerSector);
        //    bpb.ReservedSectors = 0;
        //    bpb.NumFats = 0;
        //    bpb.FatRootEntriesCount = 0;
        //    bpb.TotalSectors16 = 0;
        //    bpb.Media = 0xF8;
        //    bpb.FatSize16 = 0;
        //    bpb.SectorsPerTrack = (ushort)diskGeometry.SectorsPerTrack;
        //    bpb.NumHeads = (ushort)diskGeometry.HeadsPerCylinder;
        //    bpb.HiddenSectors = partitionStartLba;
        //    bpb.TotalSectors32 = 0;
        //    bpb.BiosDriveNumber = 0x80;
        //    bpb.ChkDskFlags = 0;
        //    bpb.SignatureByte = 0x80;
        //    bpb.PaddingByte = 0;
        //    bpb.TotalSectors64 = partitionSizeLba - 1;
        //    bpb.RawMftRecordSize = bpb.CodeRecordSize(mftRecordSize);
        //    bpb.RawIndexBufferSize = bpb.CodeRecordSize(indexBufferSize);
        //    bpb.VolumeSerialNumber = GenSerialNumber();

        //    return bpb;
        //}

        public static BiosParameterBlock FromBytes(byte[] bytes, int offset) {
            BiosParameterBlock bpb = new BiosParameterBlock();
            bpb.OemId = EndianUtilities.BytesToString(bytes, offset + 0x03, 8);
            bpb.BytesPerSector = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x0B);
            bpb.SectorsPerCluster = bytes[offset + 0x0D];
            bpb.ReservedSectors = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x0E);
            bpb.NumFats = bytes[offset + 0x10];
            bpb.FatRootEntriesCount = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x11);
            bpb.TotalSectors16 = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x13);
            bpb.Media = bytes[offset + 0x15];
            bpb.FatSize16 = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x16);
            bpb.SectorsPerTrack = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x18);
            bpb.NumHeads = EndianUtilities.ToUInt16LittleEndian(bytes, offset + 0x1A);
            bpb.HiddenSectors = EndianUtilities.ToUInt32LittleEndian(bytes, offset + 0x1C);
            bpb.TotalSectors32 = EndianUtilities.ToUInt32LittleEndian(bytes, offset + 0x20);
            bpb.BiosDriveNumber = bytes[offset + 0x24];
            bpb.ChkDskFlags = bytes[offset + 0x25];
            bpb.SignatureByte = bytes[offset + 0x26];
            bpb.PaddingByte = bytes[offset + 0x27];
            bpb.TotalSectors64 = EndianUtilities.ToInt64LittleEndian(bytes, offset + 0x28);
            bpb.MftCluster = EndianUtilities.ToInt64LittleEndian(bytes, offset + 0x30);
            bpb.MftMirrorCluster = EndianUtilities.ToInt64LittleEndian(bytes, offset + 0x38);
            bpb.RawMftRecordSize = bytes[offset + 0x40];
            bpb.RawIndexBufferSize = bytes[offset + 0x44];
            bpb.VolumeSerialNumber = EndianUtilities.ToUInt64LittleEndian(bytes, offset + 0x48);

            return bpb;
        }

        void ToBytes(byte[] buffer, int offset) {
            EndianUtilities.StringToBytes(OemId, buffer, offset + 0x03, 8);
            EndianUtilities.WriteBytesLittleEndian(buffer, BytesPerSector, offset + 0x0B);
            buffer[offset + 0x0D] = SectorsPerCluster;
            EndianUtilities.WriteBytesLittleEndian(buffer, ReservedSectors,  offset + 0x0E);
            buffer[offset + 0x10] = NumFats;
            EndianUtilities.WriteBytesLittleEndian(buffer, FatRootEntriesCount, offset + 0x11);
            EndianUtilities.WriteBytesLittleEndian(buffer, TotalSectors16, offset + 0x13);
            buffer[offset + 0x15] = Media;
            EndianUtilities.WriteBytesLittleEndian(buffer, FatSize16, offset + 0x16);
            EndianUtilities.WriteBytesLittleEndian(buffer, SectorsPerTrack, offset + 0x18);
            EndianUtilities.WriteBytesLittleEndian(buffer, NumHeads,  offset + 0x1A);
            EndianUtilities.WriteBytesLittleEndian(buffer, HiddenSectors, offset + 0x1C);
            EndianUtilities.WriteBytesLittleEndian(buffer, TotalSectors32, offset + 0x20);
            buffer[offset + 0x24] = BiosDriveNumber;
            buffer[offset + 0x25] = ChkDskFlags;
            buffer[offset + 0x26] = SignatureByte;
            buffer[offset + 0x27] = PaddingByte;
            EndianUtilities.WriteBytesLittleEndian(buffer, TotalSectors64, offset + 0x28);
            EndianUtilities.WriteBytesLittleEndian(buffer, MftCluster,  offset + 0x30);
            EndianUtilities.WriteBytesLittleEndian(buffer, MftMirrorCluster, offset + 0x38);
            buffer[offset + 0x40] = RawMftRecordSize;
            buffer[offset + 0x44] = RawIndexBufferSize;
            EndianUtilities.WriteBytesLittleEndian(buffer, VolumeSerialNumber,  offset + 0x48);
        }

        int CalcRecordSize(byte rawSize) {
            if ((rawSize & 0x80) != 0) {
                return 1 << -(sbyte)rawSize;
            }
            return rawSize * SectorsPerCluster * BytesPerSector;
        }

        private static ulong GenSerialNumber() {
            byte[] buffer = new byte[8];
            Random rng = new Random();
            rng.NextBytes(buffer);
            return EndianUtilities.ToUInt64LittleEndian(buffer, 0);
        }

        private byte CodeRecordSize(int size) {
            if (size >= BytesPerCluster) {
                return (byte)(size / BytesPerCluster);
            }
            sbyte val = 0;
            while (size != 1) {
                size = (size >> 1) & 0x7FFFFFFF;
                val++;
            }

            return (byte)-val;
        }
    }
}
