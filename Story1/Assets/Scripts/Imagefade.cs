using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Imagefade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public Button button;
    private float timer;
    public float targettime = 12f;
    private void Start()
    {
        timer = 0;
        StartCoroutine(FadeOut());
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer> targettime)
        {
            button.gameObject.SetActive(true);
            timer = 0;
        }
    }
    IEnumerator FadeOut()
    {
        yield return Fade(0f, 1f);
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
