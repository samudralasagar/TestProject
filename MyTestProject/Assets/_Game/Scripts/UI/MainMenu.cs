using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.UI;
using System;
using TMPro;
using DG.Tweening;
namespace MatchTwoCard
{
 
    public class MainMenu : UIPanel
    {
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_Text coinText;
        // Start is called before the first frame update
        void Start()
        {
            playButton.onClick.AddListener(PlayGame);
             
        }

        private void OnEnable()
        {
            DOVirtual.DelayedCall(0.1f, UpdateCoinUI);
            //UpdateCoinUI();
        }

        private void UpdateCoinUI()
        {
            coinText.text = "Coins : " + CurrencyManager.Instance.Coins.ToString();
        }

        private void PlayGame()
        {
            UIManager.Instance.PlayButtonClickAudio();
            UIManager.Instance.SwitchUI<HUD>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}