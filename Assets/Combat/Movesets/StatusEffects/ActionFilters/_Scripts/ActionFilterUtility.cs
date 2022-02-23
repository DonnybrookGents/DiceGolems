using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionFilterName { Confusion, Weak };

public class ActionFilterUtility {
    public static Dictionary<ActionFilterName, System.Type> filterOverrideDict = new Dictionary<ActionFilterName, System.Type>(){
        {ActionFilterName.Confusion, typeof(ConfusionOverride)},
        {ActionFilterName.Weak, typeof(WeakOverride)}
    };
}