using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class OpenClosed
    {
        public enum Color {
            Red, Green, Blue
        }

        public enum Size {
            Small, Medium, Large
        }

        public class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                Name = name;
                Color = color;
                Size = size;
            }
        }

        /*
         * Open-closed principle
         * assume there are new requirements for filter, you would have to edit the ProductFilter but might be already shipped to customers
         * 
        public class ProductFilter { 
            
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (var p in products)
                {
                    if (p.Size == size)
                    {
                        yield return p;
                    }
                }
            }
            
            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var p in products)
                {
                    if (p.Color == color)
                    {
                        yield return p;
                    }
                }
            }
        }
        */


        // Enterprise pattern

        // Open-closed principle - part of the system should be open for extension but closed for modification
        // in this case: ProductFilter should not be modified
        // and to add functionalities we make new Specification class which will be passed to ProductFilter

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }
            public bool IsSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> first, second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first;
                this.second = second;
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }


        public class ProductFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach(var i in items)
                {
                    if (spec.IsSatisfied(i))
                    {
                        yield return i;
                    }
                }
            }
        }


        public static void Main(string[] args)
        {
            var apple = new Product("apple", Color.Green, Size.Small);
            var tree = new Product("tree", Color.Green, Size.Large);
            var house = new Product("house", Color.Blue, Size.Large);

            List<Product> products = new List<Product> { apple, tree, house };

            /*
             * the non open-closed way of filtering products
             * 
             * 

            var pf = new ProductFilter();
            Console.WriteLine("Green products: ");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            */

            var pf = new ProductFilter();
            Console.WriteLine("Green products");
            foreach (var p in pf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }

            Console.WriteLine("Large products");
            foreach (var p in pf.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine($" - {p.Name} is large");
            }

            Console.WriteLine("Blue medium products");
            foreach (var p in pf.Filter(products, new AndSpecification<Product>(
                new ColorSpecification(Color.Blue),
                new SizeSpecification(Size.Large)
                )))
            {
                Console.WriteLine($" - {p.Name} is blue and large");
            }
        }
    }
}
