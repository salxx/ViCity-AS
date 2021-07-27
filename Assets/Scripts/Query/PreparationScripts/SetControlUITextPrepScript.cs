using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetControlUITextPrepScript : AbstractPreparationScript
{
    public GameObject controls;
    public int controlsChild = 0;
    public bool childFromPrefered = false;
    public bool showNumbers = false;
    public bool showCkey = false;
    public UIQueryRadioButtons decision;

    public override void DoPreparation()
    {
        for(int i = 0; i < controls.transform.childCount; i++)
        {
            controls.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
        if(childFromPrefered)
        {
            int option = decision.GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption();
            controls.transform.GetChild(option % 2).GetComponent<Image>().enabled = true;
        } else
        {
            controls.transform.GetChild(controlsChild).GetComponent<Image>().enabled = true;
        }
        if(showNumbers)
        {
            controls.transform.GetChild(2).GetComponent<Image>().enabled = true;
        }
        if(showCkey)
        {
            controls.transform.GetChild(3).GetComponent<Image>().enabled = true;
        }
    }
}
