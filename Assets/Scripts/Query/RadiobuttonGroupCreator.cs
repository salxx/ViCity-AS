using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiobuttonGroupCreator : MonoBehaviour
{
    public GameObject radiobuttonPrefab;
    int currentOption = -1;
    Action parentChangeListener;

    public void CreateGroup(string[] options, Action parentChangeListener)
    {
        this.parentChangeListener = parentChangeListener;
        foreach (string o in options)
        {
            GameObject child = Instantiate<GameObject>(radiobuttonPrefab, transform);
            child.GetComponentInChildren<Toggle>().onValueChanged.AddListener(e => ChangeListener(child.GetComponentInChildren<Toggle>(), e));
            child.GetComponentInChildren<Text>().text = o;
        }
    }

    public int GetCurrentOption()
    {
        return currentOption;
    }

    private void ChangeListener(Toggle self, bool newValue)
    {
        int siblingIndex = self.transform.GetSiblingIndex();
        if(siblingIndex == currentOption && newValue == false)
        {
            Debug.Log("click already chosen");
            self.isOn = true;
            return;
        }
        if(newValue == true)
        {
            currentOption = self.transform.GetSiblingIndex();
            foreach(Toggle t in GetComponentsInChildren<Toggle>())
            {
                if(t == self)
                {
                    // nothing
                } else
                {
                    t.isOn = false;
                }
            }
            parentChangeListener();
        }
    }
}
