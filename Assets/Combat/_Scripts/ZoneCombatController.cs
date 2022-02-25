using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum DiceZone {
//     Pool,
//     AttackTile,
//     DefenseTile,
//     None
// }

public class ZoneCombatController : MonoBehaviour {
    private Dictionary<string, Dictionary<string, Die>> Zones;

    void Awake() {
        Zones = new Dictionary<string, Dictionary<string, Die>>();
    }

    public void AddZone(string zoneUUID) {
        Zones.Add(zoneUUID, new Dictionary<string, Die>());
    }

    public string GetZone(string dieUUID) {
        foreach (string zone in Zones.Keys) {
            if (Zones[zone].ContainsKey(dieUUID)) {
                return zone;
            }
        }

        return null;
    }

    public Die GetDie(string dieUUID) {
        foreach (Dictionary<string, Die> zone in Zones.Values) {
            if (zone.ContainsKey(dieUUID)) {
                return zone[dieUUID];
            }
        }

        return null;
    }

    public void AddDie(string zone, Die die) {
        Zones[zone].Add(die.UUID, die);
    }

    public void RemoveDie(string uuid) {
        string zone = GetZone(uuid);
        if (!Zones.ContainsKey(zone)) {
            return;
        }
        Zones[zone].Remove(uuid);
    }

    public void MoveDie(string zone, string uuid) {

        if (!Zones.ContainsKey(zone) || GetDie(uuid) == null) {
            return;
        }

        Die tempDie = GetDie(uuid);
        RemoveDie(uuid);
        Zones[zone].Add(tempDie.UUID, tempDie);
    }

    public void SwapDice(string uuid1, string uuid2) {

        if (GetDie(uuid1) == null || GetDie(uuid2) == null) {
            return;
        }

        string tempZone = GetZone(uuid2);
        MoveDie(GetZone(uuid1), uuid2);
        MoveDie(tempZone, uuid1);
    }

    public void Clear() {
        foreach (string zone in Zones.Keys) {
            Clear(zone);
        }
    }

    public void Clear(string zone) {
        Zones[zone].Clear();
    }

    public void PrintDiceInZones() {
        foreach (string zone in Zones.Keys) {
            Debug.Log(zone + ":");
            PrintDiceInZones(zone);
            Debug.Log("\n");
        }
    }

    public void PrintDiceInZones(string zone) {
        foreach (string uuid in Zones[zone].Keys) {
            Debug.Log(uuid);
        }
    }
}
