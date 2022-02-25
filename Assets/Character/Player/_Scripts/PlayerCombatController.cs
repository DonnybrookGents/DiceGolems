using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : Character {

    public static readonly string TAG = "Player";
    public PlayerContainer PlayerData;
    public Dictionary<string, Tile> Tiles;
    private int MaxEnergy;
    private int EnergyRegeneration;
    private int Energy;
    private List<Die> Bank;

    public void CloneData() {
        MaxHealth = PlayerData.MaxHealth;
        Health = PlayerData.Health;
        EnergyRegeneration = PlayerData.EnergyRegeneration;
        MaxEnergy = PlayerData.MaxEnergy;
        Energy = PlayerData.StartingEnergy - PlayerData.EnergyRegeneration;
        Tiles = PlayerData.CopyTiles();
        Bank = PlayerData.CopyBank();
    }

    public Die GenerateDice() {
        if (Energy <= 0) {
            return null;
        }

        // Die die = new Die();
        Die refferenceDie = Bank[Random.Range(0, Bank.Count)];
        Die die = new Die(refferenceDie);

        die.Roll();

        Energy--;

        return die;
    }

    public void PrintBank() {
        foreach (Die d in Bank) {
            Debug.Log(d.UUID);
        }
    }

    public void PrintTiles() {
        foreach (Tile t in Tiles.Values) {
            Debug.Log(t.TileName);
        }
    }
    public void PrintParentTiles() {
        Debug.Log("Parent Print");
        foreach (Tile t in PlayerData.Tiles) {
            Debug.Log(t.TileName);
        }
    }
    public void AddDie(Die die) {
        PlayerData.Bank.Add(die);
        Bank.Add(die);
    }

    public void AddTileRune(Tile tileRune) {
        PlayerData.Tiles.Add(tileRune);
        Tiles.Add(tileRune.UUID, tileRune);
    }

    public int GetEnergy() {
        return Energy;
    }

    public void UpdateEnergy() {
        Energy += EnergyRegeneration;
        if (Energy > MaxEnergy) {
            Energy = MaxEnergy;
        }
    }

    public override int Heal(int hp) {
        Health += hp;
        PlayerData.Health += hp;

        if (Health > MaxHealth) {
            Health = MaxHealth;
            PlayerData.Health = PlayerData.MaxHealth;
        }

        return hp;
    }

    public override int TakeDamage(int initalDamage) {
        Health -= initalDamage;
        PlayerData.Health -= initalDamage;
        return initalDamage;
    }

    public void Respawn() {
        PlayerData.Health = PlayerData.MaxHealth;

        CloneData();
    }
}
