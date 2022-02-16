using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Character : MonoBehaviour {
    public MethodInfo QueuedEnemyMethod;
    public Object[] QueuedEnemyMethodParameters;

    public Dictionary<MethodInfo, ActionType> methodActionTypeMap;

    private Dictionary<ActionType, MethodInfo> ReactionMap = new Dictionary<ActionType, MethodInfo>(){
        {ActionType.Attack, typeof(Character).GetMethod("TakeDamage")}
    };

    public int Health;
    public int MaxHealth;

    public void ApplyInboundAction(ActionType action, System.Object[] genericParams) {
        ReactionMap[action].Invoke(this, genericParams);
    }

    protected virtual void Start() {
        methodActionTypeMap = new Dictionary<MethodInfo, ActionType>(){
            {typeof(Character).GetMethod("HeavyAttack"), ActionType.Attack},
            {typeof(Character).GetMethod("WeakAttack"), ActionType.Attack}
        };
    }

    public int Heal(int hp) {
        Health += hp;

        return hp;
    }

    public void TakeDamage(int hp) {
        Health -= hp;
    }
    public int HeavyAttack() {
        int value = Random.Range(10, 15);
        Debug.Log("Calculated Damage: " + value);
        return value;
    }
    public int WeakAttack() {
        int value = Random.Range(1, 7);
        Debug.Log("Calculated Damage: " + value);
        return value;
    }
    public ActionType DecideAction() {

        MethodInfo chosenMethod;

        if (Random.Range(0, 2) == 0) {
            chosenMethod = typeof(Character).GetMethod("HeavyAttack");
            QueuedEnemyMethodParameters = null;
        } else {
            chosenMethod = typeof(Character).GetMethod("WeakAttack");
            QueuedEnemyMethodParameters = null;
        }

        Debug.Log("Chosen Method: " + chosenMethod.Name);

        QueuedEnemyMethod = chosenMethod;
        return methodActionTypeMap[chosenMethod];
    }

    public System.Object[] ExecuteQueuedAction() {
        System.Object invokedReturn = QueuedEnemyMethod.Invoke(this, QueuedEnemyMethodParameters);

        List<System.Object> objectList = new List<System.Object>();
        if (invokedReturn is ICollection) {
            foreach (System.Object obj in (ICollection)invokedReturn) {
                objectList.Add(obj);
            }
        } else {
            objectList.Add(invokedReturn);
        }

        System.Object[] result = new System.Object[objectList.Count];
        for (int i = 0; i < objectList.Count; i++) {
            result[i] = objectList[i];
        }
        return result;
    }

    public ActionType GetActionType() {
        return methodActionTypeMap[QueuedEnemyMethod];
    }

}
