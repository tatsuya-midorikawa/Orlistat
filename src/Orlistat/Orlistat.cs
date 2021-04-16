using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Orlistat
{
    public static class Orlistat
    {
        public static byte[] Compress(byte[] source)
        {
            using var memory = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Compress, true))
            {
                deflate.Write(source, 0, source.Length);
            }
            memory.Position = 0;
            var compressed = new byte[memory.Length];
            memory.Read(compressed, 0, compressed.Length);

            return compressed;
        }

        public static byte[] Compress(Stream source)
        {
            source.Position = 0;
            using var memory = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Compress, true))
            {
                source.CopyTo(deflate, (int)source.Length);
            }
            memory.Position = 0;
            var compressed = new byte[memory.Length];
            memory.Read(compressed, 0, compressed.Length);

            return compressed;
        }

        public static async ValueTask<byte[]> CompressAsync(byte[] source)
        {
            using var memory = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Compress, true))
            {
                await deflate.WriteAsync(source, 0, source.Length);
            }
            memory.Position = 0;
            var compressed = new byte[memory.Length];
            await memory.ReadAsync(compressed, 0, compressed.Length);

            return compressed;
        }

        public static byte[] Decompress(byte[] source)
        {
            using var memory = new MemoryStream(source);
            using var workspace = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Decompress))
            {
                deflate.CopyTo(workspace);
            }

            workspace.Position = 0;
            var decompressed = new byte[workspace.Length];
            workspace.Read(decompressed, 0, decompressed.Length);

            return decompressed;
        }

        public static Stream DecompressAsStream(byte[] source)
        {
            using var memory = new MemoryStream(source);
            var workspace = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Decompress))
            {
                deflate.CopyTo(workspace);
            }

            workspace.Position = 0;
            return workspace;
        }

        public static async ValueTask<byte[]> DecompressAsync(byte[] source)
        {
            using var memory = new MemoryStream(source);
            using var workspace = new MemoryStream();
            using (var deflate = new DeflateStream(memory, CompressionMode.Decompress))
            {
                await deflate.CopyToAsync(workspace);
            }

            workspace.Position = 0;
            var decompressed = new byte[workspace.Length];
            await workspace.ReadAsync(decompressed, 0, decompressed.Length);

            return decompressed;
        }
    }

    public static class OrlistatExtension
    {
        public static byte[] Compress(this string message) => Orlistat.Compress(Encoding.UTF8.GetBytes(message));
        public static async ValueTask<byte[]> CompressAsync(this string message) => await Orlistat.CompressAsync(Encoding.UTF8.GetBytes(message));
        public static string Decompress(this byte[] source) => Encoding.UTF8.GetString(Orlistat.Decompress(source));
        public static async ValueTask<string> DecompressAsync(this byte[] source) => Encoding.UTF8.GetString(await Orlistat.DecompressAsync(source));
    }
}
