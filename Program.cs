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
        
        int dateCharNo;
        int digits;
        bool thisCentury = true;
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
            if (fileToConvert.Length <= dateCharNo)
            {
                return "Other";
            }
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
                     x = Convert.ToInt32(date) + 1900;
                }
                

                return x.ToString();
            }
            catch
            {
                return "Other";
            }
        }
        
        void loop()
        {
            strip();
            
            
            int i = 0;
            foreach (string fileToConvert in stripped)
            {
                if (digits == 2)
                {
                    if (getDate(fileToConvert) != null)
                    {
                        var from = Path.Combine(path, fileToConvert);
                        var yearFolder = Path.Combine(destPath, getDate(fileToConvert));
                        var to = Path.Combine(yearFolder, fileToConvert);
                        try
                        {                         
                            if (!Directory.Exists(yearFolder))
                            {
                                Directory.CreateDirectory(yearFolder);
                            }
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
            deleteEmpty();
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
            try
            {
                filesToConvert = Directory.GetFiles(@path);
            }
            catch
            {
                Console.WriteLine("Error with path");
                setPath();
            }
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
                
                Console.WriteLine("Images from at least year 2000? (Y/N Default = Y)");
                string z = Console.ReadLine().ToUpper();
                if (z == "Y")
                {
                    thisCentury = true;
                } else if (z == "N")
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
                Console.WriteLine("Number of digits in year (2/4): ");
                digits = Convert.ToInt32(Console.ReadLine());
                
                loop();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Try again \n" + ex);
                setDigit();
            }
        }
    }
}
