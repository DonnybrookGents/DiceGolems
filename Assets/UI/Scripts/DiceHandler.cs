using UnityEngine;
using UnityEngine.UI;

public class DiceHandler : MonoBehaviour {
    public CombatController Combat;

    public void RollDice(Text value) {
        Combat.GenerateDice();
        value.text = Combat.Pool[Combat.Pool.Count - 1].Value.ToString();
    }
}
