using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicFader : MonoBehaviour
{
    public int currentWave = 1;
    public Text accompanyingText;
    public List<CinematicText> cinematicTexts;
    public AudioSource audioSource;
    public bool shouldSwitchToMainMenuAfter = false;

    public UnityEvent onCinematicFinished = new UnityEvent();

    private Image image;

    public void StartCinematic()
    {
        if (!(this.image && this.image.color.a != 0.0f && this.image.color.a != 1.0f))
        {
            gameObject.SetActive(true);
            this.image = GetComponent<Image>();
            StartCoroutine(FadeImage(false));
            StartCoroutine(FadeText(0, false));
        }
    }

    public void SetCurrentWave(int waveNumber)
    {
        currentWave = waveNumber;
    }

    private IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(0, 0, 0, i);
                yield return null;
            }

            gameObject.SetActive(false);
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                image.color = new Color(0, 0, 0, i);
                accompanyingText.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

    private IEnumerator FadeText(int cinematicIndex, bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = 3; i >= 0; i -= Time.deltaTime)
            {
                if (i <= 1)
                {
                    accompanyingText.color = new Color(1, 1, 1, i);
                }

                if (audioSource && this.cinematicTexts.Count <= cinematicIndex + 1 && this.audioSource.volume >= i / 3)
                {
                    this.audioSource.volume = i / 3;
                }

                yield return null;
            }

            if (this.cinematicTexts.Count > cinematicIndex + 1)
            {
                StartCoroutine(FadeText(cinematicIndex + 1, false));
            } else
            {
                onCinematicFinished.Invoke();
                if (shouldSwitchToMainMenuAfter)
                {
                    GameObject.FindGameObjectWithTag("PlayFabManager").GetComponent<PlayFabManager>().UpdateLeaderboard(currentWave);
                    SceneManager.LoadScene("SampleScene 1");
                }
            }
        }
        // fade from transparent to opaque
        else
        {
            this.accompanyingText.text = this.cinematicTexts[cinematicIndex].Text;

            if (this.accompanyingText.text.Contains("{number}"))
            {
                this.accompanyingText.text = this.accompanyingText.text.Replace("{number}", currentWave.ToString());
            }

            for (float i = 0; i <= 3; i += Time.deltaTime)
            {
                if (i <= 1)
                    accompanyingText.color = new Color(1, 1, 1, i);

                yield return null;
            }

            StartCoroutine(FadeText(cinematicIndex, true));
        }
    }
}

[Serializable]
public class CinematicText
{
    public string Text;
    [HideInInspector]
    public bool Used;
}
