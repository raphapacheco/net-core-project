using System.Text;

namespace BackEnd.NetCore.Common.Utils
{
    public static class Secret
    {
        public const string Value = "ef53365136c77e4c1aec1ff764554086";

        public static byte[] GetSecretAsByteArray() => Encoding.ASCII.GetBytes(Value);
    }
}
