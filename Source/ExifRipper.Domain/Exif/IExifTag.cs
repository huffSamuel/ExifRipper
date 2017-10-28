using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.Domain.Exif
{
    public interface IExifTag
    {
        object GetValueAndType(out Type valueType);
        int Tag { get; }
        string Key { get; }
        ExifType ExifType { get; }
        Type Type { get; }
        string Description { get; }
        byte[] Value { get; }
    }
}
