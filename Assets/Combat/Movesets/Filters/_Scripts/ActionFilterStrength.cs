using System.Collections.Generic;
using UnityEngine;

public class ActionFilterStrength : FilterTypeAttack {
    public static readonly string NAME = "Strength";

    public ActionFilterStrength(int cooldown, int priority = 5) : base(cooldown, priority) { }

    public override int Execute(int damage) {
        return damage + 5;
    }

    public override string GetName() {
        return NAME;
    }
}
