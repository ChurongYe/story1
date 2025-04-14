using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hints : MonoBehaviour
{
    public Image[] hintamounts;
    private int currentHintIndex = 0;
    public AudioClip Gou;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public AudioClip nextbutton;
    public AudioSource audioSource;
    public GameObject chair;
    private int t;
    private void Start()
    {
        StartCoroutine(FadeIn());
        foreach (var img in hintamounts)
        {
            img.gameObject.SetActive(false);
        }
        t = 0;
    }
    public void Addhint()
    {
        if (currentHintIndex < hintamounts.Length)
        {
            hintamounts[currentHintIndex].gameObject.SetActive(true);
            audioSource.PlayOneShot(Gou);
            currentHintIndex++;
        }
        if (currentHintIndex == hintamounts.Length && t ==0)
        {
            audioSource.PlayOneShot(nextbutton);

            if (chair != null)
            {
                chair.SetActive(true);
            }
            t++;
        }
    }
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    IEnumerator FadeIn()
    {
        yield return Fade(1f, 0f);
        fadeImage.enabled = false;
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        //audioSource.PlayOneShot(nextbutton);
        fadeImage.enabled = true;
        yield return Fade(0f, 1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }
}

