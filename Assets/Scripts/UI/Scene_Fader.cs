using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Fader : MonoBehaviour
{
    public static Scene_Fader instance;

    public GameObject fadeCanvas;
    public Animator myAnimator;

    private void Awake()
    {
        MakeSingleton();
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private IEnumerator FadeInAnimation(string sceneName)
    {
        fadeCanvas.SetActive(true);
        myAnimator.Play("Fade_Out_Animation");
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(sceneName);
        myAnimator.Play("Fade_In_Animation");
        yield return new WaitForSeconds(1f);
        fadeCanvas.SetActive(false);
    }

    public void FadeIn(string sceneName)
    {
        StartCoroutine(FadeInAnimation(sceneName));
    }
}