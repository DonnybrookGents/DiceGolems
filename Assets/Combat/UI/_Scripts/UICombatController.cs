using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum UIState {
    Selected,
    Deselected,
    None
}

public class UICombatController : MonoBehaviour {
    public Canvas HUD;
    public Slider PlayerHealth;
    public Slider EnemyHealth;
    public Text EnemyAction;
    public Text WinLose;
    public Text EnergyLevel;
    public Transform DicePool;
    public Die SelectedDie;
    public RectTransform TileContainer;
    public RectTransform TileTemplate;
    public RectTransform DiceTemplate;
    public RectTransform PopupTemplate;
    public GameObject RewardTemplate;

    public List<ItemContainer> Rewards;

    private bool UILocked = true;
    private PlayerCombatController _PlayerCombatController;
    private Enemy _Enemy;
    private ZoneCombatController _ZonesController;
    private StateCombatController _StateController;
    private UIState _SelectedState = UIState.Deselected;

    public void Start() {
        _StateController = GetComponent<StateCombatController>();
        _PlayerCombatController = GameObject.FindWithTag(PlayerCombatController.TAG).GetComponent<PlayerCombatController>();
        _Enemy = GameObject.FindWithTag(Enemy.TAG).GetComponent<Enemy>();
        _ZonesController = GetComponent<ZoneCombatController>();
        LockUI();
    }

    public void UpdatePlayerHealth() {
        PlayerHealth.value = (float)_PlayerCombatController.Health / (float)_PlayerCombatController.MaxHealth;
    }

    public void UpdateEnemyHealth() {
        EnemyHealth.value = (float)_Enemy.Health / (float)_Enemy.MaxHealth;
    }

    public void UpdateEnemyAction(string action) {
        EnemyAction.text = _Enemy.Name + " is going to use " + action.ToUpper();
    }

    public void UpdateWinLose(string text, Color color) {
        WinLose.text = text;
        WinLose.color = color;
    }

    public void LockUI() {
        UILocked = true;
    }

    public void UnlockUI() {
        UILocked = false;
    }

    public void GetEnergy() {
        EnergyLevel.text = _PlayerCombatController.GetEnergy().ToString();

        if (_PlayerCombatController.GetEnergy() == 0) {
            EnergyLevel.color = Color.red;
        } else {
            EnergyLevel.color = Color.white;
        }
    }

    public void LoadTiles() {
        float offset = 0;
        float padding = 20;

        foreach (Tile tile in _PlayerCombatController.Tiles.Values) {
            RectTransform newTile = Instantiate<RectTransform>(TileTemplate, TileContainer, false);
            newTile.localPosition = new Vector2(-offset, 0);

            newTile.GetComponent<UICombatTile>().TileUUID = tile.UUID;
            LoadTileDiceSlots(newTile, tile.DiceSlots);

            _ZonesController.AddZone(tile.UUID);
            newTile.Find("Send").GetComponent<Button>().onClick.AddListener(() => ActivateTile(newTile.Find("DicePlaceholder")));
            newTile.Find("Rune").GetComponent<Image>().sprite = tile.Image;

            // RectTransform tooltip = newTile.Find("Tooltip") as RectTransform;
            // tooltip.Find("Name").GetComponent<Text>().text = 

            offset += newTile.sizeDelta.x + padding;
        }

        _ZonesController.AddZone(DicePool.GetComponent<UICombatTile>().TileUUID);
    }

    public void LoadTileDiceSlots(RectTransform tile, int slots) {
        RectTransform tileDiceContainer = tile.Find("DicePlaceholder") as RectTransform;
        float offset = tileDiceContainer.sizeDelta.x / (slots + 1);

        for (int i = 0; i < slots; i++) {
            RectTransform newDice = Instantiate<RectTransform>(DiceTemplate);
            newDice.SetParent(tile.Find("DicePlaceholder"), false);
            newDice.localPosition = new Vector2(-offset * (i + 1), 0);

            newDice.GetComponent<Button>().onClick.AddListener(() => MoveDice(newDice.GetComponent<UICombatDiceSlot>()));
        }
    }

    public void RollDice() {
        if (UILocked) {
            return;
        }
        if (_PlayerCombatController.GetEnergy() > 0) {
            UICombatDiceSlot poolSlot = GetEmptyPoolDiceSlot();

            if (poolSlot == null) {
                return;
            }

            GameObject rolledDie = _PlayerCombatController.GenerateDie();

            poolSlot.Set(rolledDie.GetComponent<RectTransform>());
            //_ZonesController.AddDie(DicePool.GetComponent<UICombatTile>().TileUUID, rolledDie);
        }

        GetEnergy();
    }

    public void MoveDice(UICombatDiceSlot slot) {
        if (UILocked) {
            return;
        }
        switch (_SelectedState) {
            case UIState.Selected:
                SetSlot(slot);
                break;
            case UIState.Deselected:
                SelectDice(slot);
                break;
        }
    }

    private void SelectDice(UICombatDiceSlot diceSlot) {
        if (_SelectedState == UIState.Selected || diceSlot.DieUUID == "") {
            return;
        }

        SelectedDie = _ZonesController.GetDie(diceSlot.DieUUID);

        _SelectedState = UIState.Selected;
    }

