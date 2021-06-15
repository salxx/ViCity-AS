using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class UIQueryManagerResult
{
    public string sessionId;
    public List<UIQueryParentResult> result = new List<UIQueryParentResult>();

    public UIQueryManagerResult(string sessionId)
    {
        this.sessionId = sessionId;
    }
}

public class UIQueryManager : MonoBehaviour
{
    public TextMeshProUGUI pageName;
    public UIQueryParent[] userDataQuery;
    string sessionId;

    int currentPageId = 0;
    UIQueryParent currentPage;

    private void Start()
    {
        sessionId = "" + (new System.Random()).Next(0, 100000);
        currentPage = userDataQuery[0];
        currentPage.gameObject.SetActive(true);
        pageName.text = currentPage.pageName;
    }

    internal void NotifyPageDone()
    {
        SendData();

        currentPageId++;
        currentPage.gameObject.SetActive(false);
        pageName.text = "";

        if (userDataQuery.Length > currentPageId)
        {
            currentPage = userDataQuery[currentPageId];
            currentPage.gameObject.SetActive(true);
            pageName.text = currentPage.pageName;
        } else
        {
            // TODO no more pages in catalogue
        }
    }

    public void SendData()
    {
        UIQueryManagerResult results = new UIQueryManagerResult(sessionId);
        // automatically only finds active gameobjects :)
        foreach (UIQueryParent uiQuery in GetComponentsInChildren<UIQueryParent>())
        {
            results.result.Add(uiQuery.GetData());
        }
        string json = JsonUtility.ToJson(results);
        FindObjectOfType<SendToGoogle>().PostData(json);
    }
}
