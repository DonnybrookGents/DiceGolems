using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : Character {


    public PlayerContainer PlayerData;
    [HideInInspector] public int MaxEnergy;
    [HideInInspector] public int EnergyRegeneration;
    [HideInInspector] public int Energy;
    [HideInInspector] public List<Die> Bank;

    public void CloneData() {
        MaxHealth = PlayerData.MaxHealth;
        Health = PlayerData.Health;
        EnergyRegeneration = PlayerData.EnergyRegeneration;
        MaxEnergy = PlayerData.MaxEnergy;
        Energy = PlayerData.StartingEnergy - PlayerData.EnergyRegeneration;
        Bank = PlayerData.CopyBank();
    }

    public Die GenerateDice() {
        if (Energy <= 0) {
            return null;
        }

        Die die = new Die();
        die.Roll();

        Energy--;

        return die;
    }
    //replace with tile List or Dictionary
    // public Dictionary<string, ActionInterface> Actions = new Dictionary<string, ActionInterface>(){
    //     //{ActionPlayerAttack.NAME, new ActionPlayerAttack()}
    // };

    public void SetEnergy(int energy) {
        Energy = energy;
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
}
