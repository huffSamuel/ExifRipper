using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.Domain.Exif
{
    public interface IFileExifData
    {
        string FileName { get; }
        IExifTag GetTag(int tag);
        bool TryGetTag(int tag, out IExifTag outTag);
        void AddExifTag(IExifTag tag);
    }
}
