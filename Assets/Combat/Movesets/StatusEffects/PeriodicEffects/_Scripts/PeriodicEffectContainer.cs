using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Periodic Effect", menuName = "ScriptableObjects/StatusEffects/PeriodicEffect")]
public class PeriodicEffectContainer : StatusEffectContainer, StatusEffectInterface {

    protected PeriodicEffectContainer(int priority) {
        Priority = priority;
    }

    public override void Execute(Character defenseCharacter, Character attackCharacter){
        overrideExecution.Execute(defenseCharacter, attackCharacter);
    }

}
