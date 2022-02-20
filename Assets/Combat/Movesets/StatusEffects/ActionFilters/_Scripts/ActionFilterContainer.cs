
public abstract class ActionFilterContainer : StatusEffectContainer {
    protected ActionFilterContainer(int priority) {
        Priority = priority;
    }

    public abstract int Execute(int value);
}
