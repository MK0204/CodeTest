using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    public class Animal
    {
        protected string Name
        {
            set
            {
                Console.Write(value);
            }
        }

        public virtual void Speak()
        {
        }
    }

    public class Dog : Animal
    {
        public Dog()
        {
            Name = "Spot";
        }
        public override void Speak()
        {
            Console.Write(" barked\n");
        }
    }

    public class Cat : Animal
    {
        public Cat()
        {
            Name = "Kit";
        }
        public override void Speak()
        {
            Console.Write(" meowed");
        }
    }
}
