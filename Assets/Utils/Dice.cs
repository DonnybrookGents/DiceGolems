using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice {
    private int[] _Faces;

    public Dice(int[] faces) {
        SetFaces(faces);
    }

    public Dice() : this(new int[] { 1, 2, 3, 4, 5, 6 }) { }

    public int Roll() {
        int index = _Faces.Length;
        return _Faces[Random.Range(0, index)];
    }

    public void SetFaces(int[] faces) {
        _Faces = faces;
    }

    public int[] GetFaces() {
        return _Faces;
    }
}
