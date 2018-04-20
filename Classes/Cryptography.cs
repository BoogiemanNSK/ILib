using System;
using System.Security.Cryptography;
using System.Text;

namespace I2P_Project.Classes
{
    class Cryptography
    {
        /// <summary>
        /// Hashing function
        /// </summary>
        public string GetHash(MD5 md5_hash, string input)
        {
            byte[] data = md5_hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder str_builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                str_builder.Append(data[i].ToString("x2"));
            }
            return str_builder.ToString();
        }

        /// <summary>
        /// Compares hashing string with input
        /// </summary>
        public bool VerifyHash(MD5 md5_hash, string input, string hashed)
        {
            string hash_of_input = GetHash(md5_hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if(comparer.Compare(hash_of_input, hashed) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
