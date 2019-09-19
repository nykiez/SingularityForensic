using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LogFileRSTRRestartAreaStruct  {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] CURRENT_LSN;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] LOG_CLIENTS;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CLIENT_FREE_LIST;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CLIENT_IN_USE_LIST;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] FLAGS;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] SEQUENCE_NUMBER_BITS;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] RESTART_AREA_LENGTH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] CLIENT_ARRAY_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] FILE_SIZE;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] LAST_LSN_DATA_LENGTH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] LOG_RECORD_HD_LENGTH;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] LOGPAGE_DATA_OFFSET;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] RESTARTLOG_OPEN_COUNT;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] RESERVED_STRT_AREA;
        
    }

    public class LogFileRSTRRestartArea: StructFieldDecriptorBase<LogFileRSTRRestartAreaStruct> {
        public LogFileRSTRRestartArea(LogFileRSTRRestartAreaStruct logFileRSTRRestartAreaStruct):base(logFileRSTRRestartAreaStruct) {

        }

        public const int RSTRRestartAreaSize = 64;

        public override string DisplayName => LanguageService.FindResourceString(Constants.DisplayName_LogFileRSTRArea);

        protected override void OnEditFieldDescriptorDisplayName(FieldInfo fieldInfo, EditingValueEventArgs<string> args) {
            args.Value = LanguageService.FindResourceString($"{Constants.LogFileRSTRAreaFieldPrefix}{fieldInfo.Name}");
        }


        //####################################################################################################################
        //# Raw values
        //    @property
        //    def current_lsn_raw(self): return self.data[self.CURRENT_LSN[1]:self.CURRENT_LSN[2] + 1]

        //    @property
        //    def log_clients_raw(self): return self.data[self.LOG_CLIENTS[1]:self.LOG_CLIENTS[2] + 1]

        //    @property
        //    def client_free_list_raw(self): return self.data[self.CLIENT_FREE_LIST[1]:self.CLIENT_FREE_LIST[2] + 1]

        //    @property
        //    def client_in_use_list_raw(self): return self.data[self.CLIENT_IN_USE_LIST[1]:self.CLIENT_IN_USE_LIST[2] + 1]

        //    @property
        //    def flags_raw(self): return self.data[self.FLAGS[1]:self.FLAGS[2] + 1]

        //    @property
        //    def sequence_number_bits_raw(self): return self.data[self.SEQUENCE_NUMBER_BITS[1]:self.SEQUENCE_NUMBER_BITS[2] + 1]

        //    @property
        //    def restart_area_length_raw(self): return self.data[self.RESTART_AREA_LENGTH[1]:self.RESTART_AREA_LENGTH[2] + 1]

        //    @property
        //    def client_array_offset_raw(self): return self.data[self.CLIENT_ARRAY_OFFSET[1]:self.CLIENT_ARRAY_OFFSET[2] + 1]

        //    @property
        //    def file_size_raw(self): return self.data[self.FILE_SIZE[1]:self.FILE_SIZE[2] + 1]

        //    @property
        //    def last_lsn_data_length_raw(self): return self.data[self.LAST_LSN_DATA_LENGTH[1]:self.LAST_LSN_DATA_LENGTH[2] + 1]

        //    @property
        //    def log_record_hd_length_raw(self): return self.data[self.LOG_RECORD_HD_LENGTH[1]:self.LOG_RECORD_HD_LENGTH[2] + 1]

        //    @property
        //    def logpage_data_offset_raw(self): return self.data[self.LOGPAGE_DATA_OFFSET[1]:self.LOGPAGE_DATA_OFFSET[2] + 1]

        //    @property
        //    def restartlog_open_count_raw(self): return self.data[self.RESTARTLOG_OPEN_COUNT[1]:self.RESTARTLOG_OPEN_COUNT[2] + 1]

        //    @property
        //    def reserved_strt_area_raw(self): return self.data[self.RESERVED_STRT_AREA[1]:self.RESERVED_STRT_AREA[2] + 1]

        //####################################################################################################################
        //# Interpreted values
        //    @property
        //    def current_lsn(self): return reverse_hexlify_int(self.current_lsn_raw)

        //    @property
        //    def log_clients(self) : return reverse_hexlify_int(self.log_clients_raw)

        //    @property
        //    def sequence_number_bits(self) : return reverse_hexlify_int(self.sequence_number_bits_raw)

        //    @property
        //    def restart_area_length(self) : return reverse_hexlify_int(self.restart_area_length_raw)

        //    @property
        //    def client_array_offset(self) : return reverse_hexlify_int(self.client_array_offset_raw)

        //    @property
        //    def file_size(self) : return reverse_hexlify_int(self.file_size_raw)

        //    @property
        //    def last_lsn_data_length(self) : return reverse_hexlify_int(self.last_lsn_data_length_raw)

        //    @property
        //    def log_record_hd_length(self) : return reverse_hexlify_int(self.log_record_hd_length_raw)

        //    @property
        //    def logpage_data_offset(self) : return reverse_hexlify_int(self.logpage_data_offset_raw)

        //    @property
        //    def restartlog_open_count(self) : return reverse_hexlify_int(self.restartlog_open_count_raw)

        //    ####################################################################################################################
        //# Derived values

        //####################################################################################################################
        //# Printing

        //        def all_fields_described(self):
        //        return(
        //            (self.CURRENT_LSN, self.current_lsn, self.current_lsn_raw),
        //            (self.LOG_CLIENTS, self.log_clients, self.log_clients_raw),
        //            (self.CLIENT_FREE_LIST, '', self.client_free_list_raw),
        //            (self.CLIENT_IN_USE_LIST, '', self.client_in_use_list_raw),
        //            (self.FLAGS, '', self.flags_raw),
        //            (self.SEQUENCE_NUMBER_BITS, self.sequence_number_bits, self.sequence_number_bits_raw),
        //            (self.RESTART_AREA_LENGTH, self.restart_area_length, self.restart_area_length_raw),
        //            (self.CLIENT_ARRAY_OFFSET, self.client_array_offset, self.client_array_offset_raw),
        //            (self.FILE_SIZE, self.file_size, self.file_size_raw),
        //            (self.LAST_LSN_DATA_LENGTH, self.last_lsn_data_length, self.last_lsn_data_length_raw),
        //            (self.LOG_RECORD_HD_LENGTH, self.log_record_hd_length, self.log_record_hd_length_raw),
        //            (self.LOGPAGE_DATA_OFFSET, self.logpage_data_offset, self.logpage_data_offset_raw),
        //            (self.RESTARTLOG_OPEN_COUNT, self.restartlog_open_count, self.restartlog_open_count_raw),
        //            (self.RESERVED_STRT_AREA, '', self.reserved_strt_area_raw),
        //        )
    }
}
