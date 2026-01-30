using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchTwoCard
{
    public class LevelFailUI : PopupUIPanel
    {
        [SerializeField] private Button btnRetry;
        [SerializeField] private Button btnHome;
        [SerializeField] private Button btnBuyTurns;
        [SerializeField] private TMP_Text coinsText;
        // Start is called before the first frame update
        void Start()
        {
            btnRetry.onClick.AddListener(OnRetryButtonClicked);
            btnHome.onClick.AddListener(OnHomeButtonClicked);
            btnBuyTurns.onClick.AddListener(OnBuyTurnsButtonClicked);
        }

        private void OnBuyTurnsButtonClicked()
        {
            UIManager.Instance.PlayButtonClickAudio();
            if (CurrencyManager.Instance.HasEnoughCoins(10))
            {
                CurrencyManager.Instance.RemoveCoins(10);
                LevelManager.Instance.BuyExtraTurns(5);
                CloseUI();
            }
            else
            {
                Debug.Log("Not enough coins to buy turns!");
            }
        }

        private void OnEnable()
        {
            
            DOVirtual.DelayedCall(0.5f, () =>
            {
                AudioManager.Instance.PlayAudio(AudioID.GameOver);
            });
            //AudioManager.Instance.PlayAudio(AudioID.GameOver);
            coinsText.text = "Coins : " + CurrencyManager.Instance.Coins.ToString();
        }

        private void OnHomeButtonClicked()
        {
            CardController.Instance.ResetGridData();
            UIManager.Instance.PlayButtonClickAudio();
            UIManager.Instance.SwitchUI<MainMenu>();
            CloseUI();
        }

        private void OnRetryButtonClicked()
        {
            CardController.Instance.ResetGridData();
            UIManager.Instance.PlayButtonClickAudio();
            LevelManager.Instance.RestartLevel();
            CloseUI();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}