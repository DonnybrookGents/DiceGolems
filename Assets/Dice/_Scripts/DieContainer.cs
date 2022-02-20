using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Die Instance", menuName = "ScriptableObjects/Die")]
public class DieContainer : ScriptableObject {
    public int[] Faces;

    public Die Copy(){
        return new Die(Faces);
    }
}
