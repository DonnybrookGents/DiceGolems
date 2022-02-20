using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Action", menuName = "ScriptableObjects/Actions/Attack")]
public class ActionAttack : ScriptableObject, ActionInterface {

    public string Name;
    public string Description;
    public int inclusiveMinDamage;
    public int exclusiveMaxDamage;

    [Tooltip("Leave null for default execution")] public ActionOverride overrideExecution;
    private int _CalculatedDamage;
    public int ApplyDamage(Character defenseCharacter, Character attackCharacter, int damage) {
        foreach (ActionFilter filter in attackCharacter.ActionsFilters.Values) {
            if (filter is FilterTypeAttack) {
                damage = filter.Execute(damage);
            }
        }

        foreach (ActionFilter filter in defenseCharacter.ActionsFilters.Values) {
            if (filter is FilterTypeDefense) {
                damage = filter.Execute(damage);
            }
        }

        return damage;
    }


    public void Execute(Character defenseCharacter, Character attackCharacter) {
        if (overrideExecution != null) {
            overrideExecution.Execute(defenseCharacter, attackCharacter);
        } else {
            defenseCharacter.TakeDamage(ApplyDamage(defenseCharacter, attackCharacter, _CalculatedDamage));
        }
    }

    public virtual int CalculateDamage() {
        _CalculatedDamage = Random.Range(inclusiveMinDamage, exclusiveMaxDamage);
        return _CalculatedDamage;
    }
}
