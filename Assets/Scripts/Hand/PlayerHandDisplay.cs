using Scripts.Card;
using Scripts.Hand;
using UnityEngine;
using UnityEngine.XR;

namespace Scripts.Hand
{
    public class PlayerHandDisplay : HandDisplay
    {
        PlayerHand playerHand => (PlayerHand)hand;
        CardDisplay selectedDisplay = null;

        [SerializeField] float selectedOffset = 80;

        protected override void Start()
        {
            base.Start();
            playerHand.OnCardSelected.AddListener(DisplaySelected);
        }

        protected override void DisplayHand(Hand hand)
        {
            base.DisplayHand(hand);
            DisplaySelected(playerHand.SelectedCardIndex);
        }

        private void DisplaySelected(int index)
        {
            if (GetCardOrNull(index) != null)
            {
                selectedDisplay?.ResetDisplayPosition();
                selectedDisplay = cardDisplays[index];
                cardDisplays[index].MoveCard(Vector2.up * selectedOffset);
            }
        }
    }
}
