using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOverride : ActionFilterOverride {
    public override System.Object Execute(System.Object obj, ActionFilter filter) {
        int damage = (int)obj;
        int shield = filter.Efficacy;

        if(damage > shield){
            damage -= shield;
            shield = 0;
        }
        else{
            damage = 0;
            shield -= damage;
        }

        filter.Efficacy = shield;

        return damage;
    }
    public override void IncreaseFilter(ActionFilter original, ActionFilter newFilter) {
        original.Efficacy += newFilter.Efficacy;
    }
}

