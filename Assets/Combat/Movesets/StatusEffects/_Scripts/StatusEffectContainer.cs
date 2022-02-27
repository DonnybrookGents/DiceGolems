using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectContainer : ScriptableObject {
    public string FormattedName;
    [TextArea(3, 5)] public string Description;
    public int Priority;
    public Color Color;
}
