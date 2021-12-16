using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioClip Umieranie;
    [SerializeField] private AudioClip MainMenuMusic;
    [SerializeField] private AudioClip fireball;
   
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
        if(Projectile.fireballSound)
        {
            // SoundManager.instance.LowerVolume(0.01f);
            // SoundManager.instance.PlaySound(fireball);
        }
        else SoundManager.instance.Stop();
    }
}
