using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Estimator.Data
{
    public class AdminArea
    {
        private readonly string passwordHash = "1811c01a84f78ed2880675dddcc2ae2fc7b8eb7fbcc65e00b87771fcd0982b58";

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