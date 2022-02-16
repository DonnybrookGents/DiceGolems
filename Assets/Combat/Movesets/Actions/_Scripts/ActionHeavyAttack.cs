
public class ActionHeavyAttack : ActionTypeAttack, ActionInterface {

    public static readonly string NAME = "Heavy Attack";

    public System.Tuple<int, int> DamageRange = System.Tuple.Create(10, 15);

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        defenseCharacter.TakeDamage(base.ApplyDamage(defenseCharacter, attackCharacter, Damage(), combatState));
    }

    private int Damage() {
        return base.Damage(DamageRange.Item1, DamageRange.Item2);
    }

}