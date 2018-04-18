﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hex {
    //十六进制服务提供器;
    public interface IHexService {
        /// <summary>
        /// 寻找下一个标识;
        /// </summary>
        /// <param name="hex">作用目标</param>
        /// <param name="findBytes"></param>
        /// <param name="findMethod"></param>
        /// <param name="isBlockSearch"></param>
        /// <param name="blockSize"></param>
        /// <param name="blockOffset"></param>
        void FindNextBytes(IHexDataContext hex,byte[] findBytes, 
            FindMethod findMethod, bool isBlockSearch, int blockSize, int blockOffset);

        void FindNextString(IHexDataContext hex,string findString);
        void FindNextString(IHexDataContext hex,string findString, bool isBlockSearch, int blockSize, int blockOffset);
        void FindNextBytes(IHexDataContext hex,byte[] findBytes);
        void FindNextBytes(IHexDataContext hex,byte[] findBytes, FindMethod method);

        IHexDataContext CreateNewHexDataContext(Stream stream);
    }
}