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

            Console.WriteLine("Hello World!");
            string curFile = Environment.CurrentDirectory + @"\Ini.txt";
            if (File.Exists(curFile))
            {
                foreach (string file in GetFiles(File.ReadLines(Environment.CurrentDirectory + @"\Ini.txt").First()))
                {
                    Console.WriteLine(file);
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

            int select = Console.Read();

            switch (select)
            {
                case 1:

                    break;

                case 2:

                    break;

                default:

                    break;
            }

            Console.ReadKey();
            

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
