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
    public List<TileContainer> StartingTiles;
    public List<Tile> Tiles;
    public List<DieContainer> StartingBank;
    public List<Die> Bank;

    public void CreateStartingTiles() {

        Debug.Log("Create tiles");
        Tiles = new List<Tile>();
        foreach (TileContainer tileContainer in StartingTiles) {
            Tile tile = tileContainer.Copy();
            Tiles.Add(tile);
        }
    }

    public void CreateStartingBank() {
        Debug.Log("Create bank");
        Bank = new List<Die>();
        foreach (DieContainer dc in StartingBank) {
            Die die = dc.Copy();
            Bank.Add(die);
        }
    }

    public Dictionary<string, Tile> CopyTiles() {
        Dictionary<string, Tile> newTiles = new Dictionary<string, Tile>();

        foreach (Tile tile in Tiles) {
            newTiles.Add(tile.UUID, tile);
        }

        return newTiles;
    }

    public List<Die> CopyBank() {
        List<Die> newBank = new List<Die>();

        foreach (Die die in Bank) {
            newBank.Add(die);
        }

        return newBank;
    }
}
