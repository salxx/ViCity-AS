using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRetryButtonLanguage : MonoBehaviour
{
    void OnEnable()
    {
        if(LanguageSelection.lang == 1)
        {
            GetComponentInChildren<Text>().text = "Retry";
        }
    }

}
