using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Debuff Action", menuName = "ScriptableObjects/Actions/Debuff")]
public class ActionDebuffContainer : ActionContainer
{
    public StatusEffectContainer statusEffect;

    public StatusEffectType StatusEffectType;
    public int Efficacy;
    public int Cooldown;
}
