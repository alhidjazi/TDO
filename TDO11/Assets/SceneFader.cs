using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;

    public AnimationCurve curve;

    private void Start()
    {
        //pour appele une fonction coroutine 
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }


    //FadeIn = Ecran noir vers scene (disparition de l'ecran noir)
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f) 
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            //deja on est dans une coroutine
            yield return 0;

        }
    }
    //FadeOut =vers scene Ecran noir (apparition de l'ecran noir)
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            //deja on est dans une coroutine
            yield return 0;

        }

        //le code ci-dessous ne se lit que lorsque le fondu est termine
        SceneManager.LoadScene(scene);
    }
}
