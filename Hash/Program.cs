using System;

namespace Hash
{
    class Program
    {
        private static readonly Hasher hash = new Hasher();
        private static readonly Output output = new Output();

        public static bool     ShowHelp  { get; internal set; }
        public static bool     FileFirst { get; internal set; }
        public static string   FilePath  { get; internal set; }
        public static HashType HashType  { get; internal set; }

        static Program() {
            FilePath = "";
            ShowHelp = false;
            FileFirst = false;
            HashType = HashType.Sha1;
        }

        static void Main(string[] args) {
            try {
                ProcessArgs(args);
                if (ShowHelp)
                    output.Help();
                else
                    output.Hash(FilePath, hash.FromFile(FilePath, HashType), FileFirst);
            }
            catch (ApplicationException ex) {
                output.Help(ex.Message);
            }
            catch (Exception ex) {
                output.Error("Unable to complete operation", ex);
            }
        }

        internal static void ProcessArgs(string[] args) {
            switch (args.Length) {
                case 0:
                    throw new ApplicationException("No input parameters.");
                case 1:
                    FilePath = args[0];
                    break;
                default:
                    ProcessEachArg(args);
                    break;
            }
        }

        internal static void ProcessEachArg(string[] args) {
            foreach (var a in args) {
                var arg = a.Trim();
                if (string.IsNullOrWhiteSpace(arg))
                    continue;
                ProcessSetting(arg);
            }
        }

        internal static void ProcessSetting(string arg) {
            switch (arg.ToLower()) {
                case "/?":
                case "-h":
                case "--help":
                    ShowHelp = true;
                    break;
                    
                case "-ff":
                case "--filefirst":
                    FileFirst = true;
                    break;

                case "--md5":
                    HashType = HashType.Md5;
                    break;
                case "--sha1":
                    HashType = HashType.Sha1;
                    break;
                case "--sha256":
                    HashType = HashType.Sha256;
                    break;

                default:
                    if (arg.StartsWith("--") || arg.StartsWith("-") || arg.StartsWith("/"))
                        throw new ApplicationException("Unknown parameter.");
                    FilePath = arg;
                    break;
            }
        }
    }
}