using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelection : MonoBehaviour
{
    public static int lang = 0; // 0=ger; 1=eng

    public Canvas thisCanvas;
    public Canvas nextCanvas1;

    public Text infoNextButton;

    public void SetLanguage(int l)
    {
        lang = l;
        thisCanvas.enabled = false;
        nextCanvas1.gameObject.SetActive(true);
        if(lang == 1)
        {
            SetLangEng();
        }
    }

    private void SetLangEng()
    {
        infoNextButton.text = "Skip Task";
    }
}
