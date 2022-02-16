using System.Collections.Generic;

public abstract class StatusEffectShield : StatusEffect {
    public static readonly string NAME = "Shield";
    StatusEffectShield(List<CombatState> executionStates, List<CombatState> countdownStates, 
        int count, int level) : base(executionStates, countdownStates, count, level, 10) {}

    public override int Execute(int damage, CombatState combatState){
        if(!ExecutionStates.Contains(combatState)){
            return damage;
        }

        Level -= damage;
        if(Level < 0){
            return Level * -1;
        }
        return 0;
    }
    public override string GetName() {
        return NAME;
    }


}
