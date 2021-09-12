using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleFader : MonoBehaviour
{
    public UnityEvent onTitleFadedAway = new UnityEvent();

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void ShowTitle(string title)
    {
        text.text = title;
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
