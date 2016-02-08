using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash.Test
{
    [TestClass]
    public class HasherTests
    {
        string TempFile;

        [TestInitialize]
        public void Initialize() {
            TempFile = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup() {
            if (File.Exists(TempFile))
                File.Delete(TempFile);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void FromFile_UnknownHashType() {
            new Hasher().FromFile(TempFile, HashType.Md5 + 100);
        }

        [TestMethod, ExpectedException(typeof(FileNotFoundException))]
        public void FromFile_InvalidFile_Md5() {
            Cleanup();
            new Hasher().FromFile(TempFile, HashType.Md5);
        }
        
        [TestMethod, ExpectedException(typeof(FileNotFoundException))]
        public void FromFile_InvalidFile_Sha1() {
            Cleanup();
            new Hasher().FromFile(TempFile, HashType.Sha1);
        }

        [TestMethod, ExpectedException(typeof(FileNotFoundException))]
        public void FromFile_InvalidFile_Sha256() {
            Cleanup();
            new Hasher().FromFile(TempFile, HashType.Sha256);
        }

        [TestMethod]
        public void FromFile_EmptyFile_Md5() {
            var hasher = new Hasher();

            var hash1 = hasher.FromFile(TempFile, HashType.Md5);
            var hash2 = hasher.FromFile(TempFile, new MD5CryptoServiceProvider());

            Assert.IsTrue(hash1 == "d41d8cd98f00b204e9800998ecf8427e");
            Assert.IsTrue(hash2 == "d41d8cd98f00b204e9800998ecf8427e");
        }

        [TestMethod]
        public void FromFile_EmptyFile_Sha1() {
            var hasher = new Hasher();

            var hash1 = hasher.FromFile(TempFile, HashType.Sha1);
            var hash2 = hasher.FromFile(TempFile, new SHA1CryptoServiceProvider());

            Assert.IsTrue(hash1 == "da39a3ee5e6b4b0d3255bfef95601890afd80709");
            Assert.IsTrue(hash2 == "da39a3ee5e6b4b0d3255bfef95601890afd80709");
        }

        [TestMethod]
        public void FromFile_EmptyFile_Sha256() {
            var hasher = new Hasher();

            var hash1 = hasher.FromFile(TempFile, HashType.Sha256);
            var hash2 = hasher.FromFile(TempFile, new SHA256CryptoServiceProvider());

            Assert.IsTrue(hash1 == "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855");
            Assert.IsTrue(hash2 == "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855");
        }
    }
}