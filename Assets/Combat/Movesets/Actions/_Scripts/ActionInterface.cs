using System.Collections.Generic;
public interface ActionInterface {
    public void Execute(Character defenseCharacter, Character attackCharacter, CombatState combatState);
    public string GetName();
}
