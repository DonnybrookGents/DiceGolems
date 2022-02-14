using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int Health;
    public int MaxHealth;

    public bool GetAction() {
        return Random.Range(0, 2) == 0;
    }

    public void TakeDamage(int hp) {
        Health -= hp;
    }

    public int Attack() {
        return Random.Range(1, 7);
    }

    public int Heal() {
        int hp = Random.Range(1, 7);
        Health += hp;

        return hp;
    }
}
