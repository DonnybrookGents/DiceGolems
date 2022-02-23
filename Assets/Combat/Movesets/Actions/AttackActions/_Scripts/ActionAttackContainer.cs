using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Action", menuName = "ScriptableObjects/Actions/Attack")]
public class ActionAttackContainer : ActionContainer{
    public int InclusiveMinDamage;
    public int ExclusiveMaxDamage;
}