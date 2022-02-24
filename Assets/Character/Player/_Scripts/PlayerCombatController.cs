using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : Character {


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
