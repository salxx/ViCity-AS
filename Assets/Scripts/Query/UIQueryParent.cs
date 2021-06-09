using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQueryParent : MonoBehaviour
{
    public void SendData()
    {
        string result = "";
        foreach(AbstractUIQuery uiQuery in GetComponentsInChildren<AbstractUIQuery>())
        {
            result += uiQuery.GetResult();
        }
        FindObjectOfType<SendToGoogle>().PostData(result);
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
