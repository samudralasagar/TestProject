using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace MatchTwoCard
{
    public class CardController : Singleton<CardController>
    {
        [SerializeField] Card cardPrefab;
        [SerializeField] Transform gridTransform;
        [SerializeField] private Sprite[] sprites;
        private List<Sprite> spritePairs = new List<Sprite>();

        public Sprite[] Sprites  // Change to public
        {
            get => sprites;
            set => sprites = value;
        }

        [SerializeField] private Card firstSelectedCard;
        [SerializeField] private Card secondSelectedCard;
        public int matchCount = 0;
        void Start()
        {
            //PrepareSprites();
            //GenerateCards();
        }
        public void UpdateSpritesFromLevelData(List<Sprite> levelSprites)
        {
            if (levelSprites == null || levelSprites.Count == 0)
            {
                Debug.LogWarning("Level sprites list is null or empty!");
                return;
            }
            ResetGridData();
            sprites = levelSprites.ToArray();
            PrepareSprites();
            GenerateCards();
        }

        private void PrepareSprites()
        {
            spritePairs.Clear();
            for (int i = 0; i < sprites.Length; i++)
            {
                spritePairs.Add(sprites[i]);
                spritePairs.Add(sprites[i]);
            }
            ShuffleSprites(spritePairs);
        }

        private void ShuffleSprites(List<Sprite> spriteList)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                Sprite temp = spritePairs[i];
                int randomIndex = Random.Range(i, spritePairs.Count);
                spritePairs[i] = spritePairs[randomIndex];
                spritePairs[randomIndex] = temp;
            }
        }

        public void GenerateCards()
        {
            for (int i = 0; i < spritePairs.Count; i++)
            {
                Card newCard = Instantiate(cardPrefab, gridTransform);
                newCard.SetIconSprite(spritePairs[i]);
                newCard.cardController = this;
                newCard.HideCard();
            }
        }

        public void SetSelected(Card selectedCard)
        {
            if (!selectedCard.isRevealed)
            {
                selectedCard.ShowCard();
                if (firstSelectedCard == null)
                {
                    AudioManager.Instance.PlayAudio(AudioID.CardFlip);
                    firstSelectedCard = selectedCard;
                    return;
                }

                if (secondSelectedCard == null)
                {
                    AudioManager.Instance.PlayAudio(AudioID.CardFlip);
                    secondSelectedCard = selectedCard;
                    StartCoroutine(CheckMatch());
                }

            }
            //else
            //{
            //    selectedCard.ShowCard();
            //}
        }

        IEnumerator CheckMatch()
        {
            yield return new WaitForSeconds(0.2f);
            if (firstSelectedCard.iconSprite == secondSelectedCard.iconSprite)
            {
                AudioManager.Instance.PlayAudio(AudioID.Match);
                // Match found
                matchCount++;
                if (matchCount >= spritePairs.Count / 2)
                {
                    Debug.Log("All Matches Found! Level Finished!");
                    matchCount = 0;
                    PopupUIManager.Instance.OpenUI<LevelCompleteUI>();
                }

            }
            else
            {
                AudioManager.Instance.PlayAudio(AudioID.MissMatch);
                // Mismatch - decrement turns
                if (LevelManager.Instance != null && LevelManager.Instance.levelData != null)
                {
                    LevelManager.Instance.WorkingTurns--;

                    if (LevelManager.Instance.WorkingTurns <= 0)
                    {
                        Debug.Log("No turns left! Level Failed!");
                        //LevelManager.Instance.ResetLevelTurns();
                        
                        PopupUIManager.Instance.OpenUI<LevelFailUI>();
                        ResetCardData();
                        yield break;
                    }
                }

                firstSelectedCard.HideCard();
                secondSelectedCard.HideCard();
            }
            firstSelectedCard = null;
            secondSelectedCard = null;

            // Always update HUD after match/mismatch
            UIManager.Instance.GetUI<HUD>().UpdateHUD();            
        }

        public void ResetCardData()
        {
            if (firstSelectedCard != null)
            {
                firstSelectedCard.HideCard();
            }
                
            if (secondSelectedCard != null)
            {
                secondSelectedCard.HideCard();
            }
                
            firstSelectedCard = null;
            secondSelectedCard = null;
        }

        public void ResetGridData()
        {
            if (gridTransform == null)
            {
                Debug.LogError("gridTransform is null!");
                return;
            }
            if (gridTransform.childCount > 0)
            {
                for (int i = gridTransform.childCount - 1; i >= 0; i--)
                {
                    Destroy(gridTransform.GetChild(i).gameObject);
                }
            }
            firstSelectedCard = null;
            secondSelectedCard = null;
            matchCount = 0;
        }
    }
}