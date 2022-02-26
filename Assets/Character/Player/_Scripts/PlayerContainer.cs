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

        Tiles = new List<Tile>();
        foreach (TileContainer tileContainer in StartingTiles) {
            Tile tile = tileContainer.Copy();
            Tiles.Add(tile);
        }
    }

    public void CreateStartingBank() {
        Bank = new List<Die>();
        foreach (DieContainer dc in StartingBank) {
            // Die die = dc.Copy();
            // Bank.Add(die);
        }
    }

    public Dictionary<string, Tile> CopyTiles() {
        Dictionary<string, Tile> newTiles = new Dictionary<string, Tile>();

        foreach (TileContainer tile in StartingTiles) {
            Tile t = tile.Copy();
            newTiles.Add(t.UUID, t);
        }

        return newTiles;
    }

    public List<DieContainer> CopyBank() {
        List<DieContainer> newBank = new List<DieContainer>();

        foreach (DieContainer die in StartingBank) {
            newBank.Add(die);
        }

        return newBank;
    }
}
