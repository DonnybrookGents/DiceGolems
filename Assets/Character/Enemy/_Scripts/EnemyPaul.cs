using System.Collections.Generic;
using UnityEngine;

public class EnemyPaul : EnemyController {

    public EnemyPaul() {
        MaxHealth = 40;
        Health = 40;
    }

    private Dictionary<string, ActionInterface> _Actions = new Dictionary<string, ActionInterface>(){
        {ActionLightAttack.NAME, new ActionLightAttack()},
        {ActionConfusion.NAME, new ActionConfusion(3)}
    };

    public override ActionInterface DecideAction() {
        List<string> attacks = new List<string>(){
            ActionLightAttack.NAME,
            ActionLightAttack.NAME,
            ActionConfusion.NAME
        };

        string action = attacks[Random.Range(0, attacks.Count)];
        Debug.Log("Paul is going to use" + action);
        QueuedAction = _Actions[action];
        return QueuedAction;
    }

}
