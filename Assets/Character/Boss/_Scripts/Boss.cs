using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    int resetCount = 0;
    int resetCountMeta = 0;
    public override ActionContainer QueueAction() {
        ActionContainer checkpoint = Actions[0].Action;
        if(resetCount == resetCountMeta){
            resetCount = 0;
            resetCountMeta++;
            QueuedAction = checkpoint;
            return checkpoint;
        }
        else{
            resetCount++;
        }

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
}
