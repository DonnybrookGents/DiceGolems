using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum ActionType {
    Attack,
    Defend,
    Heal,
    None
}
public class EnemyController : MonoBehaviour {

    //outbound
    public MethodInfo QueuedEnemyMethod;
    public Object[] QueuedEnemyMethodParameters;

    public Dictionary<MethodInfo, ActionType> methodActionTypeMap;
    public int Health;
    public int MaxHealth;

    //methods corresponding to action types


    public void Start() {
        methodActionTypeMap = new Dictionary<MethodInfo, ActionType>(){
            {typeof(EnemyController).GetMethod("HeavyAttack"), ActionType.Attack},
            {typeof(EnemyController).GetMethod("WeakAttack"), ActionType.Attack}
        };
    }
    public int Heal() {
        int hp = Random.Range(1, 7);
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
            chosenMethod = typeof(EnemyController).GetMethod("HeavyAttack");
            QueuedEnemyMethodParameters = null;
        } else {
            chosenMethod = typeof(EnemyController).GetMethod("WeakAttack");
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

    //methods to apply things to self
}