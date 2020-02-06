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
            Console.ReadKey();
            target = Console.ReadLine();
            source = Console.ReadLine();

            foreach (string file in GetFiles(@"D:\Documents\Coreen"))
            {
                Console.WriteLine(file);
            }
            WriteFile(source, target);

            File.WriteAllText(File.ReadLines(Environment.CurrentDirectory + @"\Log.txt").First() + @".txt", "Bonjour");
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
            // Example #1: Write an array of strings to a file.
            // Create a string array that consists of three lines.
            string[] lines = { src, tgt };
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // and then closes the file.  You do NOT need to call Flush() or Close().
            File.WriteAllLines(Environment.CurrentDirectory + @"\Log.txt", lines);
        }
    }
}
