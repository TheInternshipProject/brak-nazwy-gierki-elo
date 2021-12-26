using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioClip Umieranie;
    [SerializeField] private AudioClip MainMenuMusic;
   
    public void Update(){
        if(MainMenu.isMenuOpened)
        {
            SoundManager.instance.LowerVolume(0.05f);
           SoundManager.instance.PlaySound(MainMenuMusic);
        }
        if(Health.isPlayerDead)
        {
            SoundManager.instance.LowerVolume(0.05f);
            SoundManager.instance.PlaySound(Umieranie);
        }
        else SoundManager.instance.Stop();
    }
}
