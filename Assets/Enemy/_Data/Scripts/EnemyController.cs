using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int Health;
    public int MaxHealth;

    public void TakeDamage(int hp) {
        Health -= hp;
    }

    public void Heal(int hp) {
        Health += hp;
    }
}
