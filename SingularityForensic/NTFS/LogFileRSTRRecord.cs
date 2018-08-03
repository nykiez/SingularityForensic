//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace SingularityForensic.NTFS {
    class LogFileRSTRRecord {
        const int SECTOR_SIZE = 512;

        const int SECTOR_AMOUNT = 8;

        LogFileRSTRRecord(byte[] data) {

            //    self.header = RestartPageHeader(data[:48])     # 0x00 - 0x2F
            //self.offset_dict = search_fixup(self, data)
            //if self.offset_dict:

            //     self.data = replace_fixup(self, data)
            //else:

            //     raise Exception('Fixup error')

            // self.restart_area = RestartArea(self.data[48:112])  # 0x30 - 0x6F
            //self.log_client = LogClient(self.data[112:])        # 0x70 - END (0xC0)
        }




        //void writeout_parsed() {
        //    Console.WriteLine("\n" +
        //          "=================================================================================================\n" +
        //          "= RSTR Record\n");
        //    Console.WriteLine("    ---- Restart Page Header --------------------------------------------------------------------\n");
        //    for (description, low, high), value, value_raw in self.header.all_fields_described():
        //        out.write('    %-36s | %-7s | %-11s | %s\n' % (description, str(low) + '-' + str(high),
        //                                                        value, hexlify(value_raw)))
        //       out.write('    ---- Restart Area ---------------------------------------------------------------------------\n')
        //       for (description, low, high), value, value_raw in self.restart_area.all_fields_described():
        //        out.write('    %-36s | %-7s | %-11s | %s\n' % (description, str(low) + '-' + str(high),
        //                                                        value, hexlify(value_raw)))
        //       out.write('    ---- Log Client -----------------------------------------------------------------------------\n')
        //       for (description, low, high), value, value_raw in self.log_client.all_fields_described():
        //        out.write('    %-36s | %-7s | %-11s | %s\n' % (description, str(low) + '-' + str(high),
        //                                                        value, hexlify(value_raw)))

        //}


    }

    class RestartPageHeader {
    }
        //        //########################################################################################################################
        //        //# RESTART page header
        //        class RestartPageHeader {
        //            MAGIC_NUMBER           = ('magic number (name)', 0, 3)
        //    UPDATE_SEQUENCE_OFFSET = ('update seq. offset', 4, 5)
        //    UPDATE_SEQUENCE_COUNT  = ('update seq. count', 6, 7)
        //    CHECK_DISK_LSN         = ('check disk lsn', 8, 15)
        //    SYSTEM_PAGE_SIZE       = ('system page size', 16, 19)
        //    LOG_PAGE_SIZE          = ('log page size', 20, 23)
        //    RESTART_AREA_OFFSET    = ('restart area offset', 24, 25)
        //    MINOR_VERSION          = ('minor version', 26, 27)
        //    MAJOR_VERSION          = ('major version', 28, 29)
        //    FIXUP_VALUE            = ('fixup value', 30, 31)
        //    FIXUP_ARRAY            = ('fixup array', 32, 47)

        //    def __init__(self, data):
        //        self.data = data

        //####################################################################################################################
        //# Raw values
        //    @property
        //    def magic_number_raw(self): return self.data[self.MAGIC_NUMBER[1]:self.MAGIC_NUMBER[2] + 1]

        //    @property
        //    def update_sequence_offset_raw(self): return self.data[self.UPDATE_SEQUENCE_OFFSET[1]:self.UPDATE_SEQUENCE_OFFSET[2] + 1]

        //    @property
        //    def update_sequence_count_raw(self): return self.data[self.UPDATE_SEQUENCE_COUNT[1]:self.UPDATE_SEQUENCE_COUNT[2] + 1]

        //    @property
        //    def check_disk_lsn_raw(self): return self.data[self.CHECK_DISK_LSN[1]:self.CHECK_DISK_LSN[2] + 1]

        //    @property
        //    def system_page_size_raw(self): return self.data[self.SYSTEM_PAGE_SIZE[1]:self.SYSTEM_PAGE_SIZE[2] + 1]

        //    @property
        //    def log_page_size_raw(self): return self.data[self.LOG_PAGE_SIZE[1]:self.LOG_PAGE_SIZE[2] + 1]

        //    @property
        //    def restart_area_offset_raw(self): return self.data[self.RESTART_AREA_OFFSET[1]:self.RESTART_AREA_OFFSET[2] + 1]

        //    @property
        //    def minor_version_raw(self): return self.data[self.MINOR_VERSION[1]:self.MINOR_VERSION[2] + 1]

        //    @property
        //    def major_version_raw(self): return self.data[self.MAJOR_VERSION[1]:self.MAJOR_VERSION[2] + 1]

        //    @property
        //    def fixup_value_raw(self): return self.data[self.FIXUP_VALUE[1]:self.FIXUP_VALUE[2] + 1]

        //    @property
        //    def fixup_array_raw(self): return self.data[self.FIXUP_ARRAY[1]:self.FIXUP_ARRAY[2] + 1]

        //####################################################################################################################
        //# Interpreted values
        //    @property
        //    def magic_number(self): return self.magic_number_raw.decode()

        //    @property
        //    def update_sequence_offset(self): return reverse_hexlify_int(self.update_sequence_offset_raw)

        //    @property
        //    def update_sequence_count(self) : return reverse_hexlify_int(self.update_sequence_count_raw)

        //    @property
        //    def system_page_size(self) : return reverse_hexlify_int(self.system_page_size_raw)

        //    @property
        //    def log_page_size(self) : return reverse_hexlify_int(self.log_page_size_raw)

        //    @property
        //    def restart_area_offset(self) : return reverse_hexlify_int(self.restart_area_offset_raw)

        //    @property
        //    def minor_version(self) : return reverse_hexlify_int(self.minor_version_raw)

        //    @property
        //    def major_version(self) : return reverse_hexlify_int(self.major_version_raw)

        //    @property
        //    def fixup_value(self) : return hexlify(self.fixup_value_raw)

        //    ####################################################################################################################
        //# Derived values

        //####################################################################################################################
        //# Printing

        //            def all_fields_described(self):
        //        return(
        //            (self.MAGIC_NUMBER, self.magic_number, self.magic_number_raw),
        //            (self.UPDATE_SEQUENCE_OFFSET, self.update_sequence_offset, self.update_sequence_offset_raw),
        //            (self.UPDATE_SEQUENCE_COUNT, self.update_sequence_count, self.update_sequence_count_raw),
        //            (self.CHECK_DISK_LSN, '', self.check_disk_lsn_raw),
        //            (self.SYSTEM_PAGE_SIZE, self.system_page_size, self.system_page_size_raw),
        //            (self.LOG_PAGE_SIZE, self.log_page_size, self.log_page_size_raw),
        //            (self.RESTART_AREA_OFFSET, self.restart_area_offset, self.restart_area_offset_raw),
        //            (self.MINOR_VERSION, self.minor_version, self.minor_version_raw),
        //            (self.MAJOR_VERSION, self.major_version, self.major_version_raw),
        //            (self.FIXUP_VALUE, self.fixup_value, self.fixup_value_raw),
        //            (self.FIXUP_ARRAY, '', self.fixup_array_raw),
        //        )

        //    }


        //    }
    }