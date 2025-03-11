using Scripts.Hand;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class HandNameDisplay : MonoBehaviour
{
    [SerializeField] float displayTime = 1f;
    [SerializeField] float verticalTargetOffset = 1f;

    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void DisplayHand(HandType hand)
    {
        StartCoroutine(DisplayHandRoutine(hand));
    }

    IEnumerator DisplayHandRoutine(HandType hand)
    {
        text.text = HandRanker.HandTypeToString[hand];
        Vector3 startPos = transform.position;
        Vector3 targetPos = transform.position + Vector3.up * verticalTargetOffset;

        float elapsedTime = 0;
        while (elapsedTime < displayTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / displayTime);
            elapsedTime += Time.deltaTime;

            float hue = Mathf.Repeat(elapsedTime / displayTime, 1f);
            text.color = Color.HSVToRGB(hue, 1f, 1f);

            yield return null;
        }
        transform.position = targetPos;

        Destroy(gameObject);
    }
}
