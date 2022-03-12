using System.Text;

namespace BackEnd.NetCore.Common.Utils
{
    public static class Secret
    {
        public const string Value = "ef53365136c77e4c1aec1ff764554086";   
        
        public const string FrontendValue = "fc458a9a9d6b612401215f1e347d98ddff6e14c4";        

        public static byte[] GetSecretAsByteArray() => Encoding.ASCII.GetBytes(Value);

        public static byte[] GetFrontendSecretAsByteArray() => Encoding.ASCII.GetBytes(FrontendValue);
    }
}
