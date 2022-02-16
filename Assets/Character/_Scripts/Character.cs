using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Character {


    public int Health;
    public int MaxHealth;

    public Dictionary<string, StatusEffect> StatusEffects = new Dictionary<string, StatusEffect>();


    public int Heal(int hp) {
        Health += hp;

        return hp;
    }

    public void TakeDamage(int initalDamage) {
        int workingDamage = initalDamage;

        Health -= workingDamage;
    }
}
