using System;
using Xunit;
using Orlistat;
using System.Text;
using System.IO;

namespace Orlistat.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var src = "abc";
            var a = Orlistat.Compress(Encoding.UTF8.GetBytes(src));
            var b = Encoding.UTF8.GetString(Orlistat.Decompress(a));
        }

        [Fact]
        public void Test2()
        {
            var src = "abc";
            using var memory = new MemoryStream(Encoding.UTF8.GetBytes(src));
            var a = Orlistat.Compress(memory);

            using var decompressed = Orlistat.DecompressAsStream(a);
            var work = new byte[decompressed.Length];
            decompressed.Read(work, 0, work.Length);
            var b = Encoding.UTF8.GetString(work);
        }
    }
}
