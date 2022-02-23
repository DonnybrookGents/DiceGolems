using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicEffect : StatusEffect{

    public PeriodicEffect(PeriodicEffectName name, int priority, int efficacy, int cooldown){
        Name = name;
        Priority = priority;
        Efficacy = efficacy;
        Cooldown = cooldown;
    }
    public PeriodicEffectName Name;
}
