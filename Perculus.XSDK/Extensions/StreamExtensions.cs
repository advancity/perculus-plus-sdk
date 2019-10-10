using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Perculus.XSDK.Extensions
{
    public static class StreamExtensions
    {
        public static Stream DeepClone(this Stream input, bool close = true)
        {
            const int readSize = 256;
            byte[] buffer = new byte[readSize];
            MemoryStream ms = new MemoryStream();
            if (input != null && input.CanRead)
            {
                int count = input.Read(buffer, 0, readSize);
                while (count > 0)
                {
                    ms.Write(buffer, 0, count);
                    count = input.Read(buffer, 0, readSize);
                }
                ms.Position = 0;
                if (close)
                    input.Close();
            }
            return ms;
        }

    }
}
