using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //inbound
    public void ApplyInboundAction(ActionType action, System.Object[] genericParams) {
        ReactionMap[action].Invoke(this, genericParams);
    }

    private Dictionary<ActionType, MethodInfo> ReactionMap = new Dictionary<ActionType, MethodInfo>(){
        {ActionType.Attack, typeof(PlayerController).GetMethod("TakeDamage")}
    };


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
