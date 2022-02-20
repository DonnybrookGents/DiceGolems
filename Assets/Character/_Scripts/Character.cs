using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [HideInInspector] public int Health;
    [HideInInspector] public int MaxHealth;

    public Dictionary<string, StatusEffect> StatusEffects = new Dictionary<string, StatusEffect>();
    public Dictionary<string, ActionFilter> ActionsFilters = new Dictionary<string, ActionFilter>();

    public abstract int Heal(int hp);

    public abstract int TakeDamage(int initalDamage);


    public void HandleStatusEffect() {
        Dictionary<string, StatusEffect> newStatusEffects = new Dictionary<string, StatusEffect>();

        foreach (StatusEffect statusEffect in StatusEffects.Values) {
            //statusEffect.Execute(this);
            //statusEffect.CountDown();

            // if (statusEffect.Cooldown > 0) {
            //     newStatusEffects.Add(statusEffect.GetName(), statusEffect);
            // }
        }

        StatusEffects = newStatusEffects;
    }
}
