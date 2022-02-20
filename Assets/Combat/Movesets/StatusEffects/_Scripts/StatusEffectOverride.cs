using UnityEngine;

public abstract class StatusEffectOverride : MonoBehaviour, StatusEffectInterface {
    public abstract void Execute(Character defenseCharacter, Character attackCharacter);
}
