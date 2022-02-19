using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool IsPlayerTurnEnded;
    public GameObject Player;
    public GameObject Enemy;

    private SceneController _SceneController;
    private PlayerController _PlayerController;
    private EnemyController _EnemyController;
    private CombatController _CombatController;
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
        _PlayerController = Player.GetComponent<PlayerController>();
        _EnemyController = Enemy.GetComponent<EnemyController>();
        _CombatController = GetComponent<CombatController>();
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

        // Copy the bank.
        _CombatController.CopyBank(_PlayerController.Bank);
        _UIController.GetBankInfo();

        // Set the energy level.
        _CombatController.SetEnergy(4);
        _UIController.GetEnergy();

        // Set play and energy level.
        _UIController.UpdatePlayerHealth();
        _UIController.UpdateEnemyHealth();

        // Select enemy name and first action.
        ActionInterface action = _EnemyController.DecideAction();
        _UIController.SetEnemyName(_EnemyController.Name);
        _UIController.UpdateEnemyAction(action.GetName());

        _IsStateReady = true;
    }

    private void HandlePlayerPreTurnState() {
        _IsStateReady = false;

        // [ ] refresh tile charges

        // Update the energy.
        _CombatController.UpdateEnergy();
        _UIController.GetEnergy();

        _PlayerController.HandleStatusEffect();

        _IsStateReady = true;
    }

    private IEnumerator HandlePlayerMidTurnState() {
        _IsStateReady = false;

        // [ ] lock input controlls

        // Wait for UI input.
        yield return new WaitUntil(() => IsPlayerTurnEnded);
        IsPlayerTurnEnded = false;

        if (_EnemyController.Health < 0) {
            IsVictorious = true;
        }

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

        _EnemyController.HandleStatusEffect();

        _IsStateReady = true;
    }

    private void HandleEnemyMidTurnState() {
        _IsStateReady = false;

        _EnemyController.ExecuteQueuedAction(_PlayerController, State);

        if (_PlayerController.Health < 0) {
            IsDead = true;
        }

        _UIController.UpdateEnemyHealth();
        _UIController.UpdatePlayerHealth();

        _IsStateReady = true;
    }

    private void HandleEnemyPostTurnState() {
        _IsStateReady = false;

        // [ ] countdown/clear status effects

        ActionInterface action = _EnemyController.DecideAction();
        _UIController.UpdateEnemyAction(action.GetName());

        _IsStateReady = true;
    }

    private void HandleWinState() {
        _IsStateReady = false;

        // [ ] clear temporary negative effects
        // [ ] give reward
        // [ ] level up

        _UIController.UpdateWinLose("Victory", Color.green);
        StartCoroutine(_SceneController.BackToOverworld());
    }

    private void HandleLoseState() {
        _IsStateReady = false;

        // [ ] clear temporary negative effects
        // [ ] decrease lives

        _UIController.UpdateWinLose("Defeat", Color.red);
        StartCoroutine(_SceneController.BackToOverworld());
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
                HandleWinState();

                break;

            case CombatState.Lose:
                HandleLoseState();

                break;
        }
    }
}
