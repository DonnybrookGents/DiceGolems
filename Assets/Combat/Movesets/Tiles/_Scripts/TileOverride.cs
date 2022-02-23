using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TileOverride {
    public void Execute(Character defendingCharacter, Character attackingCharacter, List<Die> dice, Tile tile);
}
