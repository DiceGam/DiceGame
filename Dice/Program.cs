using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dice
{
    class Program
    {
        public static Dictionary<string, int> dict = new Dictionary<string, int>();
        public static Dictionary<string, int> result = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of players");
            var players = Console.ReadLine();

            Console.WriteLine("Enter number of Turns");
            var turns = Console.ReadLine();

            if (!Int32.TryParse(players, out int playerCount) || !Int32.TryParse(turns, out int turnsCount)) 
            {
                Console.WriteLine("Enter a number");
                return;
            }

            for (int i = 0; i < playerCount; i++) 
            {
                Console.WriteLine("Enter a name");
                var name = Console.ReadLine();
                dict.Add(name, 0);
            }

            for (int x = 0; x < turnsCount; x++) 
            {
                foreach (var player in dict)
                {

                    var points = Game(player.Key);
                    Console.ReadKey(true);

                    if (result.ContainsKey(player.Key))
                    {
                        result[player.Key] += points;
                    }
                    else 
                    {
                        result.Add(player.Key, points);
                    }
                    Console.Clear();
                }
            }

            foreach (var val in result.OrderByDescending(x => x.Value)) 
            {
                Console.Write(val);
            }
            System.Threading.Thread.Sleep(2000);
            while (Console.KeyAvailable)
                Console.ReadKey(true);
            Console.ReadKey(true);
        }


        public static int Game(string playerName) 
        {
            Console.CursorVisible = false;
            var random = new Random();
            int dice = 1;
            Console.Clear();
            Console.WriteLine($"{playerName}'s turn");
            bool stop = false;
            var diceTask = Task.Run(async () =>
            {
                int lastDice = 1;
                while (!stop)
                {
                    while (dice == lastDice)
                        dice = random.Next(1, 7);
                    lastDice = dice;
                    Console.SetCursorPosition(40, 10);
                    Console.Write(dice);
                    await Task.Delay(150);
                }
                switch (dice)
                {
                    case 1:
                        Console.Write(" YOU ARE HORRIBLE");
                        break;
                    case 2:
                        Console.Write(" TOO BAD");
                        break;
                    case 3:
                        Console.Write(" NOT GOOD");
                        break;
                    case 4:
                        Console.Write(" GOOD");
                        break;
                    case 5:
                        Console.Write(" VERY GOOD");
                        break;
                    case 6:
                        Console.Write(" PERFECT");
                        break;
                }
            });
            Console.ReadKey(true);
            stop = true;
            diceTask.Wait();
            return dice;
        }


    }
}
