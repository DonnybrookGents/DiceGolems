using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public List<Die> Bank;
    public int Energy;
    public int TurnEnergy;

    public void CopyBank(List<Die> bank) {
        Bank = new List<Die>(bank);
    }

    public Die GenerateDice() {
        if (Energy <= 0) {
            return null;
        }

        int index = Random.Range(0, Bank.Count);

        Die die = new Die(Bank[index].Faces);
        die.Roll();

        Energy--;

        return die;
    }

    public void SetEnergy(int energy) {
        Energy = energy;
    }

    public void UpdateEnergy() {
        Energy += TurnEnergy;
    }
}
