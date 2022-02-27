using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldButtonController : MonoBehaviour {
    private int winCount;
    public RectTransform CurrentBackground;
    public Button[] Buttons;
    public Sprite[] Backgrounds;

    void Start() {
        winCount = GameObject.FindGameObjectWithTag(GameStateController.TAG).GetComponent<OverworldController>().Wins;
        for (int i = 0; i < Buttons.Length; i++) {
            if (i == winCount) {
                Buttons[i].interactable = true;
                CurrentBackground.GetComponent<Image>().sprite = Backgrounds[i];
            } else {
                Buttons[i].interactable = false;
            }
        }
    }

    //add what to do after beating the boss

}
