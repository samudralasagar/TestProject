using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchTwoCard
{
    public class LevelFailUI : PopupUIPanel
    {
        [SerializeField] private Button btnRetry;
        [SerializeField] private Button btnHome;
        // Start is called before the first frame update
        void Start()
        {
            btnRetry.onClick.AddListener(OnRetryButtonClicked);
            btnHome.onClick.AddListener(OnHomeButtonClicked);
        }
        private void OnEnable()
        {
            CardController.Instance.ResetGridData();
        }

        private void OnHomeButtonClicked()
        {
            UIManager.Instance.SwitchUI<MainMenu>();
            CloseUI();
        }

        private void OnRetryButtonClicked()
        {
            LevelManager.Instance.RestartLevel();
            CloseUI();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}