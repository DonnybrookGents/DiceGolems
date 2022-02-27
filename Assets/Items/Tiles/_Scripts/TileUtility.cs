using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileName { BasicAttack, Heal, LeechLife, Shield, Hex, Exploit, FullCounter};
public class TileUtility {
    public static Dictionary<TileName, System.Type> TileOverrideDict = new Dictionary<TileName, System.Type>() {
        {TileName.BasicAttack, typeof(TileAttackOverride)},
        {TileName.Heal, typeof(TileHealOverride)},
        {TileName.LeechLife, typeof(TileLeechLifeOverride)},
        {TileName.Shield, typeof(TileShieldOverride)},
        {TileName.Hex, typeof(TileHexOverride)}
    };
}
