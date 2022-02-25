using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PreLoad, Splash, MainMenu, PauseMenu, Combat, Overworld, GameOver }
public class GameStateController : MonoBehaviour {
    public GameState State;
    public bool _IsStateReady;

    public SceneController SceneCont;

    public void Awake() {
        State = GameState.PreLoad;
        _IsStateReady = true;
    }

    public void HandlePreLoadState() {
        Debug.Log("Preload Scene");
        _IsStateReady = false;
        StartCoroutine(SceneCont.LoadOverWorld());
    }


    private void Update() {
        if (!_IsStateReady) {
            return;
        }

        switch (State) {
            case GameState.PreLoad:
                HandlePreLoadState();
                break;
        }
    }
}
