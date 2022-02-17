using System.Collections.Generic;

public abstract class StatusEffectPoison : StatusEffect {
    public static readonly string NAME = "Poison";
    StatusEffectPoison(List<CombatState> executionStates, List<CombatState> countdownStates, 
        int count, int level) : base(executionStates, countdownStates, count, level, 10) {}

    public override void Execute(Character character, CombatState combatState){
        if(!ExecutionStates.Contains(combatState)){
            return;
        }
        character.Health -= Level;
    }
    public override string GetName() {
        return NAME;
    }


}
