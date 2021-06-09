using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryRadioTextInput : AbstractUIQuery
{
    public TextMeshProUGUI caption;
    public TMP_InputField input;
    public string query = "Gender";

    void Awake()
    {
        caption.text = query + (required ? "*" : "");
        input.onValueChanged.AddListener(e => GetComponentInParent<UIQueryParent>().NotifyChange());
    }

    public override string GetResult()
    {
        string result = "{";
        result += "query:" + query;
        result += ",result:" + input.text;
        result += "}";
        return result;
    }

    public override bool IsAccepted()
    {
        return !required || input.text.Length > 3;
    }
}
