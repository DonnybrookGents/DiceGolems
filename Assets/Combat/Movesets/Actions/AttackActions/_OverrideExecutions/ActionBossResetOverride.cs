using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBossResetOverride : ActionOverride {
    public static int health = 0;
    public static List<ActionFilter> filters;
    public static List<PeriodicEffect> effects;
    public static bool checkpointset = false;
    public void Execute(CombatCharacter defensiveCharacter, CombatCharacter offensiveCharacter, ActionContainer action) {
        if (checkpointset) {
            offensiveCharacter.Health = health;
            offensiveCharacter.ActionFilters = new List<ActionFilter>(filters);
            offensiveCharacter.PeriodicEffects = new List<PeriodicEffect>(effects);
        }
        health = offensiveCharacter.Health;
        filters = new List<ActionFilter>(offensiveCharacter.ActionFilters);
        effects = new List<PeriodicEffect>(offensiveCharacter.PeriodicEffects);
        checkpointset = true;
    }
}
