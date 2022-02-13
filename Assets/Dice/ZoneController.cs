using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceZone {
    Pool,
    AttackTile,
    DefenseTile,
    None
}

public class ZoneController : MonoBehaviour {
    private Dictionary<DiceZone, Dictionary<string, Die>> Zones;

    void Awake() {
        Zones = new Dictionary<DiceZone, Dictionary<string, Die>>();

        Zones.Add(DiceZone.Pool, new Dictionary<string, Die>());
        Zones.Add(DiceZone.AttackTile, new Dictionary<string, Die>());
        Zones.Add(DiceZone.DefenseTile, new Dictionary<string, Die>());
    }

    public DiceZone GetZone(string dieUUID) {
        foreach (DiceZone zone in Zones.Keys) {
            if (Zones[zone].ContainsKey(dieUUID)) {
                return zone;
            }
        }
        return DiceZone.None;
    }

    public Die GetDie(string dieUUID) {
        foreach (Dictionary<string, Die> zone in Zones.Values) {
            if (zone.ContainsKey(dieUUID)) {
                return zone[dieUUID];
            }
        }
        return null;
    }

    public void AddDie(DiceZone zone, Die die) {
        Zones[zone].Add(die.UUID, die);
    }

    public void RemoveDie(string uuid) {
        DiceZone zone = GetZone(uuid);
        if (!Zones.ContainsKey(zone)) {
            return;
        }
        Zones[zone].Remove(uuid);
    }

    public void MoveDie(DiceZone zone, string uuid) {
        if (!Zones.ContainsKey(zone) || GetDie(uuid) == null) {
            return;
        }
        Die tempDie = GetDie(uuid);
        RemoveDie(uuid);
        Zones[zone].Add(tempDie.UUID, tempDie);
    }

    public void SwapDice(string uuid1, string uuid2) {
        if (GetDie(uuid1) == null && GetDie(uuid2) == null) {
            return;
        }
        DiceZone tempZone = GetZone(uuid2);
        MoveDie(GetZone(uuid1), uuid2);
        MoveDie(tempZone, uuid1);
    }

    public void Clear() {
        foreach (DiceZone zone in Zones.Keys) {
            Clear(zone);
        }
    }

    public void Clear(DiceZone zone) {
        Zones[zone].Clear();
    }

    public void PrintDiceInZones() {
        foreach (DiceZone zone in Zones.Keys) {
            Debug.Log(zone + ":");
            PrintDiceInZones(zone);
            Debug.Log("\n");
        }
    }

    public void PrintDiceInZones(DiceZone zone) {
        foreach (string uuid in Zones[zone].Keys) {
            Debug.Log(uuid);
        }
    }
}
