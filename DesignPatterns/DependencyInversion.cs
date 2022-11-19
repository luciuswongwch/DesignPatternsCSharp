using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    internal class DependencyInversion
    {
        public enum Relationship
        {
            Parent, Child, Sibling
        }

        public class Person
        {
            public string Name { get; set; }
        }

        public interface IRelationshipBrowser
        {
            IEnumerable<Person> FindAllChildrenOf(string name);
        }

        // low-level module
        public class Relationships : IRelationshipBrowser
        {
            private List<(Person, Relationship, Person)> relations 
                = new List<(Person, Relationship, Person)>();
        
            public void AddParentAndChild(Person parent, Person child)
            {
                relations.Add((parent, Relationship.Parent, child));
                relations.Add((child, Relationship.Child, parent));
            }

            public IEnumerable<Person> FindAllChildrenOf(string name)
            {
                return relations.Where(
                    x => x.Item1.Name == name && x.Item2 == Relationship.Parent
                ).Select(x => x.Item3);
            }
        }

        // high-low module

        public class Research
        {
            /*
             * instead of depending on low-level module, depends on the abstraction, IRelationshipBrowser in this case
             *
            public Research(Relationships relationships)
            {
                //
            }
            *
            */

            public Research(IRelationshipBrowser browser)
            {
                // assume we look for all childs John has in the constructor for simplicity
                foreach (var p in browser.FindAllChildrenOf("John"))
                    Console.WriteLine($"John has a child called {p.Name}");
            }
            public static void Main(string[] args)
            {
                var parent = new Person { Name = "John" };
                var child1 = new Person { Name = "Chris" };
                var child2 = new Person { Name = "Mary" };

                var relationships = new Relationships();
                relationships.AddParentAndChild(parent, child1);
                relationships.AddParentAndChild(parent, child2);

                new Research(relationships);
            }
        }
    }
}
