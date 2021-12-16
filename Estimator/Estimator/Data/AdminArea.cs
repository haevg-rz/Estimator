using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Estimator.Data
{
    public class AdminArea
    {
        private readonly string passwordHash = "de092681b408dd6bdda7c02df582d652c8ff0b52241d697fefa347e1b5135af0";

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