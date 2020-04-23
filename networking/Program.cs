using networking.Models;
using System;
using System.Net;

namespace networking
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientModel client = ClientModel.Connect("127.0.0.1", 13000);
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();

            bool playAgain = true;

            while (playAgain)
            {

                GuessNumberRequestData requestData = new GuessNumberRequestData();
                requestData.NewGame = true;
                client.SendData(DataConvertor.ToString<GuessNumberRequestData>(requestData));
                GuessNumberResponseData responseData = null;
                do
                {
                    if (requestData.NewGame)
                    {
                        requestData.NewGame = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong guess.");
                    }
                    int number;
                    Console.WriteLine("Guess number [0-9]:");
                    if (!int.TryParse(Console.ReadLine(), out number))
                    {
                        Console.WriteLine("Wrong input!");
                        continue;
                    }
                    requestData.GuessedNumber = number;
                    responseData = DataConvertor.FromString<GuessNumberResponseData>(client.SendData(DataConvertor.ToString<GuessNumberRequestData>(requestData)));
                } while (!responseData.RightNumber);
                Console.WriteLine("You did it! Wanna play again (press r to play again).");
                if (Console.ReadKey().Key != ConsoleKey.R)
                {
                    playAgain = false;
                }
            }
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }
}
