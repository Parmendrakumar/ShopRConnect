using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerApp.Model
{
    public static class Constants
    {
        public const string StorageConnection = "DefaultEndpointsProtocol=https;AccountName=shopr360storage;AccountKey=2zd9sgmh0Al3XKqn/0qsqVujwzrZrpuCUSj1iAKOucB7V1FmqIjdS95g2kmaAAIcmHRk+EYKYd3QvgG/v8YqiQ==;EndpointSuffix=core.windows.net";
        //public const string StorageConnection = "UseDevelopmentStorage=true"; // Uncomment this connection string to use the Azure Storage emulator
        public static string ApplicationURL = @"http://shopr360.azurewebsites.net/";
    }
}
