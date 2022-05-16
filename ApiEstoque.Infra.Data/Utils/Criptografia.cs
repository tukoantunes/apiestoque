using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ApiEstoque.Infra.Data.Utils
{
    public class Criptografia
    {
        public static string Get(string senha, Hash hash)
        {
            byte[] result = null;

            switch (hash)
            {
                case Hash.MD5:
                    result = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(senha));
                    break;

                case Hash.SHA1:
                    result = new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(senha));
                    break;
            }

            //converter o resultado da criptografia para string hexadecimal
            var output = string.Empty;
            foreach (var item in result)
                output += item.ToString("X2"); //X2 -> string hexadecimal

            return output;
        }
    }

    public enum Hash
    {
        MD5, SHA1
    }
}



