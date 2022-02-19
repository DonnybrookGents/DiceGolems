using UnityEngine;
using System.Collections.Generic;
public class ActionPoison : ActionTypeDebuff, ActionInterface {
    public static readonly string NAME = "Poison";
    public int Cooldown;
    public int Level;

    public ActionPoison(int cooldown, int level) {
        Cooldown = cooldown;
        Level = level;
    }

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        Debug.Log("Executing Poison");

        if (defenseCharacter.StatusEffects.ContainsKey(StatusEffectPoison.NAME)) {
            defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Cooldown += Cooldown;
            defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Level += Level;
            Debug.Log("Adding to Poison: " + defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Cooldown);
        } else {
            defenseCharacter.StatusEffects.Add(StatusEffectPoison.NAME, new StatusEffectPoison(Cooldown, Level));
            Debug.Log("Adding new Poison: " + defenseCharacter.StatusEffects[StatusEffectPoison.NAME].Cooldown);
        }
    }
}