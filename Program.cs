using Robots_ConsoleApp1;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

namespace MachineOrHuman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\data.txt");

            // Create single instance of sample data from first line of dataset for model input
            var imageBytes = File.ReadAllBytes(path);
            Robots.ModelInput sampleData = new Robots.ModelInput()
            {
                ImageSource = imageBytes,
            };


            var result = Robots_ConsoleApp1.Robots.Predict(new Robots_ConsoleApp1.Robots.ModelInput
            {
                ImageSource = imageBytes,
            });            

            // Make a single prediction on the sample data and print results.
            var sortedScoresWithLabel = Robots.PredictAllLabels(sampleData);
            Console.WriteLine($"{"Class",-40}{"Score",-20}");
            Console.WriteLine($"{"-----",-40}{"-----",-20}");


            if (result.Score.Max() < 80)
            {
                Console.WriteLine("Uknow data");
            }
            else
            {
                foreach (var score in sortedScoresWithLabel)
                {
                    Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
                }
            }
            Console.ReadKey();
        }
    }
}
