using System.Collections.Generic;
using UnityEngine;

public class EnemyPaul : EnemyController {
    private Dictionary<string, ActionInterface> _Actions = new Dictionary<string, ActionInterface>(){
        
    };


    public override ActionInterface DecideAction() {

        List<string> attacks = new List<string>(){
            //ActionLightAttack.NAME,
            //ActionLightAttack.NAME,
            // ActionConfusion.NAME,
            // ActionPoison.NAME
        };

        string action = attacks[Random.Range(0, attacks.Count)];
        QueuedAction = _Actions[action];


        return QueuedAction;
    }
}
