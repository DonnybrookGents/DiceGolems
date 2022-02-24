using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
public class TileContainer : ScriptableObject {
    public TileName TileName;
    public Sprite Image;
    public int DiceSlots;
    public int TileLevel;
    public List<TileParameter> TileParameters;

    public Tile Copy() {
        return new Tile(TileName, Image, DiceSlots, TileLevel, TileParameters);
    }
}
