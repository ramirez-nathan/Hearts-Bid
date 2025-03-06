using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Card
{
    public class CardDisplay : MonoBehaviour
    {
        Image image;
        Vector3 totalOffset = Vector3.zero;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
        }

        public void DisplayCard(Card card)
        {
            image.color = Color.white;
            image.sprite = card.Sprite;
        }

        public void ClearDisplay()
        {
            image.color = Color.clear;
            image.sprite = null;
        }

        public void MoveCard(Vector3 offset)
        {
            totalOffset += offset;
            image.transform.position += offset;
        }

        public void ResetDisplayPosition()
        {
            image.transform.position -= totalOffset;
            totalOffset = Vector3.zero;
        }
    }
}
