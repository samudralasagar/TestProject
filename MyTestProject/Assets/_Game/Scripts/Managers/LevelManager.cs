using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MatchTwoCard
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private LevelDataSO levelDataSO;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private int maxLevel;
        [SerializeField] private int currentLevelIndex;
        [SerializeField] private int workingTurns;
        
        public int WorkingTurns
        {
            get => workingTurns;
            set => workingTurns = value;
        }

        private string baseKey = "MatchTwoCard_Level";
        public LevelData levelData;
        public int Level => currentLevelIndex + 1;
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }
        private void OnEnable()
        {
            GameDataManager.onDataUpdated += LoadData;
        }
        private void OnDisable()
        {
            GameDataManager.onDataUpdated -= LoadData;
        }

        private void Init()
        {
            if (currentLevelIndex < 0)
                currentLevelIndex = GameDataManager.Instance.GetValue(baseKey, 0);
            if (maxLevel < 0)
                maxLevel = levelDataSO.levelDataList.Count;
        }

        public void LoadData()
        {
            currentLevelIndex = GameDataManager.Instance.GetValue(baseKey, 0);
            
        }

        public void LoadCurrentLeveldata()
        {
            //levelData.spriteList.Clear();
            levelData = levelDataSO.levelDataList[currentLevelIndex];
            workingTurns = levelData.turns;
            gridLayoutGroup.constraintCount = levelData.columnCount;
        }

        public LevelData GetLevelData(int level)
        {
            foreach (LevelData levelData in levelDataSO.levelDataList)
            {
                if (levelData.level == level)
                {
                    return levelData;
                }
            }
            return null;
        }

        public void LevelFinished()
        {
            currentLevelIndex++;
            if (currentLevelIndex >= maxLevel)
            {
                currentLevelIndex = 0;
            }
            GameDataManager.Instance.SetValue(baseKey, currentLevelIndex);
            UIManager.Instance.SwitchUI<MainMenu>();
        }

        public void RestartLevel()
        {             
            LoadCurrentLeveldata();
            UIManager.Instance.GetUI<HUD>().UpdateLevelData();
            UIManager.Instance.GetUI<HUD>().UpdateHUD();
        }

        public void ResetLevelTurns()
        {
            if (levelData != null)
            {
                workingTurns = levelData.turns;  
            }
        }

    }
}
