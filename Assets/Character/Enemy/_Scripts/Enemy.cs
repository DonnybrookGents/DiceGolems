using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatCharacter {

    public int turnCounter = 1;

    public static readonly string TAG = "Enemy";

    public EnemyContainer EnemyData;
    [HideInInspector] public ActionContainer QueuedAction;
    [HideInInspector] public List<WeightedAction> Actions;

    public void CloneData() {
        Name = EnemyData.Name;
        Health = EnemyData.MaxHealth;
        MaxHealth = EnemyData.MaxHealth;
        //PeriodicEffects = new List<PeriodicEffect>(EnemyData.StartingPeriodicEffects);
        //ActionsFilters = new List<ActionFilter>(EnemyData.StartingActionFilters);
        Actions = new List<WeightedAction>(EnemyData.Actions);
    }

    public virtual ActionContainer QueueAction() {
        int weightSum = 0;
        foreach (WeightedAction action in Actions) {
            weightSum += action.Weight;
        }

        int rand = Random.Range(1, weightSum + 1);

        weightSum = 0;
        foreach (WeightedAction action in Actions) {
            weightSum += action.Weight;
            if (weightSum >= rand) {
                QueuedAction = action.Action;
                break;
            }
        }

        return QueuedAction;
    }

    public void ExecuteQueuedAction(CombatCharacter target) {
        GetComponent<Animator>().SetTrigger("Attack");

        System.Type t = ActionUtility.actionOverrideDict[QueuedAction.Name];
        ActionOverride o = (ActionOverride)System.Activator.CreateInstance(t);

        o.Execute(target, this, QueuedAction);
    }

    public override int Heal(int hp) {
        Health += hp;

        if (Health > MaxHealth) {
            Health = MaxHealth;
        }

        return hp;
    }

    public override int TakeDamage(int initalDamage) {
        Health -= initalDamage;
        return initalDamage;
    }

}