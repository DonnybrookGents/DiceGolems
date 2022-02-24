using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakOverride : ActionFilterOverride {
    public override System.Object Execute(System.Object obj, ActionFilter filter) {
        int damage = (int)obj;

        return damage - filter.Efficacy;
    }
}
