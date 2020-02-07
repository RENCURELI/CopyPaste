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
        
        static void Main(string[] args)
        {
            //Path to target folder
            string target;
            //Path to source folder
            string source;
            
            Console.WriteLine("Hello World! \n" +
                "Bienvenue dans le programme de synchronisation");
            
            //Check if specified initialisation file exists
            string curFile = Environment.CurrentDirectory + @"\Ini.txt";
            if (File.Exists(curFile))
            {
                //Save folder paths to specified variables
                target = File.ReadLines(Environment.CurrentDirectory + @"\Ini.txt").Skip(1).Take(1).First();
                source = File.ReadLines(Environment.CurrentDirectory + @"\Ini.txt").First();

                Console.WriteLine("Pour commencer, appuyez sur Enter");
                while (Console.ReadKey().Key != ConsoleKey.N)
                {
                    Console.ReadLine();
                    Console.Clear();
                    //Enter command to select operation
                    Console.WriteLine("Entrez la commande voulue :\n" +
                        "1 = Mettre à jour chemin de dossier \n" +
                        "2 = Copier de la Cible à la Source \n" +
                        "3 = Copier de la Source à la Cible \n" +
                        "4 = Lister contenu de la Source \n" +
                        "5 = Lister contenu de la Cible \n" +
                        "6 = Femer programme");

                    //Read char's ASCII value
                    int value = Console.Read();
                    //Convert value to Char
                    char select = Convert.ToChar(value);
                    Console.ReadLine();

                    //Operation selection
                    switch (select)
                    {
                        case '1':
                            //Update save paths
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
                                Console.WriteLine("Copied : " + file);
                            }
                            break;

                        case '3':
                            //Copy source to target
                            foreach (string file in GetFiles(source))
                            {
                                File.Copy(file, Path.Combine(target, Path.GetFileName(file)), true);
                                Console.WriteLine("Copied : " + file);
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
                            //Exit program
                            Environment.Exit(0);
                            break;
                    }

                
                    Console.WriteLine("Thank you :) \n" +
                    "Voulez vous continuer y / n ?");
                }
            }
            else
            {
                //If file does not exist, enter paths and generate file
                Console.WriteLine("Enter Target path (format C:\\Documents'\'...'\'dossier final)");
                target = Console.ReadLine();

                Console.WriteLine("Enter Source path (format C:\\Documents'\'...'\'dossier final)");
                source = Console.ReadLine();

                WriteFile(source, target);
            }
            
            Console.ReadKey();
        }
        

        static IEnumerable<string> GetFiles(string path)
        {
            //Queue of files in folder
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try //Get exceptions
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
                    //Get paths to files in directory
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    //Output files and paths
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }

        static void WriteFile(string src, string tgt)
        {
            //Generate file and enter path strings
            string[] lines = { src, tgt };
            File.WriteAllLines(Environment.CurrentDirectory + @"\Ini.txt", lines);
        }
    }
}
