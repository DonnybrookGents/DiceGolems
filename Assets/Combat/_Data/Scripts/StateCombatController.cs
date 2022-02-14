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
    public bool IsPlayerTurnEnded;

    private CombatController _Combat;
    private ZoneCombatController _Zones;
    private bool _IsStateReady;
    private Dictionary<CombatState, CombatState> _StateMap = new Dictionary<CombatState, CombatState> {
        { CombatState.Start, CombatState.PlayerPreTurn },
        { CombatState.PlayerPreTurn, CombatState.PlayerMidTurn },
        { CombatState.PlayerMidTurn, CombatState.PlayerPostTurn },
        { CombatState.PlayerPostTurn, CombatState.EnemyPreTurn },
        { CombatState.EnemyPreTurn, CombatState.EnemyMidTurn },
        { CombatState.EnemyMidTurn, CombatState.EnemyPostTurn },
        { CombatState.EnemyPostTurn, CombatState.PlayerPreTurn },
        { CombatState.Win, CombatState.Win },
        { CombatState.Lose, CombatState.Lose }
    };

    private void Start() {
        State = CombatState.Start;
        _Zones = GetComponent<ZoneCombatController>();
        _Combat = GetComponent<CombatController>();

        _IsStateReady = true;
    }

    private CombatState NextCombatState() {
        Debug.Log(State);

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

        // [ ] set player starting energy
        // [ ] copy player bank
        // [ ] load first enemy action

        _Combat.SetEnergy(5);

        _IsStateReady = true;
    }

    private void HandlePlayerPreTurnState() {
        _IsStateReady = false;

        // [ ] add per-turn energy
        // [ ] refresh tile charges
        // [ ] trigger damage over time
        // [ ] trigger enabled debuffs

        _Combat.UpdateEnergy();

        _IsStateReady = true;
    }

    private IEnumerator HandlePlayerMidTurnState() {
        _IsStateReady = false;

        // [X] driven by UI
        // [ ] lock input controlls

        yield return new WaitUntil(() => IsPlayerTurnEnded);
        IsPlayerTurnEnded = false;

        _IsStateReady = true;
    }

    private void HandlePlayerPostTurnState() {
        _IsStateReady = false;

        // [X] clear backend dice zones
        // [ ] countdown/clear status effects

        _Zones.Clear();
        _Zones.PrintDiceInZones();

        _IsStateReady = true;
    }

    private void HandleEnemyPreTurnState() {
        _IsStateReady = false;

        // [ ] trigger damage over time
        // [ ] trigger enabled debuffs

        _IsStateReady = true;
    }

    private void HandleEnemyMidTurnState() {
        _IsStateReady = false;

        // [ ] execute selected action

        _IsStateReady = true;
    }

    private void HandleEnemyPostTurnState() {
        _IsStateReady = false;

        // [ ] countdown/clear status effects
        // [ ] select enemy action (attack/defend)

        _IsStateReady = true;
    }

    private void HandleWinState() {
        _IsStateReady = false;

        // [ ] end combat
        // [ ] clear temporary negative effects
        // [ ] give reward
        // [ ] level up
        // [ ] exit scene

        _IsStateReady = true;
    }

    private void HandleLoseState() {
        _IsStateReady = false;

        // [ ] end combat
        // [ ] clear temporary negative effects
        // [ ] decrease lives

        _IsStateReady = true;
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
                State = NextCombatState();

                break;

            case CombatState.Lose:
                HandleLoseState();
                State = NextCombatState();

                break;
        }
    }
}