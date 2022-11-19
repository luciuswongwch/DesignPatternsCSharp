using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class LiskovSubstitution
    {
        /*
         * Rectangle is a shape with independently adjustable height and width
         * Square is a shape without independently adjustable height and width
         * Mathematically, rectangle is a more 'generic' version of square,
         * but in programming term, rectangle should not be considered as a 'base' of square
         * 
         * Notable problem of square inheriting from rectangle is it will break the test case of rectangle
         * e.g. increasing width of rectangle by 11% will increase area by 11%
         * e.g. setting height of rectangle will leave width unaffected
         * test cases will break if square is casted to rectangle and passed to test cases
         */
        
        public interface IShape
        {
            int Area();
        }

        public class Rectangle : IShape
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public Rectangle()
            {

            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}, {Height}";
            }

            public int Area()
            {
                return Width * Height;
            }
        }

        public class Square : IShape
        {
            public int Length { get; set; }

            public override string ToString()
            {
                return $"{nameof(Length)}: {Length}";
            }

            public int Area()
            {
                return Length * Length;
            }
        }

        public static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {rc.Area()}");

            Square sq = new Square();
            sq.Length = 4;
            Console.WriteLine($"{sq} has area {sq.Area()}");
        }
    }
}
