using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchTwoCard
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [SerializeField] private int coins;

        public int Coins
        {
            get { return coins; }
            set { coins = value; }
        }


        // Start is called before the first frame update
        void Start()
        {
            DOVirtual.DelayedCall(0.1f, OnLoadData);
            //OnLoadData();
        }

        private void OnEnable()
        {
            GameDataManager.onDataUpdated += OnLoadData;
        }
        private void OnDisable()
        {
            GameDataManager.onDataUpdated -= OnLoadData;
            OnSaveData();
        }
        private void OnLoadData()
        {
            coins = GameDataManager.Instance.GetValue("Coins",0 );
        }
        private void OnSaveData()
        {
            GameDataManager.Instance.SaveJsonDataToFile();
        }

        public void AddCoins(int amount)
        {
            coins += amount;
            GameDataManager.Instance.SetValue("Coins", coins);
        }

        public void RemoveCoins(int amount) {
            coins -= amount;
            if (coins < 0) coins = 0;
          GameDataManager.Instance.SetValue("Coins", coins);
        }

        public bool HasEnoughCoins(int amount)
        {
            return coins >= amount;
        }


    }
}
