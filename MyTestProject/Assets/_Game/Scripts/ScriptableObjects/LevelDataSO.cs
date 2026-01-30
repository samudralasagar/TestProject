using UnityEngine;
using System.Collections.Generic;

namespace MatchTwoCard
{
    [CreateAssetMenu(fileName = "LevelDataSO", menuName = "MatchTwoCard/LevelDataSO")]
    public class LevelDataSO : ScriptableObject {
        public List<LevelData> levelDataList;
        //public void ResetList()
        //{
        //    foreach (LevelData levelData in levelDataList)
        //    {
        //        levelData.ResetList();
        //    }
        //}
    }

    [System.Serializable]
    public class LevelData{
        public int level;
        public int turns;
        public int columnCount;
        public List<Sprite> spriteList;

        //public void ResetList()
        //{
        //    spriteList.Clear();
        //}
    }

    //[System.Serializable]
    //public class LevelSprites{
        
    //    public Item item;
    //    public int targetCount;
    //    [HideInInspector] public int currentCount;
    //}
}
