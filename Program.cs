using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkStructure
{
    public class Program
    {
        static string[] filesToConvert = Directory.GetFiles(Environment.CurrentDirectory);
        string[] stripped = filesToConvert;
        int dateCharNo;
        int digits;
        static void Main(string[] args)
        {
            var n = new Program();
            Console.WriteLine(filesToConvert.Length.ToString() + " files in directory \nEnter character that the date starts from: ");
            n.strip();
        }



        void strip()
        {
            int i = 0;
            foreach (string file in filesToConvert)
            {
                int pos = file.LastIndexOf('\\');
                stripped[i] = file.Substring(pos +1);
                Console.WriteLine(stripped[i]);
                i++;
            }
            Console.ReadKey();
        }

        

        void loop()
        {
            foreach (var fileToConvert in filesToConvert)
            {
                char file = fileToConvert[dateCharNo];
                Console.WriteLine(file);
            }
        }

        void setChar()
        {
            try
            {
                dateCharNo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dateCharNo + "th character \nNumber of digits in year: ");
                setDigit();
            }
            catch
            {
                Console.WriteLine("Try again \nEnter character that the date starts from: ");
                setChar();
            }
        }
        void setDigit()
        {
            try
            {
                digits = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Number of digits in year: " + digits);
                loop();
            }
            catch
            {
                Console.WriteLine("Try again \nNumber of digits in year: ");
                setDigit();
            }
        }
    }
}
