using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Canjes.Servicios
{
    public class Utilidad
    {   
        public static string Encriptar(string str)
        {
                SHA256 sha256 = SHA256.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = sha256.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
        }
        

        public static string GenerarToken (){
            string token = "";
            token=Guid.NewGuid().ToString("N");
            return token;
        }

    }
}
