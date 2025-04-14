using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hints : MonoBehaviour
{
    public int hintamounts = 3;
    public int nowhint;
    public Image fadeImage;
    public float fadeDuration = 1f;
    private void Start()
    {
        hintamounts = 3;
        nowhint = 0;
        StartCoroutine(FadeIn());
    }
    public void Addhint()
    {
        nowhint++;
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

