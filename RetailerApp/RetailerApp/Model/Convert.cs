using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RetailerApp.Model
{
    public static class Convert
    {
        public static byte[] ToByteArray(string resource)
        {
            var assembly = typeof(Convert).GetTypeInfo().Assembly;
            byte[] buffer = null;

            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream != null)
                {
                    var length = stream.Length;
                    buffer = new byte[length];
                    stream.Read(buffer, 0, (int)length);
                }
            }

            return buffer;
        }
    }
}
