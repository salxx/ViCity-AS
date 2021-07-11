using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class UIQueryManagerResult
{
    public string sessionId;
    public int timeStamp = (int)Time.time;
    public List<UIQueryParentResult> result = new List<UIQueryParentResult>();

    public UIQueryManagerResult(string sessionId)
    {
        this.sessionId = sessionId;
    }
}

public class UIQueryManager : MonoBehaviour
{
    public Canvas infoCanvas;
    public TextMeshProUGUI pageName;
    public UIQueryCatalogue[] catalogues;
    string sessionId;

    int currentPageId = 0;
    int currentCatalogueId = 0;
    UIQueryCatalogue currentCatalogue;
    UIQueryParent currentPage;

    private void Start()
    {
        infoCanvas.enabled = false;
        sessionId = "" + (new System.Random()).Next(0, 100000);
        currentCatalogue = catalogues[currentCatalogueId];
        currentCatalogue.gameObject.SetActive(true);
        LoadCurrentPage();
    }

    public void RetryCatalogue(UIQueryCatalogue catalogueToRetry)
    {
        int index = -1;
        for(int i = 0; i < catalogues.Length; i++)
        {
            if(catalogues[i] == catalogueToRetry)
            {
                index = i;
                break;
            }
        }
        currentPageId = 999;
        currentCatalogueId = index - 1;
        NotifyPageDone();
    }

    public void NotifyPageDone()
    {
        infoCanvas.enabled = false;
        gameObject.GetComponent<Canvas>().enabled = true;
        SendData();

        currentPageId++;
        currentPage.gameObject.SetActive(false);
        pageName.text = "";

        if (currentCatalogue.pages.Length > currentPageId)
        {
            LoadCurrentPage();
        }
        else
        {
            currentCatalogue.gameObject.SetActive(false);
            currentCatalogueId++;
            if (catalogues.Length > currentCatalogueId)
            {
                currentCatalogue = catalogues[currentCatalogueId];
                currentCatalogue.gameObject.SetActive(true);
                currentPageId = 0;
                LoadCurrentPage();
            } else
            {
                // TODO no more pages in catalogue
                gameObject.GetComponent<Canvas>().enabled = false;
            }
        }
    }

    private void LoadCurrentPage()
    {
        if(currentCatalogue.userTask)
        {
            foreach(AbstractPreparationScript prepScript in currentCatalogue.GetComponentsInChildren<AbstractPreparationScript>())
            {
                prepScript.DoPreparation();
            }
            gameObject.GetComponent<Canvas>().enabled = false;
            infoCanvas.GetComponentInChildren<TextMeshProUGUI>().text = (LanguageSelection.lang == 0) ? currentCatalogue.catalogueName : currentCatalogue.catalogueNameEng;
            infoCanvas.enabled = true;
        } else
        {
            currentPage = currentCatalogue.pages[currentPageId];
            currentPage.gameObject.SetActive(true);
            pageName.text = currentPage.GetLocalizedPageName();
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
