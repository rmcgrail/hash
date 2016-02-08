using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hash.Test
{
    [TestClass]
    public class ProcessArgsTests
    {
        private const string FAKE_PATH = "A file path";

        [TestInitialize]
        public void Initialize() {
            Program.FilePath = "";
            Program.ShowHelp = false;
            Program.FileFirst = false;
            Program.HashType = HashType.Sha1;
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void ProcessArgs_NoArguments() {
            Program.ProcessArgs(new string[] { });
        }

        /// <remarks> This should be impossible, but just in case for code coverage... </remarks>
        [TestMethod]
        public void ProcessArgs_Integration_EmptyArgs() {
            Program.ProcessArgs(new string[] { "", " ", " \t ", "\r\n" });
        }

        [TestMethod]
        public void ProcessArgs_Integration_FileOnly_FilePathDefault() {
            Program.ProcessArgs(new string[] { FAKE_PATH });
            Assert.IsTrue(Program.FilePath == FAKE_PATH);
        }

        [TestMethod]
        public void ProcessArgs_Integration_ChangeAll_Short() {
            Program.ProcessEachArg(new string[] { FAKE_PATH, "-h", "-ff", "--md5" });

            Assert.IsTrue(Program.ShowHelp == true);
            Assert.IsTrue(Program.FileFirst == true);
            Assert.IsTrue(Program.FilePath == FAKE_PATH);
            Assert.IsTrue(Program.HashType == HashType.Md5);
        }

        [TestMethod]
        public void ProcessEachArg_Integration_ChangeAll_Long() {
            Program.ProcessEachArg(new string[] { FAKE_PATH, "--help", "--filefirst", "--sha1" });

            Assert.IsTrue(Program.ShowHelp == true);
            Assert.IsTrue(Program.FileFirst == true);
            Assert.IsTrue(Program.FilePath == FAKE_PATH);
            Assert.IsTrue(Program.HashType == HashType.Sha1);
        }

        [TestMethod]
        public void ProcessSetting_HashType() {
            Program.ProcessSetting("--sha256");
            Assert.IsTrue(Program.HashType == HashType.Sha256);
        }

        [TestMethod]
        public void ProcessSetting_ShowHelp() {
            Program.ProcessSetting("/?");
            Assert.IsTrue(Program.ShowHelp == true);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void ProcessSetting_InvalidSetting() {
            Program.ProcessSetting("--blarg");
        }
    }
}