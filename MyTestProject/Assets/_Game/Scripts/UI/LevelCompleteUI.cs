using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
namespace MatchTwoCard
{
    public class LevelCompleteUI : PopupUIPanel
    {
        [SerializeField] private Button nextLevelButton;
        // Start is called before the first frame update
        void Start()
        {
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private void OnEnable()
        {
            DOVirtual.DelayedCall(0.5f,()=>
            {
                CurrencyManager.Instance.AddCoins(5);
                AudioManager.Instance.PlayAudio(AudioID.Success);
            });
            
        }

        private void OnNextLevelButtonClicked()
        {
            UIManager.Instance.PlayButtonClickAudio();
            LevelManager.Instance.LevelFinished();
            CloseUI();
        }
    }
}