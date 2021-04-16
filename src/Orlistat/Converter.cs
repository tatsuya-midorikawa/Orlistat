using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Orlistat
{
    public static class Converter
    {
        public static byte[] ToBytes(this Stream stream)
        {
            if (stream == null)
                return Array.Empty<byte>();

            stream.Position = 0;
            var workspace = new byte[stream.Length];
            stream.Read(workspace, 0, workspace.Length);
            return workspace;
        }

        public static async ValueTask<byte[]> ToBytesAsync(this Stream stream)
        {
            if (stream == null)
                return Array.Empty<byte>();

            stream.Position = 0;
            var workspace = new byte[stream.Length];
            await stream.ReadAsync(workspace, 0, workspace.Length);
            return workspace;
        }

        public static Stream ToStream(byte[] data)
        {
            return new MemoryStream(data);
        }

        public static async ValueTask<Stream> ToStreamAsync(this byte[] data)
        {
            var stream = new MemoryStream();
            await stream.WriteAsync(data, 0 , data.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
