using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class InterfaceSegregation
    {
        public class Document
        {

        }

        /*
         * Interface segregation principle - to avoid exposing methods not needed by clients
         * 
         * 
        public interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultiFunctionPrinter : IMachine
        {
            void IMachine.Fax(Document d)
            {
                //
            }

            void IMachine.Print(Document d)
            {
                //
            }

            void IMachine.Scan(Document d)
            {
                //
            }
        }

        */

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public interface IMultiFunctionDevice : IPrinter, IScanner
        {

        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {
            void IPrinter.Print(Document d)
            {
                throw new NotImplementedException();
            }

            void IScanner.Scan(Document d)
            {
                throw new NotImplementedException();
            }
        }


        public class oldFashionPrinter : IPrinter
        {
            void IPrinter.Print(Document d)
            {
                throw new NotImplementedException();
            }
        }

        public static void Main(string[] args)
        {

        }
    }
}
