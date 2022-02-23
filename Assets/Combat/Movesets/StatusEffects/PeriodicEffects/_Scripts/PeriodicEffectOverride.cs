
public abstract class PeriodicEffectOverride {

    public virtual void Cooldown(Character character, PeriodicEffect effect){
        effect.Cooldown--;
    }

    public abstract void Execute(Character character, PeriodicEffect effect);
}
