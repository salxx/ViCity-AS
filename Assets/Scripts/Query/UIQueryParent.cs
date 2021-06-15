using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIQueryParentResult
{
    public string pageName;
    public List<AbstractUIQueryResult> result = new List<AbstractUIQueryResult>();
}


public class UIQueryParent : MonoBehaviour
{
    public string pageName;

    public void ContinueFlow()
    {
        GetComponentInParent<UIQueryManager>().NotifyPageDone();
    }

    public UIQueryParentResult GetData()
    {
        UIQueryParentResult result = new UIQueryParentResult();
        result.pageName = pageName;
        foreach (AbstractUIQuery uiQuery in GetComponentsInChildren<AbstractUIQuery>())
        {
            result.result.Add(uiQuery.GetResult());
        }
        return result;
    }

    public void NotifyChange()
    {
        bool canSend = true;
        foreach (AbstractUIQuery uiQuery in GetComponentsInChildren<AbstractUIQuery>())
        {
            canSend = canSend && uiQuery.IsAccepted();
        }
        GetComponentInChildren<Button>().interactable = canSend;
    }
}
