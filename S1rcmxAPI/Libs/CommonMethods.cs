using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace S1rcmxAPI.Controllers
    {

    public class CommonMethods 
    {
        public  bool ValidaUUID (string _uid)
        {

            Regex rx = new Regex(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-4[0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$");
            //UUID V4 REGEX
            //^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-4[0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$
            MatchCollection match = rx.Matches(_uid);
            Console.WriteLine(match.Count);
            if (match.Count > 0)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

    
        public string GenUID()
        {
            byte[] randomBytes = new byte[16];
            string csid;

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            randomBytes[6] = (byte)((randomBytes[6] & 0x0F) | 0x40);
            randomBytes[8] = (byte)((randomBytes[8] & 0x3F) | 0x80);

            string uuid = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

            csid = $"{uuid.Substring(0, 8)}-{uuid.Substring(8, 4)}-4{uuid.Substring(13, 3)}-8{uuid.Substring(17, 3)}-{uuid.Substring(20)}";

            return csid;
        }
    }

}