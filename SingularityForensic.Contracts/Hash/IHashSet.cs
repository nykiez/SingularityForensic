using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Hash {
    /// <summary>
    /// 哈希集;
    /// </summary>
    public interface IHashSet:IDisposable {
        /// <summary>
        /// 哈希集名称;
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 唯一标识;
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// 哈希器;
        /// </summary>
        IHasher Hasher { get; }

        /// <summary>
        /// 描述;
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 开始编辑,当使用批量操作编辑时,
        /// 内部的处理将在会批量操作时仅进行一次,调用本方法将会提升处理速度;
        /// 在调用编辑操作时,必须先调用本方法;
        /// </summary>
        /// <remarks>当完成编辑后,需调用<see cref="EndEdit"/>以提交更改</remarks>
        void BeginEdit();

        /// <summary>
        /// 终止编辑,当批量操作编辑完成后,提前调用本方法将会提交更改;
        /// </summary>
        void EndEdit();

        /// <summary>
        /// 添加哈希值与名称;调用本方法前,必须先调用<see cref="BeginEdit"/>,否则将会抛出一个运行时异常;
        /// </summary>
        /// <param name="value">哈希值的十六进制表示,不可为空且不能为空引用,且长度必须为<see cref="Hasher"/>中每个哈希值字节长度大小两倍/></param>
        /// <param name="name">哈希值名称,可以为空,但不能为空引用</param>
        void AddHashPair(string name,string value);

        /// <summary>
        /// 清除所有哈希对;
        /// </summary>
        void Clear();

        /// <summary>
        /// 根据值,名称清除某哈希对;否则将会抛出一个运行时异常;
        /// </summary>
        /// <param name="value">哈希值的十六进制表示,不可为空且不能为空引用,且长度必须为<see cref="Hasher"/>中每个哈希值字节长度大小两倍/></param>
        /// <param name="name">哈希值名称,可以为空,但不能为空引用</param>
        void RemoveHashPair(string name,string value);

        /// <summary>
        /// 进行查看操作时,须先调用本方法;
        /// </summary>
        void BeginOpen();

        /// <summary>
        /// 完成查看操作后,须调用本方法释放资源;
        /// </summary>
        void EndOpen();

        /// <summary>
        /// 匹配哈希值;调用本方法前,须先调用<see cref="BeginOpen"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns>返回所有匹配到的哈希名称-值对</returns>
        IEnumerable<IHashPair> FindHashPairs(string value);

        /// <summary>
        /// 所有哈希名称-值;调用本方法前,须先调用<see cref="BeginOpen"/>
        /// </summary>
        IEnumerable<IHashPair> GetAllHashPairs();
    }

    public interface IHashSetFactory {
        /// <summary>
        /// 从本地加载哈希集;
        /// </summary>
        /// <param name="path">本地(索引)路径</param>
        /// <param name="guid">唯一标识</param>
        /// <returns></returns>
        IHashSet LoadFromLocal(string path,string guid,IHasher hasher);

        /// <summary>
        /// 创建哈希集;
        /// </summary>
        /// <param name="path">本地(索引)路径</param>
        /// <param name="guid">唯一标识</param>
        /// <returns></returns>
        IHashSet CreateNew(string path,string guid, IHasher hasher);
    }

    public class HashSetFactory:GenericServiceStaticInstance<IHashSetFactory> {
        public static IHashSet LoadFromLocal(string path, string guid, IHasher hasher) => Current?.LoadFromLocal(path, guid, hasher);
        public static IHashSet CreateNew(string path, string guid, IHasher hasher) => Current?.CreateNew(path,  guid, hasher);
    }
}
