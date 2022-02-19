using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionTypeAttack : ActionType {
    public int ApplyDamage(Character defenseCharacter, Character attackCharacter, int damage, CombatState combatState) {
        foreach (ActionFilter filter in attackCharacter.ActionsFilters.Values) {
            if (filter is FilterTypeAttack) {
                damage = filter.Execute(damage);
            }
        }

        foreach (ActionFilter filter in defenseCharacter.ActionsFilters.Values) {
            if (filter is FilterTypeDefense) {
                damage = filter.Execute(damage);
            }
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
