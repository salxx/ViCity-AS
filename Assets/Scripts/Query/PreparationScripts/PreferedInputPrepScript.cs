using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferedInputPrepScript : AbstractPreparationScript
{
    public MouseInputPrepScript mouse;
    public WaypointInputPrepScript waypoint;
    public KeyboardInputPrepScript keyboard;

    public UIQueryRadioButtons decision;

    public override void DoPreparation()
    {
        int option = decision.GetComponentInChildren<RadiobuttonGroupCreator>().GetCurrentOption();
        switch(option)
        {
            case 0:
                keyboard.allow = true;
                keyboard.DoPreparation();
                break;
            case 1:
                mouse.allow = true;
                mouse.DoPreparation();
                break;
            case 2:
                waypoint.allow = true;
                waypoint.DoPreparation();
                break;
        }
    } 
}
