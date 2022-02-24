using UnityEngine;

public class Die {
    public int[] Faces;
    public Sprite[] Images; // Change this to its own SO.
    public int Value;
    public Sprite ImageValue;
    public string UUID;

    public Die(int[] faces, Sprite[] images) {
        Faces = faces;
        Images = images;
        UUID = System.Guid.NewGuid().ToString();
    }

    public Die() : this(new int[] { 1, 2, 3, 4, 5, 6 }, new Sprite[6]) { }

    public Die(Die die) {
        Faces = die.Faces;
        Images = die.Images;
        UUID = System.Guid.NewGuid().ToString();
    }

    public int Roll() {
        int index = Random.Range(0, Faces.Length);

        Value = Faces[index];
        ImageValue = Images[index];

        return Value;
    }
}
