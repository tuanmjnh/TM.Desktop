using System;
using System.Security.Cryptography;
using System.Text;

namespace TM.Desktop
{
    public class Encrypt
    {
        //public static string FileMD5(string filePath)
        //{
        //    byte[] retVal;
        //    using (FileStream file = new FileStream(filePath, FileMode.Open))
        //    {
        //        MD5 md5 = new MD5CryptoServiceProvider();
        //        retVal = md5.ComputeHash(file);
        //    }
        //    return retVal.ToHex("x2");
        //}
        //public static Guid ComputeMd5Hash(byte[] bytes)
        //{
        //    byte[] hash;
        //    lock (HashProviderLock)
        //    {
        //        HashProvider = HashProvider ?? new MD5CryptoServiceProvider();
        //        hash = HashProvider.ComputeHash(bytes);
        //    }
        //    return new Guid(hash);
        //}
        public static byte[] computeMd5(string k)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(k));
            md5.Clear();
            //md5.update(keyBytes);
            //return md5.digest();
            return keyBytes;
        }
        public static string md5_hash(byte[] msg)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(msg);
            var hash = new StringBuilder();

            foreach (var b in hashBytes)
                hash.AppendFormat("{0:x2}", b);
            return hash.ToString();
        }
        private byte[] GenerateHash(string Filename)
        {
            byte[] tmpSource;
            byte[] Hash;
            //Create a byte array from source data.
            tmpSource = Encoding.UTF8.GetBytes(Filename);
            Hash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            return Hash;
        }
        public static byte[] MD5Encryption(string ToEncrypt)
        {
            // Create instance of the crypto provider.
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // Create a Byte array to store the encryption to return.
            byte[] hashedbytes;
            // Required UTF8 Encoding used to encode the input value to a usable state.
            System.Text.UTF8Encoding textencoder = new System.Text.UTF8Encoding();
            // let the show begin.
            hashedbytes = md5.ComputeHash(textencoder.GetBytes(ToEncrypt));
            // Destroy objects that aren't needed.
            md5.Clear();
            md5 = null;
            // return the hased bytes to the calling method.
            return hashedbytes;
        }
        public static string CryptoMD5(string password)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(password);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
                s.Append(b.ToString("x2").ToLower());
            return s.ToString();
        }
        public static string CryptoMD5TM(string password)
        {
            string TM = "&trade;";
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(password + TM);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
                s.Append(b.ToString("x2").ToLower());
            return s.ToString();
        }
        static public string EncodeMD5(string str)
        {
            byte[] textByte = System.Text.Encoding.Default.GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5Henler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = md5Henler.ComputeHash(textByte);
                string ret = "";
                foreach (byte a in hash)
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                return ret;
            }
            catch { throw; }
        }
        public static string TMEncode(string str)
        {
            string[] arr = new string[] { "x8c", "q2z", "f2x", "67h", "hf9", "4aj", "llb", "ea2", "wr6", "bvz" };
            string[] chars = new string[] { "(", ")", ".", "," };
            string val = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].ToString() == "0") val += arr[0];
                if (str[i].ToString() == "1") val += arr[1];
                if (str[i].ToString() == "2") val += arr[2];
                if (str[i].ToString() == "3") val += arr[3];
                if (str[i].ToString() == "4") val += arr[4];
                if (str[i].ToString() == "5") val += arr[5];
                if (str[i].ToString() == "6") val += arr[6];
                if (str[i].ToString() == "7") val += arr[7];
                if (str[i].ToString() == "8") val += arr[8];
                if (str[i].ToString() == "9") val += arr[9];
            }
            return val;
        }
        public static string TMDecode(string str)
        {
            string[] arr = new string[] { "x8c", "q2z", "f2x", "67h", "hf9", "4aj", "llb", "ea2", "wr6", "bvz" };
            string val = "";
            for (int i = 0; i < str.Length - 2; i += 3)
                for (int j = 0; j < arr.Length; j++)
                    if (str[i].ToString() + str[i + 1].ToString() + str[i + 2].ToString() == arr[j]) val += j;
            return val;
        }
        public static string SHA256(string str)
        {
            var crypt = new SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public static string Base64Encode(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
        public static string Base64Decode(string base64)
        {
            var base64Bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64Bytes);
        }
    }
}