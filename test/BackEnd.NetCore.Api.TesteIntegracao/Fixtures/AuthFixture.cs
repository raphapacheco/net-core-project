using System.Collections.Generic;

namespace BackEnd.NetCore.Api.TesteIntegracao.Fixtures
{
    public static class AuthFixture
    {
        public static Dictionary<string, string> GetValidUser()
        {
            return new Dictionary<string, string>
            {
                { "username", "OWNER" },
                { "password", "123456" }
            };
        }

        public static Dictionary<string, string> GetInvalidUser()
        {
            return new Dictionary<string, string>
            {
                { "username", "INVALID" },
                { "password", "invalid" }
            };
        }
    }
}
