using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestgraphAppOnly
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = DoStuff();
            Task.WaitAll(task);
        }

        private static async Task DoStuff()
        {
            var certfile = System.IO.File.OpenRead("C:/Users/ryu/Desktop/Reservationprivate.pfx");
            var certByte = new byte[certfile.Length];
            certfile.Read(certByte, 0, (int)certfile.Length);
            var cert = new X509Certificate2(certByte,"ReservationBot2", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);

            ClientAssertionCertificate cac = new ClientAssertionCertificate("007d0819fbb34ba59ef74807170f6c47", cert);
            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/22d6a1a5-6d2b-4052-ad4d-de7257e09091", false);

            var authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com", cac);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authResult.AccessToken);
            using (var response = await client.GetAsync("https://graph.microsoft.com/v1.0/users"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var x = "";
                }
            }
        }
    }
}
