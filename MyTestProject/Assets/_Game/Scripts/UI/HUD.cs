using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

namespace MatchTwoCard
{
 
    public class HUD : UIPanel
    {
        [SerializeField] private TMP_Text turnsText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text matchesText;
        [SerializeField] private Button btnHome;
        
        public bool isBuyingTurns = false;

        // Start is called before the first frame update
        void Start()
        {
            btnHome.onClick.AddListener(OnHomeButtonClicked);
        }

        private void OnHomeButtonClicked()
        {
            UIManager.Instance.PlayButtonClickAudio();
            LevelManager.Instance.Home();
        }

        private async void OnEnable()
        {
            UpdateLevelData();  // Load level + update cards

            await Task.Yield();  // Wait 1 frame for data propagation

            UpdateHUD();  // Now data is ready
        }

        public void UpdateLevelData()
        {
            levelText.text = "Level : " + LevelManager.Instance.Level.ToString();
            if (LevelManager.Instance.WorkingTurns <= 0 && !isBuyingTurns)
            {
                LevelManager.Instance.ResetLevelTurns();
            }
            LevelManager.Instance.LoadCurrentLeveldata();
            if (CardController.Instance != null)
            {
                CardController.Instance.UpdateSpritesFromLevelData(LevelManager.Instance.levelData.spriteList);
            }
        }

        internal void UpdateHUD()
        {
            turnsText.text = "Turns : " + LevelManager.Instance.WorkingTurns.ToString();
            matchesText.text = "Matches : " + CardController.Instance.matchCount.ToString() + "/" + (LevelManager.Instance.levelData.spriteList.Count).ToString();
        }
    }
}