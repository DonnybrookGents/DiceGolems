using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Die Instance", menuName = "ScriptableObjects/Die")]
public class DieContainer : ItemContainer {
    public string Name;
    public int[] Faces;
    public Sprite[] Images;

    public GameObject DiePrefab;

    public GameObject LoadDiePrefab() {
        GameObject diePrefabCopy = Instantiate(DiePrefab);
        diePrefabCopy.name = DiePrefab.name;
        Die d = diePrefabCopy.GetComponent<Die>();
        Image i = diePrefabCopy.GetComponent<Image>();
        d.Name = Name;
        d.Faces = Faces;
        d.Images = Images;
        d.DieData = this;
        return diePrefabCopy;
    }
}
