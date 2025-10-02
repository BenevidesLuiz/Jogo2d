using UnityEngine;

public class ControlerMusica : MonoBehaviour{

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;
    public AudioSource AudioSourceDead;

    void Start(){
        AudioSource1.Play();

        Health playerHealth = Object.FindFirstObjectByType<Health>();

        if (playerHealth != null){
            playerHealth.Died.AddListener(PlayerDead);
        }

    }
    public void TocarMusicaFase2(){
        
        if (AudioSource1.isPlaying){
            AudioSource1.Stop();
        }
        
        AudioSource2.Play();
    }


    private void PlayerDead(){

        if (AudioSource1.isPlaying){
            AudioSource1.Stop();
        }
        
        if (AudioSource2.isPlaying){
            AudioSource2.Stop();
        }

        if (AudioSourceDead != null){
            AudioSourceDead.Play();
        }
    }

}
