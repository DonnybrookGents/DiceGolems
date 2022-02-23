using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DieUtility {
    public static int SumDice(List<Die> dice){
        int sum = 0;
        foreach(Die d in dice){
            sum += d.Value;
        }
        return sum;
    }
}
