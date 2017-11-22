using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StExt4GroupDesc {
        public uint bg_block_bitmap_lo;              /* Blocks bitmap block */
        public uint bg_inode_bitmap_lo;              /* Inodes bitmap block */
        public uint bg_inode_table_lo;               /* Inodes table block */
        public ushort bg_free_blocks_count_lo;     /* Free blocks count */
        public ushort bg_free_inodes_count_lo;     /* Free inodes count */
        public ushort bg_used_dirs_count_lo;           /* Directories count */
        public ushort bg_flags;                        /* EXT4_BG_flags (INODE_UNINIT, etc) */
        public uint bg_exclude_bitmap_lo;            /* Exclude bitmap for snapshots */
        public ushort bg_block_bitmap_csum_lo;     /* crc32c(s_uuid+grp_num+bbitmap) LE */
        public ushort bg_inode_bitmap_csum_lo;     /* crc32c(s_uuid+grp_num+ibitmap) LE */
        public ushort bg_itable_unused_lo;         /* Unused inodes count */
        public ushort bg_checksum;                 /* crc16(sb_uuid+group+desc) */
        //#if 0	//下面的32位是给64位预留的,只有当64位标记可用和超级块中s_desc_size > 32才能到下面的
        //	uint bg_block_bitmap_hi;				/* Blocks bitmap block MSB */
        //	uint bg_inode_bitmap_hi;				/* Inodes bitmap block MSB */
        //	uint bg_inode_table_hi;				/* Inodes table block MSB */
        //	ushort bg_free_blocks_count_hi;		/* Free blocks count MSB */
        //	ushort bg_free_inodes_count_hi;		/* Free inodes count MSB */
        //	ushort bg_used_dirs_count_hi;			/* Directories count MSB */
        //	ushort bg_itable_unused_hi;			/* Unused inodes count MSB */
        //	uint bg_exclude_bitmap_hi;			/* Exclude bitmap block MSB */
        //	ushort bg_block_bitmap_csum_hi;		/* crc32c(s_uuid+grp_num+bbitmap) BE */
        //	ushort bg_inode_bitmap_csum_hi;		/* crc32c(s_uuid+grp_num+ibitmap) BE */
        //	uint bg_reserved;
        //#endif
        public IntPtr Next;    //Ext4GroupDescTag* next;
        public IntPtr Pre; //Ext4GroupDescTag* pre;
    }
}
