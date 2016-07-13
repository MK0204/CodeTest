using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWarsAPI.Model;
using StarWarsAPI;
using System.Threading.Tasks;

namespace CodeTest
{
    [TestClass]
    public class CodingTests
    {
        /// <summary>
        /// Print to the console the time difference in seconds between the two dates below
        /// </summary>
        [TestMethod]
        public void PrintSecondsBetweenTwoDateTimesTest()
        {
            var dateTime1 = DateTime.Parse("6/1/2016 12:00 AM");
            var dateTime2 = DateTime.Now;

            var diffInSeconds = (dateTime2 - dateTime1).TotalSeconds;

            // Truncating output b/c instructions say print "seconds",
            // but makes no mention of milliseconds, etc.
            Console.WriteLine(Math.Truncate(diffInSeconds));
        }

        /// <summary>
        /// For the numbers 1 to 100 do the following:
        ///     - Print the number
        ///     - If the number is a multiple of 3 then print "Hello"
        ///     - If the number is a multiple of 5 then print "Goodbye"
        ///     - If the number is a multiple of both 3 and 5 then print "Hello and Goodbye"
        /// </summary>
        [TestMethod]
        public void HelloGoodbyeTest()
        {
            string output;

            for (int i = 1; i <= 100; i++)
            {
                output = String.Empty;

                if (i % 3 == 0)
                {
                    output = "Hello";
                }
                if (i % 5 == 0)
                {
                    output += (output.Length > 0) ? " and Goodbye" : "Goodbye";
                }

                if (output.Length > 0)
                    Console.WriteLine(i + ": " + output);
            }
        }
        /// <summary>
        /// Create a method that divides two numbers. 
        /// If there is an exception print an error message that the input is invalid and the input arguments.
        /// Exercise the exception handling scenario.
        /// </summary>
        [TestMethod]
        public void ExceptionTest()
        {
            DivideTwoNumbers(3, 0);
        }
        /// <summary>
        /// Following the above [TestMethod] directions in the strictest sense,
        /// a method has been created below which divides two numbers,
        /// and if an exception occurs it prints the arguments received.
        /// There's no mention of the 2 numbers' data types, so *int* is
        /// being used by default; nor is anything stated about this
        /// created method needing to return a value -- so it is void.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DivideTwoNumbers(int num1, int num2)
        {
            int result = 0;
            try
            {
                result = num1 / num2;
            }
            catch (Exception e) // Exception e is here only for good pratice, but not being used.
            {
                Console.WriteLine("Invalid Input: first argument is {0}, second argument is {1}", num1, num2);
            }
        }

        /// <summary>
        /// Create a base Animal class:
        ///     - Name property
        ///     - method Speak() 
        ///     - non-public but accessible from derived class method for printing the Name which will used before Speak is called
        /// 
        /// Create two derived classes from Animal for Dog and Cat
        ///     - override Speak and print the name before speaking
        ///     
        /// Example:  Dog:  "Spot barked", Cat:  "Kit meowed"
        /// 
        /// </summary>
        [TestMethod]
        public void ObjectOrientedTest()
        {
            Dog dog = new Dog();
            dog.Speak();

            Cat cat = new Cat();
            cat.Speak();
        }

        /// <summary>
        /// Write as a comment the SQL for the following:
        /// 
        /// Return the phone number and product names and purchased date for people whose 
        /// first name is "Jeff" whom have purchased something since 1/1/2016:
        /// 
        /// Assume Tables: 
        /// 
        ///     Persons
        ///         PersonId
        ///         FirstName
        ///         LastName
        ///         PhoneNumber
        ///         
        ///     PurchasedProducts        
        ///         PurchasedProductId
        ///         PersonId
        ///         ProductName
        ///         PurchasedDate
        ///         
        /// </summary>
        [TestMethod]
        public void SqlTest()
        {
            /*
             SELECT dbo.Persons.PhoneNumber, dbo.PurchasedProducts.ProductName,
	                dbo.PurchasedProducts.PurchasedDate
            FROM dbo.Persons
            JOIN dbo.PurchasedProducts
	            ON dbo.Persons.PersonId = dbo.PurchasedProducts.PersonId
            WHERE dbo.Persons.FirstName = 'Jeff'
	            AND dbo.PurchasedProducts.PurchasedDate >= '2016-01-01'
             */
        }

        /// <summary>
        /// Create a method that prints a passcode with the following criteria:
        ///     - 3 characters
        ///     - 1st:  random # 0-9
        ///     - 2nd:  number needs to be greater than 1st (random); if 1st # is 9, then 2nd # is 9
        ///     - 3rd:  alpha character (A-Z) (random)
        /// </summary>
        [TestMethod]
        public void GeneratePasscodeTest()
        {
            Random rand = new Random();

            int first = rand.Next(0, 10);
            int second = (first == 9) ? 9 : rand.Next(first + 1, 10);
            char third = (char)('A' + rand.Next(0, 26));

            string passcode = first.ToString() + second.ToString() + third;

            Console.WriteLine(passcode);
        }

        /// <summary>
        /// Create a C# class that expoes a Name property where the class has only one instance to provide global access.
        /// Hint: the class is a commonly used design pattern.
        /// 
        /// Exercise use of the class:
        ///     - Set the Name property
        ///     - Print the name property to the console
        /// </summary>
        [TestMethod]
        public void DesignPatternTest()
        {
            // Get the singleton class instance
            SingletonTest singletonTest = SingletonTest.GetSingletonTest();
            // Set the Name property
            singletonTest.Name = "John Mark Key";
            // Print the Name property value to the Test console
            Console.WriteLine(singletonTest.Name);
        }

