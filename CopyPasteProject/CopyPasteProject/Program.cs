using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace CopyPasteProject
{
    
    class Program
    {
        string[] directories = Directory.GetDirectories("D:\\");
        
        static void Main(string[] args)
        {
            string target;
            string source;
            
            Console.WriteLine("Hello World! \n" +
                "Bienvenu dans le programme de synchronisation");
            
            string curFile = Environment.CurrentDirectory + @"\Ini.txt";
            if (File.Exists(curFile))
            {
                target = File.ReadLines(Environment.CurrentDirectory + @"\Ini.txt").Skip(1).Take(1).First();
                source = File.ReadLines(Environment.CurrentDirectory + @"\Ini.txt").First();

                int value = Console.Read();
                char select = Convert.ToChar(value);

                Console.WriteLine(select);

                switch (select)
                {
                    case '1':
                        Console.WriteLine("Enter Target path");
                        target = Console.ReadLine();

                        Console.WriteLine("Enter Source path");
                        source = Console.ReadLine();

                        WriteFile(source, target);
                        break;

                    case '2':
                        //Copy target to source
                        foreach (string file in GetFiles(target))
                        {
                            File.Copy(file, Path.Combine(source, Path.GetFileName(file)), true);
                        }
                        break;

                    case '3':
                        //Copy source to target
                        foreach (string file in GetFiles(source))
                        {
                            File.Copy(file, Path.Combine(target, Path.GetFileName(file)), true);
                        }
                        break;

                    case '4':
                        //List files in source
                        foreach (string file in GetFiles(source))
                        {
                            Console.WriteLine(file);
                        }
                        break;

                    case '5':
                        //List files in target
                        foreach (string file in GetFiles(target))
                        {
                            Console.WriteLine(file);
                        }
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Enter Target path");
                target = Console.ReadLine();

                Console.WriteLine("Enter Source path");
                source = Console.ReadLine();

                WriteFile(source, target);
            }
            Console.ReadKey();
        }
        

        static IEnumerable<string> GetFiles(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }

        static void WriteFile(string src, string tgt)
        {
            string[] lines = { src, tgt };
            File.WriteAllLines(Environment.CurrentDirectory + @"\Ini.txt", lines);
        }
    }
}
