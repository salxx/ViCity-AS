using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour {


    private string BASE_URL = 
"https://docs.google.com/forms/u/0/d/e/1FAIpQLSdo7kMKGdSSTt7aTn36Ez0Ug16OQbP-NMk9GOJ4jF9ooCoMAA/formResponse";

    IEnumerator Post(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.883024206", data);
        UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form);
        yield return www.SendWebRequest();
    }

    public void PostData(string data) {
        return;
        StartCoroutine(Post(data));
    }
}