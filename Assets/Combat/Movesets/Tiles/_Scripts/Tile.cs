using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public TileName TileName;
    public Sprite Image;
    public string UUID;
    public List<Die> Dice;
    public int DiceSlots;
    public int TileLevel;
    public List<TileParameter> TileParameters;

    public Tile(TileName tileName, Sprite image, int diceSlots, int tileLevel, List<TileParameter> tileParameters) {
        TileName = tileName;
        Image = image;
        DiceSlots = diceSlots;
        UUID = System.Guid.NewGuid().ToString();
        TileLevel = tileLevel;
        TileParameters = new List<TileParameter>(tileParameters);
    }

    public Tile(TileContainer tile) : this(tile.TileName, tile.Image, tile.DiceSlots, tile.TileLevel, tile.TileParameters) { }
}
