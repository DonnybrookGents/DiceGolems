using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyContainer : ScriptableObject {
    public string Name;
    public int MaxHealth;
    public WeightedAction[] Actions;

    public PeriodicEffect[] StartingPeriodicEffects;
    public ActionFilter[] StartingActionFilters;

}
