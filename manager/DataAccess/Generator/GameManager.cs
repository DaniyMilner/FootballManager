using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;

namespace DataAccess.Generator
{
    public class GameManager
    {
        private readonly Random random = new Random();
        public bool TeamWithBall(int homeChance, int guestChance)
        {
            var randomResult = random.Next(0, homeChance + guestChance);
            return randomResult < homeChance;
        }

        public int GetMinute()
        {
            return random.Next(2, 5);
        }
    }
}
