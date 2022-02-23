using System.Collections.Generic;

public enum PeriodicEffectName { Poison, Bleed };

public class PeriodicEffectUtility {
    public static Dictionary<PeriodicEffectName, System.Type> periodicOverrideDict = new Dictionary<PeriodicEffectName, System.Type>(){
        {PeriodicEffectName.Poison, typeof(PoisonOverride)},
        {PeriodicEffectName.Bleed, typeof(BleedOverride)}
    };
}