using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect {
    public List<CombatState> ExecutionStates;
    public List<CombatState> CountdownStates;
    public int Count;
    public int Level;

    public int Priority;

    protected StatusEffect(List<CombatState> executionStates, List<CombatState> countdownStates, int count, int level, int priority) {
        ExecutionStates = executionStates;
        CountdownStates = countdownStates;
        Count = count;
        Level = level;
        Priority = priority;
    }

    public virtual int AdjustCount(int countAdjustment) {
        Count += countAdjustment;
        return Count;
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
    public abstract int Execute(int damage, CombatState combatState);

    public virtual void CountDown(CombatState combatState) {
        if (CountdownStates.Contains(combatState)) {
            Count--;
            Debug.Log("Counting Down Status Effect: " + Count);
        }
    }

    public abstract string GetName();

}
