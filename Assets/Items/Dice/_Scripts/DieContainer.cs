using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Die Instance", menuName = "ScriptableObjects/Die")]
public class DieContainer : ItemContainer {
    public int[] Faces;
    public Sprite[] Images;

    public GameObject DiePrefab;

    public GameObject LoadDiePrefab() {
        Debug.Log("Creating Die Prefab Copy");
        GameObject diePrefabCopy = Instantiate(DiePrefab);
        diePrefabCopy.name = DiePrefab.name;
        Die d = diePrefabCopy.GetComponent<Die>();
        Image i = diePrefabCopy.GetComponent<Image>();
        d.Faces = Faces;
        d.Images = Images;
        d.DieData = this;
        return diePrefabCopy;
    }
}
