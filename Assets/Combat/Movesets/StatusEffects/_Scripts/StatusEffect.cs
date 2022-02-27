using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType { PeriodicEffect, ActionFilter };
public class StatusEffect {
    public string FormattedName;
    [TextArea(3, 5)] public string Description;
    public int Priority;
    public int Efficacy;
    public int Cooldown;
    public Color Color;
}
