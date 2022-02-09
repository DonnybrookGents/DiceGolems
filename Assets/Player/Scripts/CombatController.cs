using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {
    public List<Die> Bank;
    public Dictionary<string, Die> Table;
    public Dictionary<string, Die> Pool;
    public Dictionary<string, Die> AttackTile;
    public Dictionary<string, Die> DefenseTile;
    public int Energy = 7;

    public void Awake() {
        Bank = new List<Die> {
            new Die(),
            new Die()
        };

        Table = new Dictionary<string, Die>();
        Pool = new Dictionary<string, Die>();
        AttackTile = new Dictionary<string, Die>();
        DefenseTile = new Dictionary<string, Die>();
    }

    public Die GenerateDice() {
        if (Energy <= 0) {
            return null;
        }

        int index = Random.Range(0, Bank.Count);

        Die die = new Die(Bank[index].Faces);

        die.Roll();
        Table.Add(die.UUID, die);
        Pool.Add(die.UUID, die);

        Energy--;

        return die;
    }
}
