using NUnit.Framework;
using UnityEngine;

namespace Scripts.Card
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CardDisplay : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void DisplayCard(Card card)
        {
            spriteRenderer.sprite = card.Sprite;
        }

        public void ClearDisplay()
        {
            spriteRenderer.sprite = null;
        }
    }
}
