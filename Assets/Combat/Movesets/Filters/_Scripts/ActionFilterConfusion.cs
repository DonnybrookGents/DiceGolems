using System.Collections.Generic;
using UnityEngine;

public class ActionFilterConfusion : ActionFilter {
    public static readonly string NAME = "Confusion";

    public ActionFilterConfusion(int cooldown, int priority = 10) : base(FilterType.Attack, cooldown, priority) { }

    public override int Execute(int damage) {
        Debug.Log("Rolling for confusion...");

        if (Random.Range(0, 2) == 0) {
            Debug.Log("Double!");
            return damage * 2;
        }

        Debug.Log("Zero!");
        return 0;
    }

    public override string GetName() {
        return NAME;
    }
}
