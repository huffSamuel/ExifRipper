# ExifRipper
Displays EXIF data stored in JPEG images.

![Exif Ripper UI](/img/Data.PNG "Exif Ripper UI")

# Installation
[Download](https://github.com/huffSamuel/ExifRipper/releases/download/v1.0.0/ExifRipper.Installer.msi) the MSI and run it. ExifRipper will be installed to C:\Program Files (x86)\AOBD Apps\. 

# Requirements
.NET Framework 4.6.1

# Use
Launch ExifRipper from its install location. Click the **Browse** button to select the folder you wish to read Exif data from; ExifRipper will look for compatible file types and load the Exif data into the table.

To export the data to a CSV, select **File > Export**, then select where you would like to save the file and your preferred file name. ExifRipper will export the table to a CSV file.

# Features
* Read Exif GPS coordinate tags
* Export tags to CSV

### Proposed Features
* Support for additional Exif tags
* UI Improvements (Remove Windows 98 Theme)
* Remove Caliburn.Micro dependency

# Support
Please submit issues via the issue tracker.

# Contributing
Pull request are welcome. For major changes, please open an issue first to discuss what you would like to change.

# Thanks
Thanks to [Caliburn.Micro](https://github.com/Caliburn-Micro/Caliburn.Micro) for the wonderful MVVM framework. While I intend to remove this dependency, CM makes developing MVVM applications significantly easier.

# License
[MIT](https://github.com/huffSamuel/ExifRipper/blob/master/LICENSE)
