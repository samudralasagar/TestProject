using DG.Tweening;
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
            // Rotate front face 180° to reveal
            transform.DORotate(new Vector3(0, 180, 0), 0.3f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    cardIconImage.sprite = iconSprite;  // Reveal icon after flip
                });
            isRevealed = true;
        }


        public void HideCard()
        {   
            DOTween.To(() =>  transform.eulerAngles.y,
                       x =>  transform.eulerAngles = new Vector3(0, x, 0),
                       0f, 0.3f)  // Same 0.3s duration
                   .SetEase(Ease.InBack)
                   .OnComplete(() => {
                       cardIconImage.sprite = hidIconSprite;  // Hide after flip
                   });
            isRevealed = false;
        }




    }
}