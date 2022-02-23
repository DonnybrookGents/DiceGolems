using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
public class TileContainer : ScriptableObject {
    public TileName TileName;
    public int TileLevel;
    public List<TileParameter> TileParameters;
}
