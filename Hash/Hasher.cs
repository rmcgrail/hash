using System;
using System.IO;
using System.Security.Cryptography;

namespace Hash
{
    internal class Hasher
    {
        public string FromFile(string filePath, HashType type) {
            switch(type) {
                case HashType.Md5:
                    return FromFile(filePath, new MD5CryptoServiceProvider());
                case HashType.Sha1:
                    return FromFile(filePath, new SHA1CryptoServiceProvider());
                case HashType.Sha256:
                    return FromFile(filePath, new SHA256CryptoServiceProvider());
                default:
                    throw new ApplicationException("Unknown hash type.");
            }
        }

        internal string FromFile(string filePath, HashAlgorithm algorithm) {
            byte[] bytes = algorithm.ComputeHash(File.ReadAllBytes(filePath));

            string result = "";
            foreach (var b in bytes) 
                result += $"{b,2:x2}";

            return result;
        }
    }
}