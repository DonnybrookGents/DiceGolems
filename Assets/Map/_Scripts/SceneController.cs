using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadOverWorld() {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Overworld");
    }
}
