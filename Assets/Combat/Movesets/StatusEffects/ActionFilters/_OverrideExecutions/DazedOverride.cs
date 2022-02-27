using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DazedOverride : ActionFilterOverride{
    public override System.Object Execute(System.Object obj, ActionFilter filter) {
        int missOdds = 9;
        if(filter.Efficacy == 1){
            missOdds = 8;
        }
        else if(filter.Efficacy == 2){
            missOdds = 4;
        }
        else if (filter.Efficacy == 3){
            missOdds = 2;
        }
        else if(filter.Efficacy >= 4){
            missOdds = 1;
        }
        int missRNG = Random.Range(1, 9);
        if(missRNG >= missOdds){
            filter.Cooldown = 0;
            return 0;
        }
        return (int)obj;
    }
    public override void Cooldown(CombatCharacter character, ActionFilter filter) {
        return;
    }
    public override void IncreaseFilter(ActionFilter original, ActionFilter newFilter) {
        original.Efficacy += newFilter.Efficacy;
    }
}
