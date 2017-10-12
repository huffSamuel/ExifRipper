using Caliburn.Micro;
using ExifRipper.UI.Domain;
using ExifRipper.UI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace ExifRipper.UI.ViewModels
{
    public class MainViewModel : Screen
    {
        private bool isProcessing;
        public bool IsProcessing
        {
            get { return isProcessing; }
            set
            {
                if(value != isProcessing)
                {
                    isProcessing = value;
                    NotifyOfPropertyChange("IsProcessing");
                }
            }
        }

        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                if(value != folderPath)
                {
                    folderPath = value;
                    // TODO: Separate thread
                    CreateExifData();
                    NotifyOfPropertyChange("FolderPath");
                }
            }
        }

        private CollectionViewSource exifCollectionViewSource;
        public CollectionViewSource ExifCollectionViewSource
        {
            get
            {
                return exifCollectionViewSource;
            }
            set
            {
                exifCollectionViewSource = value;
                NotifyOfPropertyChange("ExifCollectionViewSource");
            }
        }

        public MainViewModel()
        {
            ExifCollectionViewSource = new CollectionViewSource();
            var items = new List<ExifData>();
            ExifCollectionViewSource.Source = items;
           
        }

        public void SelectFolder()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    FolderPath = dialog.SelectedPath;
                }
            }
        }

        public void ExportCSV()
        {
            var sb = new StringBuilder();
            sb.AppendLine("FileName, Latutude, Longitude");
            foreach(var v in ExifCollectionViewSource.Source as List<ExifData>)
            {
                sb.AppendLine(v.FileName + ", " + v.Latitude + ", " + v.Longitude);
            }

            var sfd = new System.Windows.Forms.SaveFileDialog()
            {
                Filter = "CSV File (*.csv)|*.csv",
                AutoUpgradeEnabled = true,
                CreatePrompt = true,
                DefaultExt = "csv",
                InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString(),
            };

            if(sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF32);
            }

        }

        public void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void About()
        {
            if(MessageBox.Show("This application created by Samuel Huff. Visit my GitHub for more useful applications.", "About", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/huffSamuel");
            }
                
        }

        private void CreateExifData()
        {
            IsProcessing = true;

            Task.Factory.StartNew(() =>
            {
                var items = new List<ExifData>();
                var files = Directory.EnumerateFiles(folderPath, "*.jpg");
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    Image image = new Bitmap(file);
                    var props = new ExifData(fileName, image);
                    items.Add(props);
                }
                Execute.OnUIThread(() => ExifCollectionViewSource.Source = items);
                Execute.OnUIThread(() => IsProcessing = false);
            });
        }

        private PropertyItem GetPropertyItem(Image image, int propertyTag)
        {
            PropertyItem item = null;
            try
            {
                item = image.GetPropertyItem(propertyTag);
            }
            catch { }
            return item;
        }


        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}
