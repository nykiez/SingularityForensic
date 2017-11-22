using CDFC.Parse.Android.Structs;

namespace CDFC.Parse.Android.Contracts {
    public interface IExt4Node {

        StDirEntry? StDirEntry { get; }
        StExt4DirEntry? StExt4DirEntry { get; }
        StExt4Inode? StExt4Inode { get;}
    }
    public static class HaveINodeHelper {
        //权限标识;
        public static INodePermission GetPermission(this IExt4Node haveInode) =>
            (INodePermission)(haveInode?.StExt4Inode?.i_mode ?? 0);

        //标识ID;
        public static uint GetUID(this IExt4Node haveInode) =>
            ((uint)(haveInode?.StExt4Inode?.l_i_uid_high ?? 0) << 16) | (haveInode?.StExt4Inode?.i_uid ?? 0);

        public static uint GetGID(this IExt4Node haveInode) =>
            ((uint)(haveInode?.StExt4Inode?.l_i_gid_high ?? 0) << 16) | (haveInode?.StExt4Inode?.i_gid ?? 0);
    }
}
