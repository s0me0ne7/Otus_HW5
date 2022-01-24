using System.Dynamic;
using Newtonsoft.Json;
using System.Diagnostics;
namespace Otus_HW5
{
    class Program
    {
        //Мой reflection

        //Время на сериализацию = 464 мс

        //Время на десериализацию = 735 мс

        //JsonConvert

        //Время на сериализацию = 926 мс

        //Время на десериализацию = 967 мс
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            var someClass = new FToSerialize { I1 = 1, I2 = 2, I3 = 3, I4 = 4, I5 = 5 };
            var numberOfAttempts = 1000000;
            string rr = "";
            timer.Start();
            for (int i = 0; i < numberOfAttempts; i++)
            {
                rr = Serializer.SerializeFromObjectToCSV(someClass);
            }
            timer.Stop();
            Console.WriteLine("Мой reflection\n");
            Console.WriteLine($"Время на сериализацию = {timer.Elapsed.Milliseconds} мс\n");

            timer.Start();
            for (int i = 0; i < numberOfAttempts; i++)
            {
                Serializer.DeserializeFromCSVToObject<FToSerialize>(rr);
            }
            timer.Stop();
            Console.WriteLine($"Время на десериализацию = {timer.Elapsed.Milliseconds} мс\n");

            timer.Start();
            for (int i = 0; i < numberOfAttempts; i++)
            {
                rr = JsonConvert.SerializeObject(someClass);
            }
            timer.Stop();
            Console.WriteLine("JsonConvert\n");
            Console.WriteLine($"Время на сериализацию = {timer.Elapsed.Milliseconds} мс\n");

            timer.Start();
            for (int i = 0; i < numberOfAttempts; i++)
            {
                JsonConvert.DeserializeObject<FToSerialize>(rr);
            }
            timer.Stop();
            Console.WriteLine($"Время на десериализацию = {timer.Elapsed.Milliseconds} мс\n");

            Console.ReadKey();
        }
    }

}