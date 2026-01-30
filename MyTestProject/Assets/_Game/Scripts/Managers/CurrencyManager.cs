using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTwoCard
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        private int coins;

        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }


        // Start is called before the first frame update
        void Start()
        {

        }


        public void AddCoins(int amount)
        {
            coins += amount;
          
        }

        public void RemoveCoins(int amount) {
            coins -= amount;
            if (coins < 0) coins = 0;
          
        }

       

         
    }
}
