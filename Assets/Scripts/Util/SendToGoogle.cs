using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour {


    private string BASE_URL = 
"https://docs.google.com/forms/u/0/d/e/1FAIpQLSdo7kMKGdSSTt7aTn36Ez0Ug16OQbP-NMk9GOJ4jF9ooCoMAA/formResponse";

    public List<UIQueryManagerResult> datas = new List<UIQueryManagerResult>();

    IEnumerator Post()
    {
        string allData = "";
        foreach(UIQueryManagerResult data in datas)
        {
            allData += JsonUtility.ToJson(data) + "\n";
        }
        WWWForm form = new WWWForm();
        form.AddField("entry.883024206", allData);
        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();
    }

    public void AddData(UIQueryManagerResult data)
    {
        datas.Add(data);
    }

    public void PostData() {
        StartCoroutine(Post());
    }
}