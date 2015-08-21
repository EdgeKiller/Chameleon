using System;
using System.IO;
using System.Text;

namespace Chameleon
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                Console.WriteLine("Usage : Chameleon <script file>");
                Console.WriteLine("<script file> is a relative path to a .ch script to run.");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Missing file : " + args[0]);
                return;
            }

            string contents = readFile(args[0]);

            Chameleon chameleon = new Chameleon();
            chameleon.Interpret(contents);
        }

        private static string readFile(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                try
                {
                    StringBuilder builder = new StringBuilder();
                    char[] buffer = new char[8192];
                    int read;

                    while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        builder.Append(buffer, 0, read);
                    }

                    builder.Append("\n");

                    return builder.ToString();
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
    }
}
