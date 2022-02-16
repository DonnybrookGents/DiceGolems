
public class ActionPlayerAttack : ActionTypeAttack, ActionInterface {

    public static readonly string NAME = "Player Attack";
    public int DamageValue;

    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState) {
        defenseCharacter.TakeDamage(base.ApplyDamage(defenseCharacter, attackCharacter, Damage(DamageValue), combatState));
    }
}