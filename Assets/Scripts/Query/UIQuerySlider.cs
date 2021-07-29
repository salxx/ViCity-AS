using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIQuerySlider : AbstractUIQuery
{
    public string query = "Gender";
    public string queryEng = "Gender";
    public Slider slider;
    public Text sliderValueText;
    public int sliderValue;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        sliderValueText = slider.GetComponentsInChildren<Text>()[slider.GetComponentsInChildren<Text>().Length - 1];
        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(e => SliderChanged());
        GetComponent<TextMeshProUGUI>().text = (LanguageSelection.lang == 0 ? query : queryEng) + (required ? "*" : "");
        sliderValue = Mathf.RoundToInt(slider.value);
    }

    public override AbstractUIQueryResult GetResult()
    {
        AbstractUIQueryResult result = new AbstractUIQueryResult();
        result.query = query;
        result.result = "" + sliderValue;
        return result;
    }

    public void SliderChanged()
    {
        sliderValue = Mathf.RoundToInt(slider.value);
        sliderValueText.text = "" + sliderValue;
    }

    public override bool IsAccepted()
    {
        return true;
    }
}
