using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public string Name;
    public string Description;
    public int Priority;
    public int Cooldown;
    public int Efficacy;

    public StatusEffectOverride executionOverride;

    public virtual void Execute(Character defenseCharacter, Character attackCharacter){
        executionOverride.Execute(defenseCharacter, attackCharacter);
    }
}
