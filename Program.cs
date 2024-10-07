using System;

namespace PortScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escáner de Puertos");
            Console.Write("Ingrese la dirección IP o nombre del host: ");
            string host = Console.ReadLine();

            Console.Write("Ingrese el rango de puertos (ej. 1-1000): ");
            string portRange = Console.ReadLine();

            Scanner scanner = new Scanner(host, portRange);
            scanner.StartScanning();
        }
    }
}
