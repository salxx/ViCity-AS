using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUIQuery : MonoBehaviour
{
    public bool required = false;

    public abstract string GetResult();

    public abstract bool IsAccepted();
}
