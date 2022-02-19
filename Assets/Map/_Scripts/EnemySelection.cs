using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySelection : MonoBehaviour {
    public string EnemySceneName;

    public void LoadEnemy() {
        SceneManager.LoadScene(EnemySceneName);
    }
}
