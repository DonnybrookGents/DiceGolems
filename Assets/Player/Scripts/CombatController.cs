using System.Collections.Generic;
using UnityEngine;

public class CombatController {
    public List<Die> Generator;
    public List<Die> Pool;

    public void GenerateDice() {
        int index = Random.Range(0, Generator.Count);
        Die die = Generator[index];

        die.Roll();
        Pool.Add(die);
    }
}
