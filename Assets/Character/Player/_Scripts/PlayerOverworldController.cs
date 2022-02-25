using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldController {
    public PlayerContainer PlayerData;
    public Dictionary<string, Tile> Tiles;
    public int MaxHealth;
    public int Health;
    public int MaxEnergy;
    public int EnergyRegeneration;
    public int StartingEnergy;
    public List<Die> Bank;

    public void CloneData() {
        Debug.Log("Clone Data");
        MaxHealth = PlayerData.MaxHealth;
        Health = PlayerData.Health;
        EnergyRegeneration = PlayerData.EnergyRegeneration;
        MaxEnergy = PlayerData.MaxEnergy;
        StartingEnergy = PlayerData.StartingEnergy;
        Tiles = PlayerData.CopyTiles();
        Debug.Log("Tiles: " + Tiles.Count);
        Bank = PlayerData.CopyBank();
        Debug.Log("Bank: " + Bank.Count);
    }

    public void GenerateCombatPlayer(PlayerCombatController combatPlayer) {
        combatPlayer.PlayerData = this;
        combatPlayer.MaxHealth = MaxHealth;
        combatPlayer.Health = Health;
        combatPlayer.MaxEnergy = MaxEnergy;
        combatPlayer.Energy = StartingEnergy;
        combatPlayer.EnergyRegeneration = EnergyRegeneration;
        combatPlayer.Bank = new List<Die>();
        foreach (Die d in Bank) {
            combatPlayer.Bank.Add(d);
        }
        combatPlayer.Tiles = new Dictionary<string, Tile>();
        foreach (Tile t in Tiles.Values) {
            combatPlayer.Tiles.Add(t.UUID, t);
        }
    }

    public bool IsDead() {
        if (Health <= 0) {
            return true;
        }
        return false;
    }

    public void Clear() {
        Tiles.Clear();
        Bank.Clear();
        MaxHealth = 0;
        Health = 0;
        MaxEnergy = 0;
        EnergyRegeneration = 0;
        StartingEnergy = 0;
        PlayerData = null;
    }

    public void AddDie(Die d) {
        Bank.Add(d);
    }

    public void AddTile(Tile t) {
        Tiles.Add(t.UUID, t);
    }

    public void SetPlayerContainer(PlayerContainer pc) {
        PlayerData = pc;
    }
}
