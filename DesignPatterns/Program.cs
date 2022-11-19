using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Single reponsibility principle");
            SingleResponsibility.Main(args);

            Console.WriteLine("Open-closed principle");
            OpenClosed.Main(args);

            Console.WriteLine("Liskov substitution principle");
            LiskovSubstitution.Main(args);

            Console.WriteLine("Interface segregation principle");
            // Nothing to print from interface segration principle as interface methods are not implemented

            Console.WriteLine("Dependency inversion principle");
            DependencyInversion.Research.Main(args);
        }
    }
}
