using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class LoginScript : MonoBehaviour {

    public TextMeshProUGUI loginText;

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str);

    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture);

    void Start() {
        /*float[] myArray = new float[10];
        PrintFloatArray(myArray, myArray.Length);
        
        int result = AddNumbers(5, 7);
        Debug.Log(result);
        
        Debug.Log(StringReturnValueFunction());
        
        var texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
        BindWebGLTexture(texture.GetNativeTextureID());*/
    }

    public void Login(string credentials) {
        if(credentials.Split('$')[1].Equals("secret")) {
            loginText.text = "User: " + credentials.Split('$')[0];
        } else {
            loginText.text = "Invalid password.";
        }
    }

    static string s_dataUrlPrefix = "data:image/png;base64,";

    public void Image(string base64Image) {
        if (base64Image.StartsWith(s_dataUrlPrefix))
        {
            byte[] pngData = System.Convert.FromBase64String(base64Image.Substring(s_dataUrlPrefix.Length));

            // Create a new Texture (or use some old one?)
            Texture2D tex = new Texture2D(1, 1); // does the size matter?
            if (tex.LoadImage(pngData))
            {
                Renderer renderer = GetComponentInChildren<Renderer>();
                renderer.material.mainTexture = tex;
            }
        }
    }
}