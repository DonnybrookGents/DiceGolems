using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public OverworldController owController;

    public AudioSource audioPlayer;    
    public AudioClip openingTrack;
    public AudioClip track1;
    public AudioClip track2;

    void Awake(){
        audioPlayer.clip = track1;
        audioPlayer.Play();
        audioPlayer.volume = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioPlayer.isPlaying){
            audioPlayer.clip = track1;
            audioPlayer.Play();
        }
    }
}
