using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    public class GuessNumberService
    {
        private readonly Random random;

        private int number;

        public GuessNumberService()
        {
            random = new Random();
        }

        public void NewGame()
        {
            number = random.Next(1, 10);
        }

        public bool Guess(int number)
        {
            return this.number == number;
        }

        public GuessNumberResponseData ProcessRequest(GuessNumberRequestData requestData)
        {
            GuessNumberResponseData responseData = new GuessNumberResponseData();
            if (requestData.NewGame)
            {
                NewGame();
            }
            responseData.RightNumber = Guess(requestData.GuessedNumber);
            return responseData;
        }
    }
}
