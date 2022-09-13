using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Client
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");
                tcpclnt.Connect("192.168.0.178", 8001);         
                Console.WriteLine("Connected");
                Console.Write("Enter Username : ");
                String username = Console.ReadLine();
                Console.Write("Enter Password : ");
                String password = Console.ReadLine();                
                var Packet = new Packet
                {
                    user = username,
                    pass = password
                };
                string str = JsonSerializer.Serialize(Packet);
                Console.WriteLine();
                Console.WriteLine("Data yang dikirim Ke Server : ");
                Console.WriteLine(str);                
                Console.WriteLine();                
                Stream stm = tcpclnt.GetStream();
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");
                stm.Write(ba, 0, ba.Length);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                tcpclnt.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
            Console.ReadKey();
        }
    }
    public class Packet
    {
        public string user { get; set; }
        public string pass { get; set; }
    }
}
