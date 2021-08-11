using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillVerificationPrepScript : AbstractPreparationScript
{
    public Button sendButton;

    public UIQueryRadioButtons gender;
    public UIQueryTextInput age;
    public UIQuerySlider compExp;
    public UIQuerySlider gameExp;
    public UIQuerySlider keyboard;
    public UIQuerySlider mouse;
    public UIQuerySlider wp_keyboard;
    public UIQuerySlider wp_mouse;
    public UIQueryRadioButtons controls;
    public UIQuerySlider camera1;
    public UIQuerySlider camera2;
    public UIQuerySlider camera3;
    public UIQuerySlider camera4;
    public UIQuerySlider camera5;
    public UIQueryRadioButtons cameras;

    public UIQuerySlider avatar1;
    public UIQueryRadioButtons avatar1RB;
    public UIQuerySlider avatar2;
    public UIQueryRadioButtons avatar2RB;
    public UIQuerySlider avatar3;
    public UIQueryRadioButtons avatar3RB;

    public UIQueryTextInput q1;
    public UIQueryTextInput q2;
    public UIQueryTextInput q3;

    public override void DoPreparation()
    {
        List<UIQueryManagerResult> datas = FindObjectOfType<SendToGoogle>().datas;
        datas.ForEach(data => {
            if(data.result == null || data.result.Count == 0 || data.result[0].result == null || data.result[0].result.Count == 0) {
                return;
            }
            string query = data.result[0].pageName;
            if(query.Equals("Anonyme Datenangabe")){
                SetRadioButtonValue(gender, data.result[0].result[0]);
                SetTextInputValue(age, data.result[0].result[1]);
                SetSliderValue(compExp, data.result[0].result[2]);
                SetSliderValue(gameExp, data.result[0].result[3]);
            } else
            if(query.Equals("Steuerung Keyboard")){
                SetSliderValue(keyboard, data.result[0].result[0]);
            } else
            if(query.Equals("Steuerung Maus")){
 SetSliderValue(mouse, data.result[0].result[0]);
            } else
            if(query.Equals("Steuerung Wegpunkte Keyboard")){
 SetSliderValue(wp_keyboard, data.result[0].result[0]);
            } else
            if(query.Equals("Steuerung Wegpunkte Maus")){
 SetSliderValue(wp_mouse, data.result[0].result[0]);
            } else
            if(query.Equals("Steuerung - Bewertung")){
SetRadioButtonValue(controls, data.result[0].result[0]);
            } else
            if(query.Equals("Kamera 1 - Referenzkamera")){
SetSliderValue(camera1, data.result[0].result[0]);
            } else
            if(query.Equals("Kamera 2 - Mehr Distanz")){
SetSliderValue(camera2, data.result[0].result[0]);
            } else
            if(query.Equals("Kamera 3 - Mehr Neigung")){
SetSliderValue(camera3, data.result[0].result[0]);
            } else
            if(query.Equals("Kamera 4 - Weniger Tiefe")){
SetSliderValue(camera4, data.result[0].result[0]);
            } else
            if(query.Equals("Kamera 5 - Weniger Neigung")){
SetSliderValue(camera5, data.result[0].result[0]);
            } else
            if(query.Equals("Kameravergleich")){
SetRadioButtonValue(cameras, data.result[0].result[0]);
            }
            else if(query.Equals("Agents1")){
                SetSliderValue(avatar1, data.result[0].result[0]);
                SetRadioButtonValue(avatar1RB, data.result[0].result[1]);
            }
            else if (query.Equals("Agents2"))
            {
                SetSliderValue(avatar2, data.result[0].result[0]);
                SetRadioButtonValue(avatar2RB, data.result[0].result[1]);
            }
            else if (query.Equals("Agents3"))
            {
                SetSliderValue(avatar3, data.result[0].result[0]);
                SetRadioButtonValue(avatar3RB, data.result[0].result[1]);
            }
            else
            if (query.Equals("Fragen")){
             SetTextInputValue(q1, data.result[0].result[0]);
             SetTextInputValue(q2, data.result[0].result[1]);
             SetTextInputValue(q3, data.result[0].result[2]);
            }
        });
    }

    private void SetTextInputValue(UIQueryTextInput ti, AbstractUIQueryResult result)
    {
        ti.GetComponentInChildren<TMPro.TMP_InputField>().text = result.result;
        ti.GetComponentInChildren<TMPro.TMP_InputField>().onValueChanged.AddListener(t => 
        {
            result.result = ti.GetComponentInChildren<TMPro.TMP_InputField>().text;
            OnValueChange();
        });
    }

    private void SetRadioButtonValue(UIQueryRadioButtons radios, AbstractUIQueryResult result)
    {
        int selection = 0;
        while(!radios.options[selection].Equals(result.result)) {
            selection++;
        }
        radios.GetComponentsInChildren<Toggle>()[selection].isOn = true;
        //radios.GetComponentsInChildren<Toggle>()[selection].onValueChanged.Invoke(true);
        foreach (Toggle t in radios.GetComponentsInChildren<Toggle>())
        {
            t.onValueChanged.AddListener(e => {
                if(e){
                    result.result = radios.options[t.transform.GetSiblingIndex()];
                }
            });
        }
    }

    private void SetSliderValue(UIQuerySlider query, AbstractUIQueryResult result)
    {
        query.GetComponentInChildren<Slider>().value = int.Parse(result.result);
        query.GetComponentInChildren<Slider>().onValueChanged.AddListener(e => {
            result.result = "" + Mathf.RoundToInt(e);
        });
    }

    private void OnValueChange() {
        bool canSend = true;
        foreach (AbstractUIQuery uiQuery in GetComponentsInChildren<AbstractUIQuery>())
        {
            canSend = canSend && uiQuery.IsAccepted();
        }
        sendButton.interactable = canSend;
    }
}
