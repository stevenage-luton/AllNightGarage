using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup interactingGroup;

    private void Start()
    {
        interactingGroup.alpha = 0;
    }

    public void UIFade(bool value)
    {
       StopAllCoroutines();
        if (value)
        {
            StartCoroutine(FadeUiIn());
        }
        else
        {
            StartCoroutine(FadeUiOut());
        }

    }

    private IEnumerator FadeUiIn()
    {
        interactingGroup.alpha = 0;
        while (interactingGroup.alpha <= 1)
        {
            interactingGroup.alpha += 0.1f;
            yield return new WaitForSeconds(.05f);
        }


    }

    private IEnumerator FadeUiOut()
    {
        interactingGroup.alpha = 1;

        while (interactingGroup.alpha >= 0)
        {
            interactingGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(.05f);
        }
    }
}
