using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    /// <summary>
    /// Common interface for reading structures to/from byte arrays.
    /// </summary>
    public interface IByteArraySerializable {
        /// <summary>
        /// Gets the total number of bytes the structure occupies.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Reads the structure from a byte array.
        /// </summary>
        /// <param name="buffer">The buffer to read from.</param>
        /// <param name="offset">The buffer offset to start reading from.</param>
        /// <returns>The number of bytes read.</returns>
        int ReadFrom(byte[] buffer, int offset);

        /// <summary>
        /// Writes a structure to a byte array.
        /// </summary>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="offset">The buffer offset to start writing at.</param>
        void WriteTo(byte[] buffer, int offset);
    }

    //public static class ByteArrayHelper {
    //    public static int ReadFrom<TStruct>(TStruct st,byte[] buffer, int offset)where TStruct:struct {

    //    }
    //}
}
