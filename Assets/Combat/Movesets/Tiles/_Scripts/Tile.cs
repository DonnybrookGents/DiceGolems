using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public TileName TileName;
    public int TileLevel;
    public List<TileParameter> TileParameters;
    public void CloneData(TileContainer tile){
        TileName = tile.TileName;
        TileLevel = tile.TileLevel;
        TileParameters = new List<TileParameter>(tile.TileParameters);
    }
}
