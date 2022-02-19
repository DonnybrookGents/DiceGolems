using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public int Cooldown;
    public int Level;
    public int Priority;

    protected StatusEffect(int cooldown, int level, int priority) {
        Cooldown = cooldown;
        Level = level;
        Priority = priority;
    }

    public virtual int AdjustCooldown(int countAdjustment) {
        Cooldown += countAdjustment;
        return Cooldown;
    }

    public virtual int AdjustLevel(int levelAdjustment) {
        Level += levelAdjustment;
        return Level;
    }

    public abstract void Execute(Character character);

    public virtual void CountDown() {
        Cooldown--;
        Debug.Log("Counting Down Status Effect: " + Cooldown);
    }

    public abstract string GetName();

}
