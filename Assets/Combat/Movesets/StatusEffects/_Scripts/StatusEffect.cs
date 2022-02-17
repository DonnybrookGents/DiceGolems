using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public List<CombatState> ExecutionStates;
    public List<CombatState> CountdownStates;
    public int Cooldown;
    public int Level;
    public int Priority;

    protected StatusEffect(List<CombatState> executionStates, List<CombatState> countdownStates, int cooldown, int level, int priority) {
        ExecutionStates = executionStates;
        CountdownStates = countdownStates;
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

    public void SetExecutionStates(List<CombatState> combatStates) {
        ExecutionStates = combatStates;
    }

    public void SetCountdownStates(List<CombatState> combatStates) {
        CountdownStates = combatStates;
    }

    public abstract void Execute(Character character, CombatState combatState);
    public abstract int Execute(int damage, CombatState combatState); // delete this

    public virtual void CountDown(CombatState combatState) {
        if (CountdownStates.Contains(combatState)) {
            Cooldown--;
            Debug.Log("Counting Down Status Effect: " + Cooldown);
        }
    }

    public abstract string GetName();

}
