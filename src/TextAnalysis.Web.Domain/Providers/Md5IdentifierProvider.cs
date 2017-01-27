using System.Security.Cryptography;
using System.Text;

namespace TextAnalysis.Web.Domain.Providers
{
    public class Md5IdentifierProvider : IUniqueIdentifierProvider
    {
        public string Generate(string key)
        {
            var md5 = MD5.Create();

            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}