using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{

    Image fadeImage;
    [SerializeField] float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        FadeOut();
    }

    public void FadeIn(int scene)
    {
        gameObject.SetActive(true);
        fadeImage.CrossFadeAlpha(0, 0, false);
        fadeImage.CrossFadeAlpha(1, fadeDuration, false); 
        StartCoroutine(WaitBeforeDisable(false, scene));
    }

    public void FadeOut()
    { 
        fadeImage.CrossFadeAlpha(0, fadeDuration, false);
        StartCoroutine(WaitBeforeDisable(true, 0));
    }
     
     IEnumerator WaitBeforeDisable(bool isFadeOut, int scene)
    {
        if (isFadeOut)
        {
            yield return new WaitForSeconds(fadeDuration);
            gameObject.SetActive(false);
        } else
        { 
            yield return new WaitForSeconds(fadeDuration);
            SceneManager.LoadScene(scene);
        }

       
    }  

}
