using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerApp
{
    public interface IFileService
    {
        void SavePicture(string name, Stream data, string location = "temp");
        string GetPictureFromDisk(string imagename, string location);

        void DownloadFile(string url, string folder);
       
    }

    public interface IDownloader
    {
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }

    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
