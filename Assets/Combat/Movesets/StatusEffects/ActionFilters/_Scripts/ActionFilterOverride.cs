
public abstract class ActionFilterOverride {
    public virtual void Cooldown(CombatCharacter character, ActionFilter filter){
        filter.Cooldown--;
    }
    public virtual void IncreaseFilter(ActionFilter original, ActionFilter newFilter){
        original.Cooldown += newFilter.Cooldown;
    }
    public abstract System.Object Execute(System.Object obj, ActionFilter filter);
}
