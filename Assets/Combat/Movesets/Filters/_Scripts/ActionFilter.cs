using System.Collections.Generic;
using UnityEngine;



public abstract class ActionFilter {
    public int Cooldown;
    public int Priority;

    protected ActionFilter(int cooldown, int priority) {
        Cooldown = cooldown;
        Priority = priority;
    }

    public abstract int Execute(int value);

    public abstract string GetName();
}
