using System.Collections.Generic;
using UnityEngine;

public enum FilterType {
    Attack,
    Defend
};

public abstract class ActionFilter {
    public FilterType Filter;
    public int Cooldown;
    public int Priority;

    protected ActionFilter(FilterType filter, int cooldown, int priority) {
        Filter = filter;
        Cooldown = cooldown;
        Priority = priority;
    }

    public virtual int AdjustCooldown(int amount) {
        Cooldown += amount;
        return Cooldown;
    }

    public abstract int Execute(int damage);

    public abstract string GetName();
}
