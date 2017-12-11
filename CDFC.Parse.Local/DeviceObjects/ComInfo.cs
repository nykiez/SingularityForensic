using System;
using System.Management;

namespace CDFCEntities.DeviceObjects {
    public static class ComInfo {
        private static string localHardID;
        /// <summary>
        /// 获取本地硬件信息;
        /// </summary>
        public static string LocalHardID {
            get {
                if (localHardID == null) {
                    try {
                        localHardID = GetBIOSSerialNumber() + GetCPUSerialNumber();
                    }
                    catch (Exception ex) {
                        EventLogger.RegisterLogger.WriteLine("ComInfo->LocalHardID错误:" + ex.Message);
                        throw ex;
                    }
                }
                return localHardID;
            }
        }
        /// <summary>  
        /// 获取主板序列号  
        /// </summary>  
        /// <returns></returns>  
        public static string GetBIOSSerialNumber() {
            try {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get()) {
                    sBIOSSerialNumber = mo["SerialNumber"].ToString().Trim();
                }
                return sBIOSSerialNumber;
            }
            catch {
                try {
                    return identifier("Win32_BIOS", "Manufacturer")
                        + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                        + identifier("Win32_BIOS", "IdentificationCode")
                        + identifier("Win32_BIOS", "SerialNumber")
                        + identifier("Win32_BIOS", "ReleaseDate")
                        + identifier("Win32_BIOS", "Version");
                }
                catch {
                    throw new PlatformNotSupportedException("获取BIOS序列号错误!");
                }
            }
        }


        /// <summary>  
                /// 获取CPU序列号  
                /// </summary>  
                /// <returns></returns>  
        public static string GetCPUSerialNumber() {
            try {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Processor");
                string sCPUSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get()) {
                    sCPUSerialNumber = mo["ProcessorId"].ToString().Trim();
                }
                return sCPUSerialNumber;
            }
            catch {
                try {
                    string retVal = identifier("Win32_Processor", "UniqueId");
                    if (retVal == "") //If no UniqueID, use ProcessorID
                               {
                        retVal = identifier("Win32_Processor", "ProcessorId");
                        if (retVal == "") //If no ProcessorId, use Name
                                       {
                            retVal = identifier("Win32_Processor", "Name");
                            if (retVal == "") //If no Name, use Manufacturer
                                               {
                                retVal = identifier("Win32_Processor", "Manufacturer");
                            }
                            //Add clock speed for extra security
                            retVal += identifier("Win32_Processor", "MaxClockSpeed");
                        }
                    }
                    return retVal;
                }
                catch {
                    throw new PlatformNotSupportedException("获取CPU错误!");
                }

            }
        }
        private static string identifier(string wmiClass, string wmiProperty) {
            string result = "";
            ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc) {
                //Only get the first one
                if (result == "") {
                    try {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch {
                    }
                }
            }
            return result;
        }

    }
}
