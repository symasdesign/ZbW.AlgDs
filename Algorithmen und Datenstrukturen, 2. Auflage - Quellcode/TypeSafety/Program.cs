using System;
using My.Collections;

namespace TypeSafety
{
    class Program
    {
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        static void Main(string[] args)
        {
            LinkedList<Person> list = new LinkedList<Person>();

            list.AddRange(new Person[] { 
                            new Person() {FirstName = "Hans", LastName = "Muster" },
                            new Person() {FirstName = "Peter", LastName = "Schmidt" },
                            new Person() {FirstName = "Berta", LastName = "Müller" },
                            new Person() {FirstName = "Hermann", LastName = "Schulze" },
            });

            foreach (Person p in list)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName);
                if (p.LastName.StartsWith("M"))
                    list.Remove(p);
            }
        }
    }
}
