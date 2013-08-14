using System;

namespace FizzBuzz
{
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            var continueChoiceRegex = new Regex("[^Nn]");
            string choice = "Y";
            while (continueChoiceRegex.IsMatch(choice))
            {
                Console.WriteLine("FizzBuzz");

                long start;
                string startIn;
                do
                {
                    Console.Write("Start: ");
                    startIn = Console.ReadLine();
                }
                while (!long.TryParse(startIn, out start));

                long end;
                string endIn;
                do
                {
                    Console.Write("End: ");
                    endIn = Console.ReadLine();
                }
                while (!long.TryParse(endIn, out end));

                DateTime began = DateTime.Now;
                try
                {
                    string fizzBuzz = FizzBuzz(start, end);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                DateTime finished = DateTime.Now;

                TimeSpan elapsed = finished - began;
                Console.WriteLine("Elapsed: {0}", elapsed);

                Console.Write("Repeat? [Y/n]: ");
                choice = Console.ReadKey().KeyChar.ToString();

                Console.WriteLine(Environment.NewLine);
            }
        }

        public static string FizzBuzz(long start, long end)
        {
            return FizzBuzzSavvy(start, end);
        }

        public static string FizzBuzzNaive(long start, long end)
        {
            string result = "";

            for (long i = start; i <= end; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    result += "FizzBuzz";
                }
                else if (i % 3 == 0)
                {
                    result += "Fizz";
                }
                else if (i % 5 == 0)
                {
                    result += "Buzz";
                }
                else
                {
                    result += i;
                }
            }

            return result;
        }
        
        public static string FizzBuzzSavvy(long start, long end)
        {
            const long MaxArraySize = int.MaxValue;
            long count = end - start + 1;
            if (count > MaxArraySize)
            {
                long maxCount = MaxArraySize - 1;
                throw new Exception(string.Format("{0}-{1} ({2}) is too large.  {0}-{3} ({4}) is the maximum.", start, end, count, start + maxCount, MaxArraySize));
            }

            var results = new string[count];

            var actions = new Action<long,long>[15];
            actions[0] = (i, idx) => results[idx] = "FizzBuzz";
            actions[1] = (i, idx) => results[idx] = i.ToString();
            actions[2] = (i, idx) => results[idx] = i.ToString();
            actions[3] = (i, idx) => results[idx] = "Fizz";
            actions[4] = (i, idx) => results[idx] = i.ToString();
            actions[5] = (i, idx) => results[idx] = "Buzz";
            actions[6] = (i, idx) => results[idx] = "Fizz";
            actions[7] = (i, idx) => results[idx] = i.ToString();
            actions[8] = (i, idx) => results[idx] = i.ToString();
            actions[9] = (i, idx) => results[idx] = "Fizz";
            actions[10] = (i, idx) => results[idx] = "Buzz";
            actions[11] = (i, idx) => results[idx] = i.ToString();
            actions[12] = (i, idx) => results[idx] = "Fizz";
            actions[13] = (i, idx) => results[idx] = i.ToString();
            actions[14] = (i, idx) => results[idx] = i.ToString();

            long mod = start % 15;
            long index = 0;
            for (long i = start; i <= end; i++)
            {
                actions[mod](i, index);
                mod += 1;
                if (mod >= 15)
                {
                    mod = 0;
                }
                index++;
            }

            return results.ToString();
        }
    }
}
