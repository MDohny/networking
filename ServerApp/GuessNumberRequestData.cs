using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    public class GuessNumberRequestData
    {
        public bool NewGame { get; set; }
        public int GuessedNumber { get; set; }
    }
}
