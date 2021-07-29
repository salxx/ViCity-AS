using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDataPrepScript : AbstractPreparationScript
{
    public override void DoPreparation()
    {
        FindObjectOfType<SendToGoogle>().PostData();
    }
}
