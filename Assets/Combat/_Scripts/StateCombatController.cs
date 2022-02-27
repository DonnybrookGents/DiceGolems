using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState {
    Start,
    PlayerPreTurn,
    PlayerMidTurn,
    PlayerPostTurn,
    EnemyPreTurn,
    EnemyMidTurn,
    EnemyPostTurn,
    Win,
    Lose,
    None
};

public class StateCombatController : MonoBehaviour {
    public CombatState State;
    public bool IsDead;
    public bool IsVictorious;
    public bool IsStateControllerDriven;
    private SceneController _SceneController;
    private PlayerCombatController _PlayerCombatController;
    private Enemy _Enemy;
    private ZoneCombatController _ZonesController;
    private UICombatController _UIController;
    private bool _IsStateReady;
    private Dictionary<CombatState, CombatState> _StateMap = new Dictionary<CombatState, CombatState> {
        { CombatState.Start, CombatState.PlayerPreTurn },
        { CombatState.PlayerPreTurn, CombatState.PlayerMidTurn },
        { CombatState.PlayerMidTurn, CombatState.PlayerPostTurn },
        { CombatState.PlayerPostTurn, CombatState.EnemyPreTurn },
        { CombatState.EnemyPreTurn, CombatState.EnemyMidTurn },
        { CombatState.EnemyMidTurn, CombatState.EnemyPostTurn },
        { CombatState.EnemyPostTurn, CombatState.PlayerPreTurn }
    };

    private void Start() {
        State = CombatState.Start;

        _SceneController = GetComponent<SceneController>();
        _PlayerCombatController = GameObject.FindWithTag(PlayerCombatController.TAG).GetComponent<PlayerCombatController>();

        GameObject.FindGameObjectWithTag(DDOL.TAG).GetComponent<OverworldController>().overWorldPlayer.GenerateCombatPlayer(_PlayerCombatController);
        _Enemy = GameObject.FindWithTag(Enemy.TAG).GetComponent<Enemy>();
        _ZonesController = GetComponent<ZoneCombatController>();
        _UIController = GetComponent<UICombatController>();

        _IsStateReady = true;
    }

    private CombatState NextCombatState() {
        if (IsVictorious) {
            return CombatState.Win;
        }

        if (IsDead) {
            return CombatState.Lose;
        }

        return _StateMap[State];
    }

    private void HandleStartState() {
        _IsStateReady = false;

        // Load info.
        //_PlayerCombatController.CloneData();
        _Enemy.CloneData();
        _UIController.LoadTiles();
        _UIController.GetEnergy();

        // Set play and energy level.
        _UIController.UpdatePlayerHealth();
        _UIController.UpdateEnemyHealth();
        _UIController.SetBank();

        // Select enemy name and first action.
        //set enemy action, pass to ui
        ActionContainer action = _Enemy.QueueAction();
        _UIController.UpdateEnemyAction(action.Name.ToString());

        _IsStateReady = true;
    }

    private void HandlePlayerPreTurnState() {
        _IsStateReady = false;

        // [ ] refresh tile charges

        // Update the energy.
        _PlayerCombatController.UpdateEnergy();
        _UIController.GetEnergy();

        _PlayerCombatController.HandlePeriodicEffects();

        _UIController.UpdateEnemyHealth();
        _UIController.UpdatePlayerHealth();

        _UIController.UpdatePlayerStatusEffects(_PlayerCombatController);
        _UIController.UpdateEnemyStatusEffects(_Enemy);

        _IsStateReady = true;
    }

    private IEnumerator HandlePlayerMidTurnState() {
        _IsStateReady = false;


        // [ ] lock input controlls

        // Wait for UI input.
        _UIController.UnlockUI();
        yield return new WaitUntil(() => IsStateControllerDriven);
        _UIController.LockUI();

        IsStateControllerDriven = false;

        if (_Enemy.IsDead()) {
            IsVictorious = true;
        }

        yield return new WaitForSeconds(1);

        _IsStateReady = true;
    }

    private void HandlePlayerPostTurnState() {
        _IsStateReady = false;

        // Clear the dice zones.

        _ZonesController.Clear();

        _IsStateReady = true;
    }

    private void HandleEnemyPreTurnState() {
        _IsStateReady = false;

        _Enemy.HandlePeriodicEffects();

        _UIController.UpdateEnemyHealth();
        _UIController.UpdatePlayerHealth();

        _UIController.UpdatePlayerStatusEffects(_PlayerCombatController);
        _UIController.UpdateEnemyStatusEffects(_Enemy);

        _IsStateReady = true;
    }

    private void HandleEnemyMidTurnState() {
        _IsStateReady = false;

        _Enemy.ExecuteQueuedAction(_PlayerCombatController);

        if (_PlayerCombatController.IsDead() || _Enemy.IsDead()) {
            IsDead = true;
        }

        _UIController.UpdateEnemyHealth();
        _UIController.UpdatePlayerHealth();
        _UIController.UpdatePlayerStatusEffects(_PlayerCombatController);
        _UIController.UpdateEnemyStatusEffects(_Enemy);

        _IsStateReady = true;
    }

    private void HandleEnemyPostTurnState() {
        _IsStateReady = false;

        //[ ] countdown/clear status effects

        ActionContainer action = _Enemy.QueueAction();
        _UIController.UpdateEnemyAction(action.Name.ToString());

        _IsStateReady = true;
        _Enemy.turnCounter++;
    }

    private IEnumerator HandleWinState() {
        _UIController.UpdateWinLose("Victory", Color.green);
        _IsStateReady = false;

        _UIController.UnlockUI();
        _UIController.LoadRewardPopup();
        yield return new WaitUntil(() => IsStateControllerDriven);
        _UIController.LockUI();
        // [ ] clear temporary negative effects
        // [ ] give reward
        // [ ] level up
        OverworldController owController = GameObject.FindGameObjectWithTag(GameStateController.TAG).GetComponent<OverworldController>();
        owController.Wins++;
        //owController.Wins++;
        StartCoroutine(_SceneController.LoadOverWorld());
    }

    private void HandleLoseState() {
        _IsStateReady = false;

        // [ ] clear temporary negative effects
        // [ ] decrease lives

        _UIController.UpdateWinLose("Defeat", Color.red);

        //_PlayerCombatController.Respawn();

        StartCoroutine(_SceneController.LoadOverWorld());
    }


    private void Update() {
        if (!_IsStateReady) {
            return;
        }

        switch (State) {
            case CombatState.Start:
                HandleStartState();
                State = NextCombatState();

                break;

            case CombatState.PlayerPreTurn:
                HandlePlayerPreTurnState();
                State = NextCombatState();

                break;

            case CombatState.PlayerMidTurn:
                StartCoroutine(HandlePlayerMidTurnState());
                State = NextCombatState();

                break;

            case CombatState.PlayerPostTurn:
                HandlePlayerPostTurnState();
                State = NextCombatState();

                break;

            case CombatState.EnemyPreTurn:
                HandleEnemyPreTurnState();
                State = NextCombatState();

                break;

            case CombatState.EnemyMidTurn:
                HandleEnemyMidTurnState();
                State = NextCombatState();

                break;

            case CombatState.EnemyPostTurn:
                HandleEnemyPostTurnState();
                State = NextCombatState();

                break;

            case CombatState.Win:
                StartCoroutine(HandleWinState());

                break;

            case CombatState.Lose:
                HandleLoseState();

                break;
        }
    }
}
