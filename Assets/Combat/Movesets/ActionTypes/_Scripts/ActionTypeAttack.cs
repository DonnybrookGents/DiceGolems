using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTypeAttack : ActionType {

    public int ApplyDamage(Character defenseCharacter, Character attackCharacter, int damage, CombatState combatState){
        foreach(StatusEffect statusEffect in attackCharacter.StatusEffects.Values){
            damage = statusEffect.Execute(damage, combatState);
        }
        foreach(StatusEffect statusEffect in defenseCharacter.StatusEffects.Values){
            damage = statusEffect.Execute(damage, combatState);
        }
        return damage;
    }
    public virtual int Damage(int minDamage, int maxDamage) {
        return Damage(Random.Range(minDamage, maxDamage));
    }

    public virtual int Damage(int damage) {
        return damage;
    }
}
