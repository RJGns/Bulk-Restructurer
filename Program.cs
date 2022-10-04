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
        static string[] filesToConvert;
        string[] stripped;
        string path;
        string destPath = @"E:\Media\Pictures\Bulk rename\Out";
        static string[] yearFolders = { "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022" };
        int dateCharNo;
        int digits;
        bool thisCentury;
        static void Main(string[] args)
        {
            var n = new Program();
            
            n.setPath();
        }



        void strip()
        {
            int i = 0;
            foreach (string file in filesToConvert)
            {
                int pos = file.LastIndexOf('\\');
                stripped[i] = file.Substring(pos +1);
                
                i++;
            }
            
        }

        string getDate(string fileToConvert)
        {
            string date;
            if (digits == 2)
            {
                char a = fileToConvert[dateCharNo - 1];
                char b = fileToConvert[dateCharNo];

                StringBuilder sb = new StringBuilder();
                sb.Append(a);
                sb.Append(b);
                date = sb.ToString();


            }
            else if (digits == 4)
            {
                char a = fileToConvert[dateCharNo - 1];
                char b = fileToConvert[dateCharNo];
                char c = fileToConvert[dateCharNo + 1];
                char d = fileToConvert[dateCharNo + 2];
                StringBuilder sb = new StringBuilder();
                sb.Append(a);
                sb.Append(b);
                sb.Append(c);
                sb.Append(d);
                date = sb.ToString();
            }
            else date = "F";
            try
            {
                int x;
                if (thisCentury)
                {
                     x = Convert.ToInt32(date) + 2000;
                } else
                {
                     x = Convert.ToInt32(date) + 1000;
                }
                

                return x.ToString();
            }
            catch
            {
                return "Other";
            }
        }
        void checkForFolders()
        {
            if (!Directory.GetDirectories(destPath).Contains("2014"))
            {
                Directory.CreateDirectory(destPath);
                foreach (string year in yearFolders)
                {
                    Directory.CreateDirectory(Path.Combine(destPath,year));
                }
            }
        }
        void loop()
        {
            strip();
            checkForFolders();
            deleteEmpty();
            int i = 0;
            foreach (string fileToConvert in stripped)
            {
                if (digits == 2)
                {
                    if (getDate(fileToConvert) != null)
                    {
                        try
                        {
                            var from = Path.Combine(path, fileToConvert);
                            var to = Path.Combine(Path.Combine(destPath, getDate(fileToConvert)), fileToConvert);
                            File.Move(from, to);
                            Console.WriteLine("Moved");
                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                        i++;
                    }
                }
                else if (digits == 4)
                {
                    if (getDate(fileToConvert) != null)
                    {
                        try
                        {
                            File.Move(fileToConvert, Environment.CurrentDirectory + "\\" + getDate(fileToConvert));
                            Console.WriteLine(fileToConvert + " moved");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                    }
                } else
                {
                    Console.WriteLine("Digits need to be 2 or 4");
                    Console.ReadKey();
                }
            }
            Console.ReadKey();
        }
        void deleteEmpty()
        {
            foreach (var directory in Directory.GetDirectories(destPath))
            {
               
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }
        void setPath()
        {
            Console.WriteLine("Enter images current path: ");
            path = Console.ReadLine();
            
            filesToConvert = Directory.GetFiles(@path);
            stripped = filesToConvert;
            Console.WriteLine("Enter destination path: ");
            destPath = Console.ReadLine();
            
            setChar();
        }
        void setChar()
        {
            try
            {
                Console.WriteLine("Enter character date starts from: ");
                dateCharNo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dateCharNo + "th character \nNumber of digits in year: ");
                Console.WriteLine("Images from at least year 2000? (Y/N Default = Y)");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    thisCentury = true;
                } else if (Console.ReadLine().ToUpper() == "N")
                {
                    thisCentury= false;
                } else
                {
                    thisCentury= true;
                }
                setDigit();
            }
            catch
            {
                Console.WriteLine("Try again");
                setChar();
            }
        }
        void setDigit()
        {
            try
            {
                digits = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Number of digits in year (2/4): " + digits);
                loop();
            }
            catch
            {
                Console.WriteLine("Try again \nNumber of digits in year (2/4): ");
                setDigit();
            }
        }
    }
}
