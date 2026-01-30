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

        private void OnNextLevelButtonClicked()
        {
            LevelManager.Instance.LevelFinished();
            CloseUI();
        }
    }
}