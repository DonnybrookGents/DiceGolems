using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class EnemyController : Character {
    public string Name;

    public ActionInterface QueuedAction;

    public abstract ActionInterface DecideAction();

    public void ExecuteQueuedAction(Character defenseCharacter, CombatState combatState) {
       // QueuedAction.Execute(defenseCharacter, this, combatState);
    }

    public override int TakeDamage(int initalDamage) {
        Health -= initalDamage;
        return initalDamage;
    }
    public override int Heal(int hp) {
        Health += hp;
        return hp;
    }

}