using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CRC.Source.Models;

namespace CRC.Source.Service
{
    public static class MD5Hash
    {
        public static string calculate(byte[] buffer)
        {
            var md5 = MD5.Create();
            byte[] hashenc = md5.ComputeHash(buffer);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }

}