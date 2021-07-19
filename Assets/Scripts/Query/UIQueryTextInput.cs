using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIQueryTextInput : AbstractUIQuery
{
    public int minCharacters = 3;
    public int maxCharacters = 250;
    public bool numbersOnly = false;
    public TextMeshProUGUI caption;
    public TMP_InputField input;
    public string queryName = "Anmerkung";
    public string query = "Gender";
    public string queryEng = "Gender";
    string previousValue;

    void Awake()
    {
        caption.text = (LanguageSelection.lang == 0 ? query : queryEng) + (required ? "*" : " (optional)");
        input.onValueChanged.AddListener(e => {
            if (e.Length > maxCharacters)
            {
                input.text = previousValue;
            }
            if(e.Length > 0 && numbersOnly)
            {
                int res;
                if(!int.TryParse(e, out res))
                {
                    input.text = previousValue;
                }
            }
            previousValue = input.text;
            GetComponentInParent<UIQueryParent>().NotifyChange();
        });
    }

    public override AbstractUIQueryResult GetResult()
    {
        AbstractUIQueryResult result = new AbstractUIQueryResult();
        result.query = queryName;
        result.result = input.text;
        return result;
    }

    public override bool IsAccepted()
    {
        return !required || input.text.Length >= minCharacters;
    }
}
