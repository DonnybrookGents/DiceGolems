using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int Health;
    public int MaxHealth;
    public List<Die> Bank;

    private void Start() {
        Bank = new List<Die> {
            new Die(),
            new Die(new int[] { 1, 1, 1, 6, 6, 6 })
        };
    }

    public void TakeDamage(int hp) {
        Health -= hp;
    }

    public void Heal(int hp) {
        Health += hp;
    }
}
