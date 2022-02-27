using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour {

    public DieContainer DieData;
    public string Name;
    public int[] Faces;
    public Sprite[] Images; // Change this to its own SO.
    public int Value;
    public string UUID;

    public int Roll() {
        int index = Random.Range(0, Faces.Length);

        Value = Faces[index];
        //set image
        GetComponent<Image>().sprite = Images[index];

        return Value;
    }

    public void Start() {
        UUID = System.Guid.NewGuid().ToString();
    }
}
