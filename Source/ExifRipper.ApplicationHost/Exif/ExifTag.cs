using ExifRipper.Domain.Exif;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.ApplicationHost.Exif
{
    public class ExifTag : IExifTag
    {
        public int Tag { get; private set; }
        public string Key { get; private set; }
        public ExifType ExifType { get; private set; }
        public string Description { get; private set; }
        public byte[] Value { get; private set; }
        public Type Type { get; private set; }

        public ExifTag(int tag, byte[] value)
        {
            this.Tag = tag;
            this.Value = value;
            SetType();
        }

        public ExifTag(PropertyItem item) : this(item.Id, item.Value)
        {
            
        }

        public object GetValueAndType(out Type valueType)
        {
            valueType = this.Type;
            return null;           
        }

        private void SetType()
        {
            switch(Tag)
            {
                case 1:
                case 3:
                    ExifType = ExifType.Ascii;
                    break;
                case 2:
                case 4:
                    ExifType = ExifType.Rational;
                    break;
                default:
                    ExifType = ExifType.Undefined;
                    break;
            }

            switch(ExifType)
            {
                case ExifType.Ascii:
                case ExifType.Rational:
                    this.Type = typeof(string);
                    break;
                default:
                    this.Type = typeof(object);
                    break;
            }
        }
    }
}
