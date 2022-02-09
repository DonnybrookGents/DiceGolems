using UnityEngine;

public class Die {
    public int[] Faces;
    public int Value;
    public string UUID;

    public Die(int[] faces) {
        Faces = faces;
        UUID = System.Guid.NewGuid().ToString();
    }

    public Die() : this(new int[] { 1, 2, 3, 4, 5, 6 }) { }

    public int Roll() {
        int index = Faces.Length;
        int value = Faces[Random.Range(0, index)];

        Value = value;
        return value;
    }
}
