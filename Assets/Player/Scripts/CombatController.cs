using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public List<Die> Bank;
    public int Energy = 7;

    public void Awake() {
        Bank = new List<Die> {
            new Die(),
            new Die(new int[] { 1, 1, 1, 6, 6, 6 })
        };
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

    //public moveDice

    //private findzonefromid
}
