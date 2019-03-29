using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeText : MonoBehaviour
{

    int i = 1;
    Text component;
    float actualTime = 0;
    IEnumerator coroutine;

    string[] parts = new string[]
    {
        "Toute sa vie Archibald, scientifique de son état, travailla à mettre au point une machine à voyager dans le temps. Après des années de recherche il arriva au résultat tant espéré. Au cours d’un de ses voyages un imprévu se produisit, il se retrouva piégé dans une faille temporelle.",
        "Trois ans passèrent après cet incident sans qu’il ne donnât signe de vie. Tous ses proches se désespéraient de ne plus jamais le revoir. Sa petite fille, Clémence, était très attristée par cette absence. Pour se remémorer certains souvenirs elle décida de se rendre dans le laboratoire de son grand-père. ",
        "Elle tomba alors sur les notes qu’il avait laissées. Ces dernières concernaient toutes le même sujet : une machine à voyager dans le temps. Un croquis de celle-ci était dessiné, et il ne lui fallut pas longtemps pour comprend qu’il s’agissait de l’objet qui se trouvait près d’elle.",
        "Sans vraiment savoir ce qu’elle faisait, elle actionna l’appareil. Elle se retrouva transportée dans une salle blanche qui lui était parfaitement inconnue. C’était la salle du temps !"
    };
    private void Start()
    {
        this.component = GetComponent<Text>();
        this.component.text = parts[0];
        component.color = new Color(component.color.r, component.color.g, component.color.b, 0);
        coroutine = WaitForStart();
        StartCoroutine(coroutine);
        //StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
    }

    public IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(6);

        coroutine = FadeTextToFullAlpha(1f);
        StartCoroutine(coroutine);

        yield return null;
    }


    public IEnumerator FadeTextToFullAlpha(float t)
    {
        component.color = new Color(component.color.r, component.color.g, component.color.b, 0);
        while (component.color.a < 1.0f)
        {
            component.color = new Color(component.color.r, component.color.g, component.color.b, component.color.a + (Time.deltaTime / t));
            yield return null;
        }

        StopCoroutine(coroutine);
        coroutine = WaitAndFade(10);
        StartCoroutine(coroutine);

    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        component.color = new Color(component.color.r, component.color.g, component.color.b, 1);
        while (component.color.a > 0.0f)
        {
            component.color = new Color(component.color.r, component.color.g, component.color.b, component.color.a - (Time.deltaTime / t));
            yield return null;
        }

        StopCoroutine(coroutine);
        ChangeText();

    }

    private IEnumerator WaitAndFade(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            StopCoroutine(coroutine);
            coroutine = FadeTextToZeroAlpha(1f);
            StartCoroutine(coroutine);
        }
    }

    public void ChangeText()
    {
        if(i+1 == parts.Length)
        {
            coroutine = WaitForScene();
            StartCoroutine(coroutine);
        } else
        {
            component.text = parts[i++];
            coroutine = FadeTextToFullAlpha(1f);
            StartCoroutine(coroutine);
        }
       
    }

    IEnumerator WaitForScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

        yield return null;
    }
}

