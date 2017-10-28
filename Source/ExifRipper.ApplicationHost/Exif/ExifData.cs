using ExifRipper.ApplicationHost.Exif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.Domain.Exif
{
    public class ExifData : IFileExifData
    {
        private Dictionary<int, IExifTag> exifTags;
        public string FileName { get; private set; }

        public ExifData(string fileName, Image image)
        {
            exifTags = new Dictionary<int, IExifTag>();
            FileName = fileName;
            foreach(var tag in image.PropertyItems)
            {
                exifTags.Add(tag.Id, new ExifTag(tag));
            }
        }

        #region IFileExifData Support

        public bool TryGetTag(int tag, out IExifTag outTag)
        {
            outTag = null;
            bool exists = false;

            if (exifTags.ContainsKey(tag))
            {
                outTag = exifTags[tag];
                exists = true;
            }

            return exists;
        }

        public IExifTag GetTag(int tag)
        {
            if (exifTags.ContainsKey(tag))
                return exifTags[tag];

            throw new IndexOutOfRangeException("tag");
        }

        public void AddExifTag(IExifTag tag)
        {
            exifTags.Add(tag.Tag, tag);
        }

        #endregion
    }
}
