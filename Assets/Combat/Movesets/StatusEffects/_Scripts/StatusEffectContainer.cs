using UnityEngine;


public abstract class StatusEffectContainer : ScriptableObject{

    public string Name;
    public string Description;
    public int Priority;
    public StatusEffectOverride overrideExecution;
    public virtual string GetName(){
        return Name;
    }
    public abstract void Execute(Character defenseCharacter, Character attackCharacter);
}