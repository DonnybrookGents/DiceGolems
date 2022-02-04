using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public List<Die> Vat;
    public List<Die> Pool;

    public void Start() {
        Vat = new List<Die> {
            new Die(),
            new Die()
        };

        Pool = new List<Die>();
    }

    public void GenerateDice() {
        int index = Random.Range(0, Vat.Count);
        Die die = Vat[index];

        Debug.Log(die.Roll());
        Pool.Add(die);
    }
}
