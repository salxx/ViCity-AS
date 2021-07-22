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

    private void Start()
    {
    }

    private void OnDisable()
    {

        StopAllCoroutines();
    }

    private void OnEnable()
    {
        NotifyChange();

        if (LanguageSelection.lang == 1)
        {
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Next";
        }

        if (GetComponentInChildren<Button>() && GetComponentInChildren<Button>().interactable)
        {
            GetComponentInChildren<Button>().interactable = false;
            StartCoroutine(DelayedButton());
        }
    }

    private IEnumerator DelayedButton()
    {
        int seconds = 2;
        while(seconds > 0)
        {
            GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = GetLocalizedNext() + " (" + seconds + ")";
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = GetLocalizedNext();
        GetComponentInChildren<Button>().interactable = true;
    }

    private static string GetLocalizedNext()
    {
        return LanguageSelection.lang == 0 ? "Weiter" : "Next";
    }

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
            AbstractUIQueryResult res = uiQuery.GetResult();
            if(res != null)
            {
                result.result.Add(res);
            }
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
        if(GetComponentInChildren<Button>())
        {
            GetComponentInChildren<Button>().interactable = canSend;
        }
    }
}
