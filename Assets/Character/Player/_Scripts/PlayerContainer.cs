using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Instance", menuName = "ScriptableObjects/Player")]
public class PlayerContainer : ScriptableObject {

    public string Name;
    public int MaxHealth;
    public int Health;
    public int MaxEnergy;
    public int StartingEnergy;
    public int EnergyRegeneration;
    public List<TileContainer> Tiles;
    public List<DieContainer> Bank;

    public Dictionary<string, Tile> CopyTiles() {
        Dictionary<string, Tile> newTiles = new Dictionary<string, Tile>();

        foreach (TileContainer tileContainer in Tiles) {
            Tile tile = tileContainer.Copy();
            newTiles.Add(tile.UUID, tile);
        }

        return newTiles;
    }

    public List<Die> CopyBank() {
        List<Die> newBank = new List<Die>();

        foreach (DieContainer dc in Bank) {
            newBank.Add(dc.Copy());
        }

        return newBank;
    }
}
