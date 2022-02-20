using UnityEngine;

public abstract class ActionOverride : MonoBehaviour, ActionInterface {
    public abstract void Execute(Character defenseCharacter, Character attackCharacter);
}
