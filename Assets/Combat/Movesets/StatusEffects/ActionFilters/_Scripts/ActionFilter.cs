using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FilterType { AttackActor, AttackRecipient, DebuffActor, DebuffRecipient, BuffActor, BuffRecipient };
public class ActionFilter : StatusEffect {

    public ActionFilter(ActionFilterName name, FilterType type, int priority, int efficacy, int cooldown) {
        Name = name;
        Type = type;
        Priority = priority;
        Efficacy = efficacy;
        Cooldown = cooldown;
    }
    public ActionFilterName Name;
    public FilterType Type;

}
