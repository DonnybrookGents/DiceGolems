using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerController : Character {
    public static readonly CombatState PRETURN = CombatState.PlayerPreTurn;
    public static readonly CombatState POSTTURN = CombatState.PlayerPostTurn;
    public static readonly CombatState OFFENSE = CombatState.PlayerMidTurn;
    public static readonly CombatState DEFENSE = CombatState.EnemyMidTurn;

    public List<Die> Bank;

    public PlayerController() {
        MaxHealth = 100;
        Health = 100;
        Debug.Log("player start");
        Bank = new List<Die> {
            new Die(),
            new Die(new int[] { 1, 1, 1, 6, 6, 6 })
        };
    }
}
