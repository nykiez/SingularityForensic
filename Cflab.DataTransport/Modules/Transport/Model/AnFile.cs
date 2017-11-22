using System;
using System.Collections.Generic;
using System.Linq;

namespace Cflab.DataTransport.Modules.Transport.Model
{
	[Serializable]
    public class AnFile : IInfo,IComparable<AnFile>
    {
		/// <summary>
		/// 是否为根目录
		/// </summary>
	    public bool IsRoot { get; set; } = false;

		/// <summary>
		/// 索引号
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// 文件名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 解析出来的路径
		/// </summary>
	    private string mPath;

		/// <summary>
		/// 完整路径
		/// </summary>
		public string Path {
			get => Parent == null ? mPath : $"{Parent.Path}/{Name}";
		    set => mPath = value;
		}

        /// <summary>
        /// 获取解析出来的路径
        /// </summary>
        public string GetAbsPath()
        {
            return mPath;
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

		/// <summary>
		/// 是否为目录
		/// </summary>
		public bool IsDir { get; set; }

		/// <summary>
		/// 是否是硬链接
		/// </summary>
		public bool IsLink { get; set; }

		/// <summary>
		/// 链接目标地址
		/// </summary>
	    public string TargetPath { get; set; }

		/// <summary>
		/// 链接目标
		/// </summary>
		public AnFile Target { get; set; }

		/// <summary>
		/// 修改时间
		/// </summary>
		public long DateModif { get; set; }

		/// <summary>
		/// 父文件索引
		/// </summary>
		public int ParentIndex { get; set; }

		/// <summary>
		/// 子文件索引
		/// </summary>
		public int[] ChildrenIndex { get; set; }

		/// <summary>
		/// 所属目录
		/// </summary>
	    public AnFile Parent { get; set; }

		/// <summary>
		/// 子文件列表
		/// </summary>
	    public List<AnFile> Children { get; set; }

        /// <summary>
        /// 加载子文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="files"></param>
        private static void Load(AnFile file, IReadOnlyList<AnFile> files)
		{
			if (!file.IsRoot)
			{
				file.Parent = files[file.ParentIndex];
			}
			if (file.ChildrenIndex == null)
			{
				return;
			}
            // 按索引加载子文件
			file.Children = file.ChildrenIndex.Select(index => files[index]).ToList();
            // 遍历子文并解析子文件的子文件
			foreach (var anFile in file.Children)
			{
				if (!anFile.IsDir)
				{
					continue;
				}
			    Load(anFile, files);
			}
		}

        /// <summary>
        /// 解析链接文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="root"></param>
        private static void ParseLink(AnFile file, AnFile root)
        {
            if (file == null)
            {
                return;
            }
            var temp = root?.Children?.Find(item => item.Path.StartsWith(file.TargetPath));
            if (temp == null)
            {
                return;
            }
            if (file.TargetPath.Equals(temp.Path))
            {
                file.Children = temp.Children;
            }
            ParseLink(file,temp);
        }

        /// <summary>
        /// 加载子文件并解析link硬链接
        /// </summary>
        /// <param name="file"></param>
        /// <param name="files"></param>
        internal static void Prepare(AnFile file, List<AnFile> files)
        {
            if (file == null)
            {
                return;
            }
            if (files == null)
            {
                return;
            }
            if (files.Count == 0)
            {
                return;
            }
            // 按文件索引顺序排序列表
            files.Sort((letf, right) => letf.CompareTo(right));
            
            // 加载子目录
            Load(file, files);

            // 找出所有link文件
            var links = files.FindAll(item => item.IsLink);
            // 解析link链接
            links.ForEach(link => ParseLink(link, file));           
        }

        /// <summary>
        /// 展开成列表
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public List<AnFile> ToList(bool sort = false)
        {
            var list = new List<AnFile>();
            ForEach(file => list.Add(file));
            if (sort)
            {
                list.Sort((letf, right) => letf.CompareTo(right));
            }
            return list;
        }

        /// <summary>
        /// 遍历所子文件和子文件的子文件
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(Action<AnFile> action)
        {
            // 执行遍历回调
            action?.Invoke(this);
            if (IsLink)
            {
                return;
            }
            // 如果是文件夹类型，遍历子文件
            if (IsDir)
            {
                Children?.ForEach(item => item.ForEach(action));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
	    public int CompareTo(AnFile other)
	    {
		    return Index.CompareTo(other.Index);
	    }
    }
}
