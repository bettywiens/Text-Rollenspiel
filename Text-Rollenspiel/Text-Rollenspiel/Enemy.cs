using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rollenspiel
{
    internal class Enemy
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public List<Attack> Attacks { get; set; }

        public int Health { get; set; }
    }
}
