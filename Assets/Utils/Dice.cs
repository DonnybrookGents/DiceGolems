using UnityEngine;

public class Die {
    private int[] _Faces;

    public Die(int[] faces) {
        SetFaces(faces);
    }

    public Die() : this(new int[] { 1, 2, 3, 4, 5, 6 }) { }

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
