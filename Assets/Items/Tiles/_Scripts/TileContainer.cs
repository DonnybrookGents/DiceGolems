using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
public class TileContainer : ItemContainer {
    public TileName TileName;
    public string FormattedName;
    [TextArea(3, 5)] public string Description;
    public int DiceSlots;
    public int TileLevel;
    public int TileCharges;
    public List<TileParameter> TileParameters;
    public List<StatusEffectContainer> OptionalStatusEffects;

    public Tile Copy() {
        return new Tile(TileName, FormattedName, Description, Image, DiceSlots, TileLevel, TileCharges, TileParameters, OptionalStatusEffects);
    }
}
