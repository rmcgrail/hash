using System;
using System.IO;

namespace Hash
{
    internal class Output
    {
        public void Hash(string filePath, string fileHash, bool fileFirst) {
            filePath = Path.GetFileName(filePath);
            Console.WriteLine(fileFirst ? $"{filePath}\t{fileHash}" : $"{fileHash}\t{filePath}");
        }

        public void Help(string message) {
            Console.WriteLine();
            Console.WriteLine(message);
            Help();
        }
        public void Help() {
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("\tHash.exe --sha256 filename");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("\t--md5, --sha1, --sha256: sets hash type.");
            Console.WriteLine("\t-ff, --filefirst: swaps output format from hash first to file first.");
            Console.WriteLine("\t-h, --help, /?: shows this information.");
            Console.WriteLine();
        }

        public void Error(string message, Exception ex) {
#if !DEBUG
            Error($"{message}; {ex.Message}");
#else
            Error($"{message}; {ex.ToString()}");
#endif
        }
        public void Error(string message) {
            Console.WriteLine();
            Console.WriteLine($"[ ERROR ] {message}");
            Console.WriteLine();
        }
    }
}