using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Estimator.Data
{
    public class AdminArea
    {
        private readonly string passwordHash = "7f460e4ca00cebe0a0fc97a69b515a0e54f3c033e2aa82b362b2acaf4b18444e";

        public async Task<bool> IsAdmin(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var hashstring = new SHA256Managed();
            var hash = hashstring.ComputeHash(bytes);

            var hashString = hash.Aggregate(string.Empty, (current, x) => current + string.Format("{0:x2}", x));

            return hashString.Equals(this.passwordHash);
        }
    }
}