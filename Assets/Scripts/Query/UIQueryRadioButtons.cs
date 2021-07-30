using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryRadioButtons : AbstractUIQuery
{
    public string query = "Gender";
    public string queryEng = "Gender";
    public string[] options = new string[] { "m", "f", "d" };
    public string[] optionsEng = new string[] { "m", "f", "d" };

    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = (LanguageSelection.lang == 0 ? query : queryEng) + (required ? "*" : "");
        GetComponentInChildren<RadiobuttonGroupCreator>().CreateGroup(LanguageSelection.lang == 0 ? options : optionsEng, () => GetComponentInParent<UIQueryParent>().NotifyChange());
    }

    public override AbstractUIQueryResult GetResult()
    {
        AbstractUIQueryResult result = new AbstractUIQueryResult();
        result.query = query;
        int option = GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption();
        if(option >= 0)
        {
            result.result = options[option];
        }
        return result;
    }

    public override bool IsAccepted()
    {
        return !required || GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption() >= 0;
    }
}
