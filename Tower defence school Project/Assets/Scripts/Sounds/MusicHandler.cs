using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioClip MainMenuMusic;
   
    public void Update(){
        if(PauseMenu.GameIsPaused)
        {
            SoundManager.instance.LowerVolume(0.05f);
           SoundManager.instance.PlaySound(MainMenuMusic);
        }
        else SoundManager.instance.Stop();
    }
}
