using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileOverride {
    public void Execute(CombatCharacter defendingCharacter, CombatCharacter attackingCharacter, List<Die> dice, Tile tile);
}
