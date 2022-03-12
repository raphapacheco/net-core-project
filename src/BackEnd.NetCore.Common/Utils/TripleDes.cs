using BackEnd.NetCore.Common.DataContracts;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.NetCore.Common.Utils
{
    public static class TripleDes
    {
        public static string Encrypt(byte[] key, string info)
        {
            var des = CreateTripleDES(key);
            des.Mode = CipherMode.ECB;
            var ct = des.CreateEncryptor();
            var input = Encoding.UTF8.GetBytes(info);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Convert.ToBase64String(output);
        }

        public static string Decrypt(byte[] key, string info)
        {
            var des = CreateTripleDES(key);
            des.Mode = CipherMode.ECB;
            var ct = des.CreateDecryptor();            
            var input = Convert.FromBase64String(info);
            var output = ct.TransformFinalBlock(input, 0, input.Length);
            return Encoding.UTF8.GetString(output);
        }

        private static TripleDES CreateTripleDES(byte[] key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            var desKey = md5.ComputeHash(key);
            des.Key = desKey;
            des.IV = new byte[8];
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            return des;
        }

        public static UsuarioAuth DecryptAutenticacaoRequest(byte[] key, AutenticacaoRequest request)        
        {
            var userDecrypt = Decrypt(key, request.Usuario);
            UsuarioAuth usuario = JsonConvert.DeserializeObject<UsuarioAuth>(userDecrypt);

            return usuario;
        }
    }
}
