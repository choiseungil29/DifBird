using UnityEngine;
using System.Collections;

public struct Contents
{
    public string charName;
    public string description;

    public Contents(string charName, string description)
    {
        this.charName = charName;
        this.description = description;
    }
}