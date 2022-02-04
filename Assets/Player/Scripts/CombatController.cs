using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public List<Die> Vat;
    public List<Die> Pool;
    public int Energy = 7;

    public void Awake() {
        Vat = new List<Die> {
            new Die(),
            new Die()
        };

        Pool = new List<Die>();
    }

    public void GenerateDice() {
        if (Energy > 0) {
            int index = Random.Range(0, Vat.Count);
            Die die = Vat[index];

            die.Roll();
            Pool.Add(die);

            Energy--;
        }
    }
}
