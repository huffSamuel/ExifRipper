using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExifRipper.ApplicationHost.Tests
{
    [TestClass]
    public class ExifRipperTests
    {
        private const string EXIF_DATA_TAKEN = "4/28/2015 9:53 AM";
        private const string EXIF_DIMENSIONS = "1536 x 2048";
        private const string EXIF_WIDTH = "1536 pixels";

        [TestMethod]
        public void Constructor_ShouldInitializeFileData()
        {
            var ripper = new ExifRipper();
            Assert.IsNotNull(ripper.FileData);
        }

        [TestMethod]
        public void GetExifData_jpegFilePath_FileDataShouldContainOneEntry()
        {
            var expected = 1;

            var ripper = new ExifRipper();
            RipTestFile(ripper);

            var actual = ripper.FileData.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetExifData_ValidFilePath_ShouldHaveValidLatitude()
        {
            var expected = "";

            var ripper = new ExifRipper();
            RipTestFile(ripper);

            //var actual = ripper.FileData[0].GetTag();
        }

        private void RipTestFile(ExifRipper ripper)
        {
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..", "SampleData", "DSC00208.JPG");
            ripper.GetExifData(filePath);
        }
    }
}
