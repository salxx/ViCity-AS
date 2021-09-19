using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AvatarNameValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button connectButton;

    public void ValidateName()
    {
        string avatarName = inputField.text;
        ServerOnly.avatarName = avatarName;
        connectButton.interactable = avatarName.Length >= 3 && avatarName.Length <= 15;
    }
}
