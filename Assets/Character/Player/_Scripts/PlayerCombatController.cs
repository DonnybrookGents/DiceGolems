using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : CombatCharacter {

    public static readonly string TAG = "Player";
    public PlayerOverworldController PlayerData;
    public Dictionary<string, Tile> Tiles;
    public int MaxEnergy;
    public int EnergyRegeneration;
    public int Energy;
    public List<GameObject> Bank;

    public GameObject GenerateDie() {
        if (Energy <= 0) {
            return null;
        }
        GameObject diePrefab = Bank[Random.Range(0, Bank.Count)];
        GameObject newDiePrefab = Instantiate(diePrefab);
        newDiePrefab.name = diePrefab.name;
        newDiePrefab.GetComponent<Die>().Roll();
        Energy--;
        return newDiePrefab;
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

    public void AddCombatDie(GameObject die) {
        Bank.Add(die);
    }
}
