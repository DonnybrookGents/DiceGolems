using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Character {
    public int Health;
    public int MaxHealth;

    public Dictionary<string, StatusEffect> StatusEffects = new Dictionary<string, StatusEffect>();
    public Dictionary<string, ActionFilter> ActionsFilters = new Dictionary<string, ActionFilter>();

    public int Heal(int hp) {
        Health += hp;

        return hp;
    }

    public void TakeDamage(int initalDamage) {
        int workingDamage = initalDamage;

        Health -= workingDamage;
    }

    public void HandleStatusEffect() {
        Dictionary<string, StatusEffect> newStatusEffects = new Dictionary<string, StatusEffect>();

        foreach (StatusEffect statusEffect in StatusEffects.Values) {
            statusEffect.Execute(this);
            statusEffect.CountDown();

            if (statusEffect.Cooldown > 0) {
                newStatusEffects.Add(statusEffect.GetName(), statusEffect);
            }
        }

        StatusEffects = newStatusEffects;
    }
}
