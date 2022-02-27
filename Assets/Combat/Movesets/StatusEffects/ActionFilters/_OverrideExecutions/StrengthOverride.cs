using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthOverride : ActionFilterOverride {
    public override System.Object Execute(System.Object obj, ActionFilter filter) {
        int damage = (int)obj;
        damage += filter.Efficacy;

        return damage;
    }
    public override void IncreaseFilter(ActionFilter original, ActionFilter newFilter) {
        original.Efficacy += newFilter.Efficacy;
    }
    public override void Cooldown(CombatCharacter character, ActionFilter filter) {
        return;
    }
}
