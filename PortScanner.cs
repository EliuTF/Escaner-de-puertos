using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace PortScanner
{
    public class Scanner
    {
        private string _host;
        private int _startPort;
        private int _endPort;

        public Scanner(string host, string portRange)
        {
            _host = host;

            string[] range = portRange.Split('-');
            _startPort = int.Parse(range[0]);
            _endPort = int.Parse(range[1]);
        }

        public void StartScanning()
        {
            Console.WriteLine($"Escaneando { _host } desde el puerto { _startPort } hasta el puerto { _endPort }...");
            List<Task> tasks = new List<Task>();

            for (int port = _startPort; port <= _endPort; port++)
            {
                tasks.Add(Task.Run(() => CheckPort(port)));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Escaneo completo.");
        }

        private void CheckPort(int port)
        {
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    var result = client.BeginConnect(_host, port, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(100));
                    
                    if (success)
                    {
                        Console.WriteLine($"Puerto {port} está abierto.");
                    }
                }
                catch
                {
                    // No hacer nada si ocurre un error (puerto cerrado o no alcanzable)
                }
            }
        }
    }
}
