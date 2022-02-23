using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType {PeriodicEffect, ActionFilter};
public class StatusEffect {
    public int Priority;
    public int Efficacy;
    public int Cooldown;
}
