using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : CombatCharacter {

    public static readonly string TAG = "Player";
    public PlayerOverworldController PlayerData;
    public Dictionary<string, Tile> Tiles;
    public int MaxEnergy;
    public int EnergyRegeneration;
    public int Energy;
    public List<Die> Bank;

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

    public void AddCombatTile(Tile t) {
        Tiles.Add(t.UUID, t);
    }

    public void AddCombatDie(Die d) {
        Bank.Add(d);
    }

    public void AddDie(Die d) {
        PlayerData.AddDie(d);
        AddCombatDie(d);
    }
    public void AddTile(Tile t) {
        PlayerData.AddTile(t);
        AddCombatTile(t);
    }
}
