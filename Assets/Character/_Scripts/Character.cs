using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [HideInInspector] public string Name;
    [HideInInspector] public int Health;
    [HideInInspector] public int MaxHealth;

    [HideInInspector] public List<PeriodicEffect> PeriodicEffects = new List<PeriodicEffect>();
    [HideInInspector] public List<ActionFilter> ActionFilters = new List<ActionFilter>();

    public abstract int Heal(int hp);

    public abstract int TakeDamage(int initalDamage);

    public virtual void AddPeriodicEffect(PeriodicEffect effect) {
        foreach (PeriodicEffect pe in PeriodicEffects) {
            if (pe.Name == effect.Name) {
                pe.Cooldown += effect.Cooldown;
                return;
            }
        }
        PeriodicEffects.Add(effect);
    }

    public virtual void AddActionFilter(ActionFilter filter) {
        foreach (ActionFilter af in ActionFilters) {
            if (af.Name == filter.Name) {
                af.Cooldown += filter.Cooldown;
                return;
            }
        }
        ActionFilters.Add(filter);
    }


    public void HandlePeriodicEffects() {
        List<PeriodicEffect> newPeriodicEffects = new List<PeriodicEffect>();

        foreach (PeriodicEffect effect in PeriodicEffects) {

            System.Type t = PeriodicEffectUtility.periodicOverrideDict[effect.Name];
            PeriodicEffectOverride o = (PeriodicEffectOverride)System.Activator.CreateInstance(t);
            o.Execute(this, effect);
            o.Cooldown(this, effect);

            if (effect.Cooldown > 0) {
                newPeriodicEffects.Add(effect);
            }
        }

        PeriodicEffects = newPeriodicEffects;

        List<ActionFilter> newFilters = new List<ActionFilter>();

        foreach (ActionFilter filter in ActionFilters) {

            System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
            ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
            o.Cooldown(this, filter);

            if (filter.Cooldown > 0) {
                ActionFilters.Add(filter);
            }
        }

        ActionFilters = newFilters;
    }
}
