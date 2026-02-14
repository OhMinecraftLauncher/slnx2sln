using Microsoft.VisualStudio.SolutionPersistence.Serializer;

namespace slnx2sln
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: no input files!");
                return 1;
            }
            else
            {
                string input_file = args[0];
                string ex = Path.GetExtension(input_file);
                string filepath_without_e = Path.Combine(Path.GetDirectoryName(input_file) ?? "", Path.GetFileNameWithoutExtension(input_file));

                Console.WriteLine("Input File: " + input_file);
                Console.WriteLine("File Extension: " + ex);

                if (ex == ".sln")
                {
                    Console.WriteLine("Target FilePath: " + filepath_without_e + ".slnx");

                    var solutionModel = SolutionSerializers.SlnFileV12.OpenAsync(input_file, CancellationToken.None).Result;

                    SolutionSerializers.SlnXml.SaveAsync(filepath_without_e + ".slnx", solutionModel, CancellationToken.None).Wait();

                    Console.WriteLine();
                    Console.WriteLine("Successfully Convert .sln to .slnx!");
                }
                else if (ex == ".slnx")
                {
                    Console.WriteLine("Target FilePath: " + filepath_without_e + ".sln");

                    var solutionModel = SolutionSerializers.SlnXml.OpenAsync(input_file, CancellationToken.None).Result;

                    SolutionSerializers.SlnFileV12.SaveAsync(filepath_without_e + ".sln", solutionModel, CancellationToken.None).Wait();

                    Console.WriteLine();
                    Console.WriteLine("Successfully Convert .slnx to .sln!");
                }
                else
                {
                    Console.WriteLine("Error: wrong extension: " + ex);
                    return 2;
                }
            }

            return 0;
        }
    }
}