using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AvatarNameSynchronizer : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetAvatarName))]
    public string avatarName;

    private void SetAvatarName(string oldAvatarName, string newAvatarName)
    {
        GetComponentInChildren<TMPro.TextMeshProUGUI>().text = avatarName;
    }

    [Command]
    public void SetAvatarName(string newAvatarName)
    {
        avatarName = newAvatarName;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            SetAvatarName(ServerOnly.avatarName);
        }
    }
}
