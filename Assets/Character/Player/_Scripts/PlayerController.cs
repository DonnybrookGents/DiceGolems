using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerController : Character {

    public List<Die> Bank;

    protected override void Start() {
        Debug.Log("player start");
        base.Start();
        Bank = new List<Die> {
            new Die(),
            new Die(new int[] { 1, 1, 1, 6, 6, 6 })
        };
    }
}
