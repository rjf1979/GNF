using System.Security.Cryptography;
using System.Text;

namespace GNF.Common.Arithmetic
{
    internal class HashAlgorithm
    {
        public static long Hash(byte[] digest, int nTime)
        {
            long rv = ((long)(digest[3 + nTime * 4] & 0xFF) << 24)
                    | ((long)(digest[2 + nTime * 4] & 0xFF) << 16)
                    | ((long)(digest[1 + nTime * 4] & 0xFF) << 8)
                    | ((long)digest[0 + nTime * 4] & 0xFF);

            return rv & 0xffffffffL; /* Truncate to 32-bits */
        }

        public static byte[] ComputeMd5(string k)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(k));
            md5.Clear();
            return keyBytes;
        }
    }
}