        // ==============================
        // Expert Dev Continuation Tests
        // ==============================

        /// <summary>
        /// Read the data-example-1.csv file into a list of products
        /// - Print the product details for anything with the Color Red.
        /// - Print the average price for products in each category (Parent SKU not set)
        /// </summary>
        [TestMethod]
        public void FileParsingTest()
        {
            List<string> lstRows = new List<string>();  // List containing the .csv rows
            int totalRowCount = 0;              // set to the number of rows in the List
            bool isHeaderRow = true;            // init to true since this will be the first iteration  
            string currCategory = String.Empty; // current category for averaging prices 
            string[] rowDetails;                // details from .csv row when 'Split()'
            decimal categoryTotal = 0;          // totals prices for each product category
            int categoryRowCount = 0;           // used for getting average price, counts rows iterated for product category

            // .csv file kept in same relative path as when Testing Solution received
            StreamReader sr = new StreamReader(@"..\..\data-example-1.csv");
            while (!sr.EndOfStream)
            {
                lstRows.Add(sr.ReadLine());
            }
            sr.Close();

            totalRowCount = lstRows.Count;

            // Per instructions, first print all products that are *red*
            Console.WriteLine("*************** RED PRODUCTS ****************");
            foreach (var row in lstRows)
            {
                rowDetails = row.Split(',');
                if (rowDetails[4].ToLower() == "red")
                {   // Print the details of this red product
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        rowDetails[0], rowDetails[1], rowDetails[2],
                        rowDetails[3], rowDetails[4], rowDetails[5]);
                }
            }

            // Per instructions, next print the average price of each product category
            Console.WriteLine("********** PRODUCT PRICE AVERGAGES **********");
            foreach (var row in lstRows)
            {
                if (isHeaderRow)    // skip the header row
                {
                    isHeaderRow = false;
                }
                else
                {
                    rowDetails = row.Split(',');

                    if (rowDetails[2] == String.Empty) // new product category - Parent SKU field empty
                    {
                        if (!String.IsNullOrEmpty(currCategory))    // check if currCategory previously set
                        {
                            // Print the average for the current product category
                            decimal categoryAverage = categoryTotal / categoryRowCount;
                            Console.Write("\tAverage Product Price: " + categoryAverage + "\n");
                        }

                        currCategory = rowDetails[1];  // store the product category/Parent SKU
                        categoryTotal = 0;              // reset the product category total
                        categoryRowCount = 0;           // reset the row count since this is a new product category/Parent SKU

                        // Print the Product Category row, then print the average after it's calculated
                        Console.Write("{0}\t{1}\t{2}",
                        rowDetails[0], rowDetails[1], rowDetails[5]);
                    }
                    else  // iterating over products in a particular category, so total their prices
                    {
                        categoryRowCount += 1;
                        categoryTotal += Convert.ToDecimal(rowDetails[3]); // add to the category total
                    }
                }
            }

            // process the average of the last product category in the List
            if (categoryRowCount > 0)
            {
                // Print the average for the final product category
                decimal categoryAverage = categoryTotal / categoryRowCount;
                Console.Write("\tAverage Product Price: " + categoryAverage);
            }

        }

        /// <summary>
        /// Create a method to recurse through a given directory looking for files with a certain extension (i.e. *.txt)
        /// and count the words in the file to print out the word count for each file.
        ///     - loading and counting of words for each file should be done in it's own thread
        ///     - display the number of files processed and average file word count 
        /// </summary>
        [TestMethod]
        public void ThreadTest()
        {
            Int32 totalWordCount = 0;
            decimal averageWordCount = 0;
            string[] txtFilesArray = Directory.GetFiles(@"c:\temp\", "*.txt");
            int noOfFiles = txtFilesArray.Length;

            foreach (var txtFile in txtFilesArray)
            {
                var reader = Task.Factory.StartNew(() =>
                {
                    char[] delimiters = { ' ', ',', '.', ';', ':', '-', '_', '/', '\n', '\r', '\u000A' };

                    string words = File.ReadAllText(txtFile);
                    var wordArray = words.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    var wordCount = wordArray.Length;
                    Console.WriteLine("File: {0}, Word Count: {1}", txtFile, wordCount);

                    return wordCount;
                });

                totalWordCount += reader.Result;
            }

            if (noOfFiles > 0)
                averageWordCount = (decimal)totalWordCount / noOfFiles;

            Console.WriteLine("\nNumber of files processed: {0}\nAverage word count: {1}", noOfFiles, averageWordCount.ToString("0.##"));
        }

        /// <summary>
        /// Using the Star Wars REST api:  https://swapi.co/
        /// 
        /// Print the starships where the length > 10, and the pilot names for each starship.
        /// </summary>
        [TestMethod]
        public void ServiceTest()
        {
            var api = new StarWarsAPIClient();
            StarWarsEntityList<Starship> starships = api.GetAllStarship().Result;

            foreach (var starship in starships.results)
            {
                if (Convert.ToSingle(starship.length) > 10)
                {
                    Console.WriteLine(starship.name);

                    foreach (var pilot in starship.pilots)
                    {
                        try
                        {
                            Uri uri = new Uri(pilot);
                            var pilotNumber = uri.Segments[uri.Segments.Length - 1].Replace("/", String.Empty);
                            Console.WriteLine("\t" + api.GetPeopleAsync(pilotNumber).Result.name);
                        } catch (Exception ex)
                        {
                            Console.WriteLine("\tFailed to get pilot name");
                        }
                    }
                }
            }
        }

    }
}
