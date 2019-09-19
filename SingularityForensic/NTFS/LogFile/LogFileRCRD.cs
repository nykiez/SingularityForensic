using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.LogFile {
    class LogFileRCRD: ILogFileRecord {
        public const int PAGE_HEADER_LENGTH = 64;
        public const int LSN_HEADER_LENGTH = 48;
        public const int SECTOR_SIZE = 512;
        public const int SECTOR_AMOUNT = 8;

        public LogFileRCRD(byte[] data, int pageNumber, LogFileRCRDLeftOverData remaining = null, int offset = 0, int clusterSize = 4096) {
            this.Data = data;
            this.PageNr = pageNumber;
            this.Offset = offset;
            this.ClusterSize = clusterSize;
            this.PreLeftOver = remaining;
            this.LSNStop = 0;
            this.Error = 0;

            var headerStruct = data.ToStructWithMarshal<LogFileRCRDHeaderStruct>();
            if (headerStruct == null) {
                throw new InvalidOperationException($"Failed to get {nameof(LogFileRSTRHeaderStruct)}.");
            }
            RCRDHeader = new LogFileRCRDHeader(headerStruct.Value);
            if (RCRDHeader.MalformedPage) {
                LoggerService.WriteCallerLine($"{RCRDHeader.MalformedPage} can't be true.");
                return;
            }

            this.OffsetDict = LogFileUtil.SearchFixup(this, this.Data);
            if(OffsetDict != null) {
                LogFileUtil.ReplaceFixup(this, this.Data);
            }

            if (this.PreLeftOver != null) {

            }
        }

        public byte[] Data { get; }
        public int PageNr { get; }
        public int Offset { get; }
        public int ClusterSize { get; }
        public LogFileRCRDLeftOverData PreLeftOver { get; }
        public int LSNStop { get; }
        public int Error { get; }

        public LogFileRCRDHeader RCRDHeader { get; }

        public ILogFileRecordHeader Header => throw new NotImplementedException();

        public int SectorSize => SECTOR_SIZE;

        public int SectorAmount => SECTOR_AMOUNT;
        public byte FinalizingData { get; private set; }
        public Dictionary<int, byte[]> OffsetDict { get; }
        public void ParsePrevLeftover() {
        //    FinalizingData = Data.SubBytes(PAGE_HEADER_LENGTH, PreLeftOver.missing_data_length]
        //    self.prev_leftover.data += finalizing_data
        //self.prev_leftover.lsn_data = LSNRecordData(self.prev_leftover.data, 0)
        //self.prev_leftover.lsn_hdr.page_nr = self.nr
        //self.prev_leftover.lsn_hdr.nr = str(0)
        //if not self.prev_leftover.lsn_hdr.malformed_entry() and not self.prev_leftover.lsn_data.malformed_entry():
        //    self.lsn_entries.append((self.prev_leftover.lsn_hdr, self.prev_leftover.lsn_data))
        //    self.offset = self.PAGE_HEADER_LENGTH + self.prev_leftover.missing_data_length
        }
        

        //####################################################################################################################
        //# class functions

        //# parse remaining split LSN from previous page
        //    def parse_prev_leftover(self):
        //        finalizing_data = self.data[self.PAGE_HEADER_LENGTH:self.PAGE_HEADER_LENGTH+self.prev_leftover.missing_data_length]
        //        self.prev_leftover.data += finalizing_data
        //        self.prev_leftover.lsn_data = LSNRecordData(self.prev_leftover.data, 0)
        //        self.prev_leftover.lsn_hdr.page_nr = self.nr
        //        self.prev_leftover.lsn_hdr.nr = str(0)
        //        if not self.prev_leftover.lsn_hdr.malformed_entry() and not self.prev_leftover.lsn_data.malformed_entry():
        //            self.lsn_entries.append((self.prev_leftover.lsn_hdr, self.prev_leftover.lsn_data))
        //            self.offset = self.PAGE_HEADER_LENGTH + self.prev_leftover.missing_data_length

        //# dump raw data to file
        //    def dump_page_to_file(self, header=None, cursor=None):
        //        pre = 'non_valid_page_' if header else 'page_'
        //        offset = '_'+str(cursor) if cursor else ''
        //# dump_path = os.path.join(os.getcwd(), self.dump_dir) if self.dump_dir else os.getcwd()
        //# print(dump_path)
        //        filename = os.path.join(self.dump_dir, pre+self.nr+offset)
        //        with open(filename, 'wb') as f:
        //            f.write(self.data)

        //####################################################################################################################
        //# PRINT functions
        //    def writeout_parsed(self, out):
        //        self.writeout_basic(out)

        //    def writeout_headers(self, out):
        //        self.writeout_page_header(out)
        //        for (lsn_header, lsn_data) in self.lsn_entries:
        //            lsn_header.writeout_parsed(out)

        //    def writeout_basic(self, out):
        //        self.writeout_page_header(out)
        //        for (lsn_header, lsn_data) in self.lsn_entries:
        //            lsn_header.writeout_parsed(out)
        //            lsn_data.writeout_parsed(out)

        //    def writeout_leftover(self, out):
        //        if self.leftover:
        //            self.leftover.writeout_header(out)
        //            self.leftover.writeout_data(out)
        //# self.leftover.writeout_raw_data(out)
        //        else:
        //            out.write('    ---- No LEFTOVER ----\n')

        //    def writeout_all(self, out):
        //        self.writeout_page_header(out)
        //        for (lsn_header, lsn_data) in self.lsn_entries:
        //            lsn_header.writeout_parsed(out)
        //            lsn_data.writeout_parsed(out)
        //            lsn_data.writeout_operation_data(out)
        //            lsn_data.writeout_itrprt_op_data(out)
        //        self.writeout_leftover(out)

        //    def writeout_page_header(self, out):
        //        out.write('\n'
        //                  '=================================================================================================\n'
        //                  '= RCRD Record %s\n' % self.nr)
        //        out.write('  ---- log page Header --------------------------------------------------------------------------\n')
        //        for (description, low, high), value, value_raw in self.header.all_fields_described():
        //            out.write('  %-30s | %-5s | %-11s | %s\n' % (description, str(low) + '-' + str(high),
        //                                                           value, hexlify(value_raw)))

        //    def export_csv(self, csv_writer):
        //        if not self.lsn_entries:
        //            csv_writer.writerow(self.formatted_csv())
        //            return
        //        for (lsn_hdr, lsn_data) in self.lsn_entries:
        //            record = self.formatted_csv()
        //            record.extend(lsn_hdr.formatted_csv())
        //            record.extend(lsn_data.formatted_csv())
        //            csv_writer.writerow(record)

        //# Some ordering variables, used in the future, not finished
        //    @property
        //    def connector_prev_lsn(self):
        //        if self.lsn_entries:
        //            return self.lsn_entries[0][0].previous_lsn
        //        else:
        //            return None

        //    @property
        //    def connector_last_lsn(self):
        //        return self.header.last_end_lsn

        //    @property
        //    def entry_count(self):
        //        return len(self.lsn_entries)

        //    @property
        //    def formatted_csv_column_headers(self):
        //        return ['conn prev LSN',  # Page info
        //                'conn last LSN',
        //                ]

        //    @property
        //    def lsn_header_csv_columns(self):
        //        return LSNRecordHeader.formatted_csv_column_headers()

        //    @property
        //    def lsn_data_csv_columns(self):
        //        return LSNRecordData.formatted_csv_column_headers()

        //    def formatted_csv(self):
        //        return[self.connector_prev_lsn,
        //               self.connector_last_lsn
        //               ]
        //    }
    }
}