    private void SetSlot(UICombatDiceSlot toSlot) {
        // if (_SelectedState != UIState.Selected || SelectedDie.UUID == toSlot.DieUUID) {
        //     return;
        // }

        // string toZone = toSlot.GetComponentInParent<UICombatTile>().TileUUID;

        // Die tempSlot = _ZonesController.GetDie(toSlot.DieUUID);
        // UICombatDiceSlot fromSlot = GetAllDiceSlot(SelectedDie.UUID);

        // if (tempSlot == null) {
        //     _ZonesController.MoveDie(toZone, SelectedDie.UUID);
        // } else {
        //     _ZonesController.SwapDice(tempSlot.UUID, SelectedDie.UUID);
        // }

        // string fromZone = fromSlot.GetComponentInParent<UICombatTile>().TileUUID;
        // fromSlot.Clear();

        // if (tempSlot != null) {
        //     fromSlot.Set(tempSlot);
        //     toSlot.Clear();
        // }

        // toSlot.Set(SelectedDie);

        // SelectedDie = null;
        // _SelectedState = UIState.Deselected;
    }

    public void ActivateTile(Transform slotParent) {

        string tileZone = slotParent.GetComponentInParent<UICombatTile>().TileUUID;
        Tile tile = _PlayerCombatController.Tiles[tileZone];
        Debug.Log("TileCharges: " + tile.TileCharges);
        if (tile.TileCharges == 0) {
            return;
        }

        List<Die> dice = new List<Die>();
        foreach (Die die in slotParent.GetComponentsInChildren<Die>()) {
            dice.Add(die);
            Destroy(die.gameObject);
        }

        if (dice.Count > 0) {
            _PlayerCombatController.GetComponent<Animator>().SetTrigger("Attack");
        }

        System.Type t = TileUtility.TileOverrideDict[tile.TileName];
        TileOverride o = System.Activator.CreateInstance(t) as TileOverride;
        o.Execute(_Enemy, _PlayerCombatController, dice, tile);

        UpdateEnemyHealth();
        UpdatePlayerHealth();

        if (_Enemy.IsDead() || _PlayerCombatController.IsDead()) {
            EndTurn();
        }

    }

    public void GrantReward(ItemContainer reward) {

        Debug.Log("GrantReward");

        PlayerOverworldController overworldPlayer = GameObject.FindGameObjectWithTag(DDOL.TAG).GetComponent<OverworldController>().overWorldPlayer;

        if (reward.GetType() == typeof(DieContainer)) {
            overworldPlayer.AddDie((DieContainer)reward);
        } else if (reward.GetType() == typeof(TileContainer)) {
            TileContainer tc = (TileContainer)reward;
            Tile t = tc.Copy();
            overworldPlayer.AddTile(t);
        }

        LockUI();
        _StateController.IsStateControllerDriven = true;
    }

    public void LoadRewardPopup() {
        RectTransform rewardPopup = Instantiate<RectTransform>(PopupTemplate, HUD.transform, false);
        rewardPopup.name = PopupTemplate.name;
        RectTransform rewardContainer = (RectTransform)rewardPopup.Find("RewardsMenu").Find("Rewards");
        Debug.Log(rewardContainer);


        float start = -rewardContainer.sizeDelta.x / 2;
        float offset = rewardContainer.sizeDelta.x / (Rewards.Count + 1);


        for (int i = 0; i < Rewards.Count; i++) {
            Debug.Log("i: " + i);
            Debug.Log("Count: " + Rewards.Count);
            GameObject rewardObject = Instantiate<GameObject>(RewardTemplate, rewardContainer, false);
            Transform t = rewardObject.GetComponent<Transform>();
            t.localPosition = new Vector2(start + (offset * (i + 1)), 0);
            Button b = rewardObject.GetComponentInChildren<Button>();
            ItemContainer ic = Rewards[i];
            b.onClick.AddListener(() => GrantReward(ic));
            Image img = b.GetComponent<Image>();
            img.sprite = Rewards[i].Image;
        }
    }

    public void EndTurn() {
        if (UILocked) {
            return;
        }

        foreach (UICombatDiceSlot diceSlot in HUD.GetComponentsInChildren<UICombatDiceSlot>()) {
            diceSlot.Clear();
        }

        _StateController.IsStateControllerDriven = true;
    }

    private UICombatDiceSlot GetEmptyPoolDiceSlot() {
        foreach (UICombatDiceSlot diceSlot in DicePool.GetComponentsInChildren<UICombatDiceSlot>()) {
            if (diceSlot.gameObject.transform.Find("Die") == null) {
                return diceSlot;
            }
        }

        return null;
    }

    private UICombatDiceSlot GetAllDiceSlot(string uuid) {
        foreach (UICombatDiceSlot diceSlot in HUD.GetComponentsInChildren<UICombatDiceSlot>()) {
            if (uuid == diceSlot.DieUUID) {
                return diceSlot;
            }
        }
        return null;
    }
}
