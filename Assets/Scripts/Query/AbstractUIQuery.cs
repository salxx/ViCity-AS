using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbstractUIQueryResult
{
    public string query;
    public string result;
}

public abstract class AbstractUIQuery : MonoBehaviour
{
    public bool required = false;

    public abstract AbstractUIQueryResult GetResult();

    public abstract bool IsAccepted();
}
