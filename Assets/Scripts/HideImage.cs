using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideImage : MonoBehaviour {

    IEnumerator coroutine;
    Image image;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        coroutine = WaitForDisable();
        StartCoroutine(coroutine);
	}

    IEnumerator WaitForDisable()
    {
        yield return new WaitForSeconds(5);

        StopCoroutine(coroutine);
        coroutine = FadeText(1);
        StartCoroutine(coroutine);

        yield return null;
    }

    public IEnumerator FadeText(float t)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        while (image.color.a > 0.0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / t));
            yield return null;
        }

        StopCoroutine(coroutine);
       // GetComponent<Image>().enabled = false;

    }
}
