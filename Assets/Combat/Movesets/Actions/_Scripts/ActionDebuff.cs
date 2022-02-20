using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "Debuff Action", menuName = "ScriptableObjects/Actions/Debuff")]
public class ActionDebuff : ScriptableObject, ActionInterface {

    public string Name;
    public string Description;
    public StatusEffect statusEffect;
    public int Efficacy;
    public int Cooldown;
    [Tooltip("Leave null for default execution")] public ActionOverride overrideExecution;

    public void ApplyDebuff(Character defenseCharacter){
        if (defenseCharacter.StatusEffects.ContainsKey(statusEffect.GetName())) {
            //defenseCharacter.StatusEffects[statusEffect.GetName()].Cooldown += Cooldown;
            //defenseCharacter.StatusEffects[statusEffect.GetName()].Efficacy += Efficacy;
        } else {
            defenseCharacter.StatusEffects.Add(statusEffect.GetName(), (StatusEffect)System.Activator.CreateInstance(statusEffect.GetType(), new System.Object[]{Cooldown, Efficacy, statusEffect.Priority} ));
        }
    }
    public void Execute(Character defenseCharacter, Character attackCharacter) {
        if (overrideExecution != null) {
            overrideExecution.Execute(defenseCharacter, attackCharacter);
        } else {
            ApplyDebuff(defenseCharacter);
        }
    }
}
