using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace QList.Other
{
    public static class QListCompresser
    {
        public static void SaveCompressedQList(string path, string qlstContent)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal, leaveOpen: false))
            using (StreamWriter writer = new StreamWriter(gzipStream, Encoding.UTF8))
            {
                writer.Write(qlstContent);
            }
        }
        public static string LoadCompressedQList(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress, leaveOpen: false))
            using (StreamReader reader = new StreamReader(gzipStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
