namespace Knapsack
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    using Knapsack.Solvers;
    using Knapsack.Utils;

    public class EntryPoint
    {
        public static void Main()
        {
            /* var input1 = ReadInput("./../../SampleInputs/easy20.txt");
             PrintResults(input1);*/
            var input2 = new KnapsackInput()
            {
                Capacity = 25,
                ExpectedResult = 44,
                Items =
                                     new List<Item>()
                                         {
                                             new Item { Name = "1", Weight = 2, Value = 4 },
                                             new Item { Name = "2", Weight = 4,  Value = 7 },
                                             new Item { Name = "3", Weight = 6, Value = 10 },
                                             new Item { Name = "4", Weight = 8, Value = 13 },
                                             new Item { Name = "5", Weight = 10, Value = 16 },
                                             new Item { Name = "6", Weight = 12, Value = 19 },
                                             new Item { Name = "7", Weight = 14, Value = 22 },
                                             new Item { Name = "8", Weight = 16, Value = 25 }
                                         }
            };
           // PrintResults(input2);

            var input3 = new KnapsackInput()
            {
                Capacity = 10,
                ExpectedResult = 6,
                Items = new List<Item>()
                                         {
                                             new Item() { Name = "1", Value = 40, Weight = 4 },
                                             new Item() { Name = "2", Value = 42, Weight = 7 },
                                             new Item() { Name = "3", Value = 25, Weight = 5 },
                                             new Item() { Name = "4", Value = 12, Weight = 3 }
                                         }
            };
            // PrintResults(input3);

            var input4 = new KnapsackInput()
            {
                Capacity = 16,
                ExpectedResult = 90,
                Items = new List<Item>()
                                             {
                                                 new Item { Name = "1", Value = 40, Weight = 2 },
                                                 new Item { Name = "2", Value = 30, Weight = 5 },
                                                 new Item { Name = "3", Value = 50, Weight = 10 },
                                                 new Item { Name = "4", Value = 10, Weight = 5 }
                                             }
            };
            //  PrintResults(input4);

            var inputManually = new KnapsackInput(29,9,2,3);
           // PrintResults(inputManually);

            var inputRandom = new KnapsackInput();
            inputRandom.GenerateRandomItems();
            inputRandom.PrintAllItems();
            PrintResults(inputRandom);

        }


        public static void PrintResults(KnapsackInput input)
        {
            IList<KnapsackSolver> solvers = new List<KnapsackSolver>()
                                                    {
                                                        new BranchAndBoundSolver(input.Items, input.Capacity),
                                                        new GreedySolver(input.Items, input.Capacity),
                                                        new DynamicProgrammingSolver(input.Items,input.Capacity)
                                                    };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Max Capacity is {0}", input.Capacity);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var solver in solvers)
            {
                Stopwatch sw = new Stopwatch();

                sw.Start();

                var solution = solver.Solve();

                sw.Stop();

                Console.WriteLine(solution);
                Console.WriteLine("Elapsed = {0}\n", sw.Elapsed);
                Console.ReadKey();
            }
        }

        /*  public static KnapsackInput ReadInput(string fileName)
           {
               var input = new KnapsackInput();

               try
               {
                   using (StreamReader reader = new StreamReader(fileName))
                   {
                       int count = int.Parse(reader.ReadLine());
                       for (int i = 0; i < count; i++)
                       {
                           var line = reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                           var item = new Item
                           {
                               Name = line[0],
                               Value = int.Parse(line[1]),
                               Weight = int.Parse(line[2])
                           };

                           input.Items.Add(item);
                       }

                       input.Capacity = int.Parse(reader.ReadLine());
                       input.ExpectedResult = int.Parse(reader.ReadLine());
                   }
               }
               catch (Exception e)
               {
                   Console.WriteLine("The file could not be read:");
                   Console.WriteLine(e.Message);
               }

               return input;
           }*/
    }
}