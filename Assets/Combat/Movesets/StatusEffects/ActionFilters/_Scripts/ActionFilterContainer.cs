using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionFilter", menuName = "ScriptableObjects/StatusEffects/ActionFilter")]
public class ActionFilterContainer : StatusEffectContainer {
    public ActionFilterName Name;
    public FilterType Type;
}
