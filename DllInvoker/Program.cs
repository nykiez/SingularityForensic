using CDFC.Info.Android;
using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DllInvoker {
    class Program {
        public static long[] GetByteCount2(Stream stream) {

            int bufferLenght = 1048576; //1mb
            byte[] buffer = new byte[bufferLenght];
            long[] storedCnt = new long[256];

            while (stream.Position < stream.Length) {

                var readSize = stream.Read(buffer, 0, bufferLenght);

                for (int i = 0; i < readSize; i++) {
                    storedCnt[buffer[i]]++;
                }
                //foreach (byte b in buffer)
                //    storedCnt[b]++;

                Console.WriteLine($"{stream.Position}");
                //TODO: Recalcul position for EOF...
                //Position += bufferLenght;
            }

            return storedCnt;
        }
        public static int MaxThreadNum = 2;
        public static int BufferSize = 104857600;
        public static long[] GetByteCount(Stream stream) {
            var cd = new long[byte.MaxValue + 1];

            byte[] buffer = new byte[BufferSize];

            var locker = new object();
            var btArrArr = new byte[MaxThreadNum][];

            for (int i = 0; i < MaxThreadNum; i++) {
                btArrArr[i] = new byte[BufferSize];
            }


            var evts = new ManualResetEvent[MaxThreadNum];
            for (int i = 0; i < MaxThreadNum; i++) {
                evts[i] = new ManualResetEvent(false);
            }

            for (int i = 0; i < MaxThreadNum; i++) {
                //Deploy 4 theads for reading and counting.
                var arrayIndex = i;
                new Thread(cb => {
                    while (true) {
                        var readSize = 0;
                        lock (stream) {
                            readSize = stream.Read(buffer, 0, BufferSize);
                        }
                        Console.WriteLine($"{stream.Position}");
                        for (int j = 0; j < readSize; j++) {
                            cd[buffer[j]]++;
                        }
                        if (readSize == 0) {
                            evts[arrayIndex].Set();
                        }
                    }

                }).Start();
            }

            WaitHandle.WaitAll(evts);
            return cd;
        }
        private static void DO() {
            StackFrame frame = new StackFrame(1);
            var me = frame.GetMethod();
            var ns = me.ReflectedType.Name;
        }

        static void Main(string[] args) {
            
            //try {
            //    if(args.Length < 2) {
            //        Console.WriteLine("Invalid start args count!");
            //        throw new ArgumentException(nameof(args));
            //    }
            //    var vm = new CreateCaseWindowViewModel();
            //    SingularityCase.Current = vm.SingulartityCase;
            //    SingularityCase.Current.Save();
            //    var pro = 0;
            //    var device = AndroidDevice.LoadFromPath(args[0], true, tp => {
            //        if (tp.curSize * 100 / tp.allSize > pro) {
            //            pro = (int)(tp.curSize * 100 / tp.allSize);
            //            Console.WriteLine($"{tp.curSize}/{tp.allSize}");
            //        }
            //    });
            //    var adv = new AndroidDeviceCaseFile(device, string.Empty, DateTime.Now);
            //    Console.WriteLine($"Building Xml Doc");
            //    SingularityCase.Current.AddCaseFile(adv);
            //    var tp2 = AdvPythonHelper.GetProcessOutPut(adv, args[1]);

            //    Console.WriteLine("Parsing Finished...");
            //    Process.Start("explorer", "/e,/select," + $"{tp2.outPutPath.Replace("//","\\").Replace("/","\\")}\\{tp2.outDocName}");
            //    Console.ReadKey();
            //}
            //catch (Exception ex) {
            //    Console.WriteLine(ex.Message);
            //    Console.WriteLine("Press to exit");
            //    Console.ReadKey();
            //}

            ////AndroidDeviceCaseFile adc = new AndroidDeviceCaseFile();
            //string[] ss = new string[] {
            //    "AdbFileAudio",
            //    "AdbFileImage",
            //    "AdbFileVideo",
            //    "AdbInfoContact",
            //    "AdbInfoSms",
            //    "AdbInfoCalllog",
            //    "AdbInfoGps",
            //    "AdbInfoPackage"
            //};
            //foreach (var s in ss) {
            //    Console.WriteLine($"public const string {s} = nameof({s});");
            //}
            //Type[] tps = new Type[] { typeof(AdbContactModel), typeof(AdbCalllogModel), typeof(AdbGpsModel), typeof(AdbPackageModel) };
            //foreach (var tp in tps) {
            //    foreach (var prop in tp.GetProperties()) {
            //        Console.WriteLine($"<sys:String x:Key=/"{prop.Name}/"></sys:String>");
            //    }
            //}


            ////foreach (var prop in tp.GetProperties()) {
            ////    Console.WriteLine($"<DataGridTextColumn Binding = /"{{Binding {prop.Name}}}/" Header=/"{{DynamicResource {prop.Name}}}/"/>");
            ////}

            ////Console.WriteLine($"else if (typeof({tp.Name}) == typeof(TDbModel)){{"+Environment.NewLine
            ////    +$"return PinKindsDefinitions.{tp.Name};" + Environment.NewLine + "}");

            ////System.IO.Directory.CreateDirectory("D:/SingularitySolution/SingularityShell/bin/Debug/Cases/Case9-26-2017 5-44-28 PM/AndroidDevices/3dfc3760ca2a442a93aa2cf03f4f24fd-mmcblk0/1a9befeed0af43a697638a398a1ec5f0/23/data/com.tencent.mobileqq/databases");
            ////var key = Registry.LocalMachine;
            //////var software = key.CreateSubKey("SoftWare//CDFC//1CE90000000467414D410000B18F0BFC");
            ////key.DeleteSubKey("SoftWare//CDFC//1CE90000000467414D410000B18F0BFC");
            ////Console.WriteLine("Done");
            ////var s = Guid.NewGuid().ToString();
            ////using (var pro = PythonHelper.GetProcess("Phone_msg_calllog_extract.py", "D://C# Console/Python/read.xml")) {
            ////    pro.Close();
            ////}
            
            var connString = "data source = D://C# Console/Python/2017/qq_2112355002.db";
            var conn = new SQLiteConnection(connString);

            var ad = new SQLiteDataAdapter("select * from WA_MFORENSICS_020200", conn);
            var dt = new DataSet();
            ad.Fill(dt);
            ad.Dispose();
            foreach (DataColumn col in dt.Tables[0].Columns) {
                string tpString = string.Empty;
                if (col.DataType == typeof(long)) {
                    tpString = "long";
                }
                else if (col.DataType == typeof(string)) {
                    tpString = "string";
                }
                else if (col.DataType == typeof(byte[])) {
                    tpString = "byte[]";
                }
                else {
                    throw new Exception();
                }
                Console.WriteLine($"public {tpString} {col.ColumnName} {{get;set;}}");
            }

            var context = new AndroidDeviceQQContext(connString);
            foreach (var mem in context.Friends) {

            }
        }

        /// <summary>
        /// 大华系统的年月日，分，进制数字规则；
        /// </summary>
        //    private static readonly uint[] DHHexNum = new uint[] {
        //        67108864,4194304,131072,4096,64,1
        //    };
        //    #region 各系统的时间获得方法
        //    /// <summary>
        //    /// 获得大华的时间
        //    /// </summary>
        //    /// <param name="dateNum">从起始时间的偏移量</param>
        //    /// <returns>用短整型数组表示的年月日，时分秒</returns>
        //    private static DateTime? GetDHTime(uint dateNum) {
        //        short[] dateNums = new short[6];
        //        DateTime dt;

        //        for (byte index = 0; index < 6; index++) {
        //            var innerDateNum = dateNum;
        //            for (byte innerIndex = 0; innerIndex < index; innerIndex++) {
        //                innerDateNum %= DHHexNum[innerIndex];
        //            }
        //            if (innerDateNum != 0) {
        //                dateNums[index] = System.Convert.ToByte(innerDateNum / DHHexNum[index]);
        //            }
        //        }

        //        try {
        //            //大华起始时间为2000年初始时间;
        //            dt = new DateTime(dateNums[0] + 2000, dateNums[1], dateNums[2], dateNums[3], dateNums[4], dateNums[5]);
        //            return dt;
        //        }
        //        //若时间构造失败;
        //        catch {
        //            return null;
        //        }
        //    }
        //    #endregion

        //    private static bool IsHeadSame(byte[] arr1,byte[] arr2,int len,int arr1Start = 0,int arr2Start = 0) {
        //        var min = Math.Min(arr1.Length - arr1Start, arr2.Length - arr2Start);
        //        if(min < 0) {
        //            return false;
        //        }

        //        var index = 0;
        //        while(index < min) {
        //            if(arr1[index + arr1Start] != arr2[index + arr2Start]) {
        //                return false;
        //            }
        //            index++;
        //        }
        //        return true;
        //    }
        //    public static RegularFile BinarySearchFile(List<RegularFile> arr, int low, int high, long key) {
        //        int mid = (low + high) / 2;
        //        if (low > high)
        //            return null;
        //        else {
        //            if (GetDeviceStartLBA(arr[mid]) == key)
        //                return arr[mid];
        //            else if (arr[mid].StartLBA > key)
        //                return BinarySearchFile(arr, low, mid - 1, key);
        //            else
        //                return BinarySearchFile(arr, mid + 1, high, key);
        //        }
        //    }
        //    private static long GetDeviceStartLBA(RegularFile file) {
        //        if (file.GetParent<Partition>() != null) {
        //            var part = file.GetParent<Partition>();
        //            return part.StartLBA + file.StartLBA;
        //        }
        //        else {
        //            return file.StartLBA;
        //        }
        //    }

        //    private static void TraveringAddFile(CDFC.Parse.Abstracts.Directory dir,List<RegularFile> files) {
        //        dir.Children.ForEach(p => {
        //            if(p is CDFC.Parse.Abstracts.Directory && !IIterableHelper.IsBackUpFile(p as IIterableFile) && !IIterableHelper.IsBackFile(p as IIterableFile)) {
        //                TraveringAddFile(p as CDFC.Parse.Abstracts.Directory,files);
        //            }
        //            else if(p is RegularFile){
        //                files.Add(p as RegularFile);
        //            }
        //        });
        //    }
        //}

    }
}
