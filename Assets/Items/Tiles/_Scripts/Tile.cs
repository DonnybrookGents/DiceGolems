using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile {
    public TileName TileName;
    public string FormattedName;
    [TextArea(3, 5)] public string Description;
    public Sprite Image;
    public string UUID;
    public List<Die> Dice;
    public int DiceSlots;
    public int TileLevel;
    public int TileCharges;
    public List<TileParameter> TileParameters;
    public List<StatusEffectContainer> OptionalStatusEffects;

    public Tile(TileName tileName, string formattedName, string description, Sprite image, int diceSlots, int tileLevel, int tileCharges, List<TileParameter> tileParameters, List<StatusEffectContainer> optionalStatusEffects) {
        TileName = tileName;
        FormattedName = formattedName;
        Description = description;
        Image = image;
        DiceSlots = diceSlots;
        UUID = System.Guid.NewGuid().ToString();
        TileLevel = tileLevel;
        TileCharges = tileCharges;
        TileParameters = new List<TileParameter>(tileParameters);
        OptionalStatusEffects = new List<StatusEffectContainer>(optionalStatusEffects);
    }

    public Tile(TileContainer tile) : this(tile.TileName, tile.FormattedName, tile.Description, tile.Image, tile.DiceSlots, tile.TileLevel, tile.TileCharges, tile.TileParameters, tile.OptionalStatusEffects) { }
}
