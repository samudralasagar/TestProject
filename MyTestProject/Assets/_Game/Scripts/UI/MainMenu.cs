using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.UI;
using System;
namespace MatchTwoCard
{
 
    public class MainMenu : UIPanel
    {
        [SerializeField] private Button playButton;

        // Start is called before the first frame update
        void Start()
        {
            playButton.onClick.AddListener(PlayGame);
             
        }

        private void PlayGame()
        {
            UIManager.Instance.SwitchUI<HUD>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}