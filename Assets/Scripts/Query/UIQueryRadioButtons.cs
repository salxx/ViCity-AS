using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryRadioButtons : AbstractUIQuery
{
    public string query = "Gender";
    public string[] options = new string[] { "m", "f", "d" };

    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = query + (required ? "*" : "");
        GetComponentInChildren<RadiobuttonGroupCreator>().CreateGroup(options, () => GetComponentInParent<UIQueryParent>().NotifyChange());

    }

    public override AbstractUIQueryResult GetResult()
    {
        AbstractUIQueryResult result = new AbstractUIQueryResult();
        result.query = query;
        result.result = options[GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption()];
        return result;
    }

    public override bool IsAccepted()
    {
        return !required || GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption() >= 0;
    }
}
