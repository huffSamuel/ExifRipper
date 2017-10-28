using ExifRipper.Domain.Exif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.ApplicationHost
{
    public class ExifRipper
    {
        public List<ExifData> FileData
        {
            get;
            private set;
        }

        public ExifRipper()
        {
            FileData = new List<ExifData>();
        }

        public void GetExifData(string filePath)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            using (Image image = new Bitmap(filePath))
            {
                FileData.Add(new ExifData(fileName, image));
            }
        }

        public void GetExifData(List<string> filePaths)
        {
            foreach(var filePath in filePaths)
            {
                GetExifData(filePath);
            }
        }
    }
}
