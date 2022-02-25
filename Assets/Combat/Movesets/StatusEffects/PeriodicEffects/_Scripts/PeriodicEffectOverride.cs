
public abstract class PeriodicEffectOverride {

    public virtual void Cooldown(CombatCharacter character, PeriodicEffect effect){
        effect.Cooldown--;
    }

    public abstract void Execute(CombatCharacter character, PeriodicEffect effect);
}
