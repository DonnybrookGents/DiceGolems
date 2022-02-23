
public abstract class ActionFilterOverride {
    public virtual void Cooldown(Character character, ActionFilter filter){
        filter.Cooldown--;
    }
    public abstract System.Object Execute(System.Object obj, ActionFilter filter);
}
