using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MatchTwoCard
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image cardIconImage;
        private Button cardButton;
        public Sprite hidIconSprite;
        public Sprite iconSprite;

        public bool isRevealed = false;

        public CardController cardController;
        // Start is called before the first frame update
        void Start()
        {
            cardButton = GetComponent<Button>();
            cardButton.onClick.AddListener(OnCardClick);
        }

        public void OnCardClick()
        {
            if (cardController != null)
            {
                cardController.SetSelected(this);
            }
        }
        public void SetIconSprite(Sprite sprite)
        {
            iconSprite = sprite;
        }

        public void ShowCard()
        {
            isRevealed = true;
            cardIconImage.sprite = iconSprite;
        }

        public void HideCard()
        {
            isRevealed = false;
            cardIconImage.sprite = hidIconSprite;
        }




    }
}