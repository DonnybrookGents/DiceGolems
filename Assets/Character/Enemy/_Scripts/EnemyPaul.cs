using System.Collections.Generic;
using UnityEngine;

public class EnemyPaul : EnemyController {

    private Dictionary<string, ActionInterface> _Actions = new Dictionary<string, ActionInterface>(){
        {ActionLightAttack.Name, new ActionLightAttack()}
    };

    public override ActionInterface DecideAction() {

        List<string> attacks = new List<string>(){
            ActionLightAttack.Name,
            ActionLightAttack.Name,
            ActionHeavyAttack.Name
        };
        string action = attacks[Random.Range(0, attacks.Count)];
        Debug.Log("Beast is going to " + action);
        QueuedAction = _Actions[action];
        return QueuedAction;
    }

}
