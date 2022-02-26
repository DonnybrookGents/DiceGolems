using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {
    public static readonly string TAG = "GameController";
    void Awake() {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Loading game");
    }
    void OnApplicationQuit() {
        Destroy(gameObject);
    }
}
