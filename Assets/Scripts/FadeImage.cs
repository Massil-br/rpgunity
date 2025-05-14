using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeImage : MonoBehaviour
{
    public Image imageToFade;
    public float fadeDuration = 1f; // Durée du fade-in
    public float displayDuration = 2f; // Temps d'affichage plein
    public float fadeOutDuration = 1f; // Fade-out facultatif

    public DeathPopup death;

    public DateTime timer;

    void Start()
    {
        if (death == null)
        {
            death = FindFirstObjectByType<DeathPopup>();
        }
        if (death == null){
            Debug.Log("Death Skill issue");
        }
    }

    public void Skill_Issue(){
      StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        // Vérification et configuration du RectTransform
        RectTransform rt = imageToFade.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = new Vector2(200f, 200f);
            rt.anchoredPosition = Vector2.zero;
        }

        Color color = imageToFade.color;
        color.a = 0f;  // Alpha 0
        imageToFade.color = color;
        imageToFade.gameObject.SetActive(true);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            imageToFade.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(displayDuration);

        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
            imageToFade.color = color;
            yield return null;
        }

        imageToFade.gameObject.SetActive(false);

        death.DeathScreen("You died ! \n\nYou should try\n to get some skill\n \"or maybe cheat\"");
    }
}
