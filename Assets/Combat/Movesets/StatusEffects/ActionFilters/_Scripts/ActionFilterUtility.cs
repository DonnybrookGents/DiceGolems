using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionFilterName { Confusion, Weak, Shield };

public enum FilterType { AttackActor, AttackRecipient, DebuffActor, DebuffRecipient, BuffActor, BuffRecipient, SupportActor, SupportRecipient };
//attacks (damage) and support (healing) expect an int
//debuff and buff expect a status effect

public class ActionFilterUtility {

    public static Dictionary<ActionFilterName, System.Type> filterOverrideDict = new Dictionary<ActionFilterName, System.Type>(){
        {ActionFilterName.Confusion, typeof(ConfusionOverride)},
        {ActionFilterName.Weak, typeof(WeakOverride)},
        {ActionFilterName.Shield, typeof(ShieldOverride)}
    };

    public static System.Object ApplyFiltersOfType(System.Object obj, List<ActionFilter> actionFilters, FilterType applicableFilterType) {
        actionFilters.Sort((a1, a2) => a1.Priority.CompareTo(a2.Priority));
        List<ActionFilter> filters = new List<ActionFilter>();
        foreach (ActionFilter filter in actionFilters) {
            if (filter.Type == applicableFilterType) {
                System.Type t = ActionFilterUtility.filterOverrideDict[filter.Name];
                ActionFilterOverride o = (ActionFilterOverride)System.Activator.CreateInstance(t);
                obj = o.Execute(obj, filter);
            }
        }
        return obj;
    }
}