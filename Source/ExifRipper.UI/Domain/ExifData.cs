using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifRipper.UI.Domain
{
    internal class ExifData
    {
        public ExifData(string fileName, Image image)
        {
            FileName = fileName;
            if (image.PropertyIdList.Contains(2)) SetLatitude(image.GetPropertyItem(2).Value, image.GetPropertyItem(1).Value);
            if (image.PropertyIdList.Contains(4)) SetLongitude(image.GetPropertyItem(4).Value, image.GetPropertyItem(3).Value);
            image.Dispose();
        }

        private void SetLatitude(byte[] value, byte[] latitudeReference)
        {
            var dms = GetDMS(value);

            StringBuilder sb = new StringBuilder();
            sb.Append(dms.Item1).Append("° ").Append(dms.Item2).Append("' ").Append(dms.Item3).Append("\" ").Append(BitConverter.ToChar(latitudeReference, 0));
            Latitude = sb.ToString();
        }

        Tuple<int, int, double> GetDMS(byte[] value)
        {
            List<byte> listValue = value.ToList();
            var dn = listValue.GetRange(0, 4).ToArray();
            var dd = listValue.GetRange(4, 4).ToArray();
            var mn = listValue.GetRange(8, 4).ToArray();
            var md = listValue.GetRange(12, 4).ToArray();
            var sn = listValue.GetRange(16, 4).ToArray();
            var sd = listValue.GetRange(20, 4).ToArray();

            var d = BitConverter.ToInt32(dn, 0) / BitConverter.ToInt32(dd, 0);
            var m = BitConverter.ToInt32(mn, 0) / BitConverter.ToInt32(md, 0);
            double s = (double)BitConverter.ToInt32(sn, 0) / (double)BitConverter.ToInt32(sd, 0);

            return new Tuple<int, int, double>(d, m, s);
        }

        private void SetLongitude(byte[] value, byte[] longitudeReference)
        {
            var dms = GetDMS(value);

            StringBuilder sb = new StringBuilder();
            sb.Append(dms.Item1).Append("° ").Append(dms.Item2).Append("' ").Append(dms.Item3).Append("\" ").Append(BitConverter.ToChar(longitudeReference, 0));
            Longitude = sb.ToString();
        }


        public string FileName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
