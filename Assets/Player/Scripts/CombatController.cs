using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public List<Die> Bank;
    public Dictionary<string, Die> Pool;
    public int Energy = 7;

    public void Awake() {
        Bank = new List<Die> {
            new Die(),
            new Die()
        };

        Pool = new Dictionary<string, Die>();
    }

    public Die GenerateDice() {
        if (Energy <= 0) {
            return null;
        }

        int index = Random.Range(0, Bank.Count);

        Die die = new Die(Bank[index].Faces);

        die.Roll();
        Pool.Add(die.UUID, die);

        Energy--;

        return die;
    }
}
