using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionName {LightAttck, HeavyAttack, GivePoison, GiveWeak};
public class ActionUtility : MonoBehaviour
{
    public static Dictionary<ActionName, System.Type> actionOverrideDict = new Dictionary<ActionName, System.Type>(){
        {ActionName.LightAttck, typeof(ActionAttackOverride)},
        {ActionName.HeavyAttack, typeof(ActionAttackOverride)},
        {ActionName.GivePoison, typeof(ActionDebuffOverride)},
        {ActionName.GiveWeak, typeof(ActionDebuffOverride)}
    };
}
