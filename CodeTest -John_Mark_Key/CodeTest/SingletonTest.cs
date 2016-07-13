using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    class SingletonTest
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static readonly SingletonTest _instance =
          new SingletonTest();

        private string _name;

        // Construtor is private to prevent direct instantiation
        private SingletonTest()
        {
            // Empty Constructor, since this class cannot be
            // directly instantiated.
        }

        // Per Instructions, exposing Name property with get/set
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        // Method by which this class is avaiable to other classes.
        // Guarantees this will be a Singleton class.
        public static SingletonTest GetSingletonTest()
        {
            return _instance;
        }


    }
}
