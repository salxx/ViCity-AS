using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour {


    private string BASE_URL = 
"https://docs.google.com/forms/u/0/d/e/1FAIpQLSdo7kMKGdSSTt7aTn36Ez0Ug16OQbP-NMk9GOJ4jF9ooCoMAA/formResponse?entry.883024206=test2";

    IEnumerator Post() {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes("some data");

        using (UnityWebRequest www = new UnityWebRequest(BASE_URL, UnityWebRequest.kHttpVerbPOST)) {
            UploadHandlerRaw uH = new UploadHandlerRaw(bytes);
            DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
            www.uploadHandler = uH;
            www.downloadHandler = dH;
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.Send();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            } else {
                Debug.Log(www.ToString());
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

 public void Start() {
     Subscribe();
 }

    public void Subscribe() {
        StartCoroutine(Post());
    }
}