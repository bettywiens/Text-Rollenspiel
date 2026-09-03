using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rollenspiel
{
    internal class Knight
    {
        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Agility { get; set; }

        public int Luck { get; set; }
        public int Health { get; set; }

        public Knight()
        {
            Attack = 30;
            Defense = 0;
            Agility = 10;
            Luck = 32;
            Health = 300;
        }



        
    }
}
