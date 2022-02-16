
using System.Collections.Generic;
public class ActionConfusion : ActionTypeDebuff, ActionInterface {

    public static readonly string NAME = "Confusion";
    public static readonly int COUNT = 2;
    public int Turns;
    public ActionConfusion(int turns){
        Turns = turns;
    }

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        
        if(defenseCharacter.StatusEffects.ContainsKey(StatusEffectConfusion.NAME)){
            defenseCharacter.StatusEffects[StatusEffectConfusion.NAME].Count += COUNT;
        }
        else{
            defenseCharacter.StatusEffects.Add(StatusEffectConfusion.NAME, 
                new StatusEffectConfusion(new List<CombatState>(){CombatState.PlayerMidTurn}, new List<CombatState>(){CombatState.PlayerPostTurn}, COUNT)
            );
        }
    }

    // private int Debuff() {
    //     return base.Damage(DamageRange.Item1, DamageRange.Item2);
    // }

}