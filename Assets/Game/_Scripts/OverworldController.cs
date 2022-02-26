using UnityEngine;

public class OverworldController : MonoBehaviour {
    [HideInInspector] public PlayerOverworldController overWorldPlayer;
    //move scriptable object to character selection or new game process
    public PlayerContainer PlayerContainer;

    public void Start() {
        overWorldPlayer = new PlayerOverworldController();
        overWorldPlayer.SetPlayerContainer(PlayerContainer);
        overWorldPlayer.CloneData();
    }
}
