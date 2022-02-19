using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class EnemyController : Character {
    public string Name;

    public static readonly CombatState PRETURN = CombatState.EnemyPreTurn;
    public static readonly CombatState POSTTURN = CombatState.EnemyPostTurn;
    public static readonly CombatState OFFENSE = CombatState.EnemyMidTurn;
    public static readonly CombatState DEFENSE = CombatState.PlayerMidTurn;
    public ActionInterface QueuedAction;

    public abstract ActionInterface DecideAction();

    public void ExecuteQueuedAction(Character defenseCharacter, CombatState combatState) {
        QueuedAction.Execute(defenseCharacter, this, combatState);
    }

}