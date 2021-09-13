using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TooltipFader : MonoBehaviour
{
    public bool shouldSkip = false;
    public bool shouldSkipFirst = false;

    public UnityEvent onTitleFadedAway = new UnityEvent();
    public UnityEvent onTitleSkipped = new UnityEvent();

    private Text text;

    private void Start()
    {
        if (shouldSkip)
        {
            onTitleSkipped.Invoke();
        }
        else
        {
            text = GetComponent<Text>();

            if (!shouldSkipFirst)
            {
                ShowTitle();
            }
        }
    }

    public void ShowTitle()
    {
        StartCoroutine(FadeText(false));
    }

    private IEnumerator FadeText(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 5; i >= 0; i -= Time.deltaTime)
            {
                if (i <= 1)
                {
                    text.color = new Color(1, 1, 1, i);
                }

                yield return null;
            }

            onTitleFadedAway.Invoke();
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 3; i += Time.deltaTime)
            {
                if (i <= 1)
                    text.color = new Color(1, 1, 1, i);

                yield return null;
            }

            StartCoroutine(FadeText(true));
        }
    }
}
