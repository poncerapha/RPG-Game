using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour {

    public static UIFade instance;

    public Image fadeImage;
    public bool shoulFadeToBlack;
    public bool shoulFadeFromBlack;
    public float fadeSpeed;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        HandleFade();
    }

    private void HandleFade()
    {
        if (shoulFadeToBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeImage.color.a == 1f)
            {
                shoulFadeToBlack = false;
            }
        }

        if (shoulFadeFromBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, Mathf.MoveTowards(fadeImage.color.a, 0, fadeSpeed * Time.deltaTime));
            if (fadeImage.color.a == 0)
            {
                shoulFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shoulFadeToBlack = true;
        shoulFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shoulFadeFromBlack = true;
        shoulFadeToBlack = false;
    }
}
