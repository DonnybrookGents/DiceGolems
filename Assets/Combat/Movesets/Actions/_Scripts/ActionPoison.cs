using UnityEngine;
using System.Collections.Generic;
public class ActionPoison : ActionTypeDebuff, ActionInterface {
    public static readonly string NAME = "Poison";
    public int Cooldown;
    public int Level;

    public string GetName() {
        return NAME;
    }

    public ActionPoison(int cooldown, int level) {
        Cooldown = cooldown;
        Level = level;
    }

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        if (defenseCharacter.StatusEffects.ContainsKey(StatusEffectPoison.NAME)) {
            defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Cooldown += Cooldown;
            defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Level += Level;
        } else {
            defenseCharacter.StatusEffects.Add(StatusEffectPoison.NAME, new StatusEffectPoison(Cooldown, Level));
        }
    }
}