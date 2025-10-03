using UnityEngine;

public class ControlerMusica : MonoBehaviour{

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;
    public AudioSource AudioSourceDead;
    public AudioSource hitBoss;
    public AudioSource bossfootStep;
    public AudioSource hitPlayer;

    private void Awake()
    {
        float volume = PlayerPrefs.GetFloat("volume");

        AudioSource1.volume = volume;
        AudioSource2.volume = volume;
        AudioSourceDead.volume = volume;
        hitBoss.volume = volume;
        bossfootStep.volume = volume;
        hitPlayer.volume = volume;
    }
    void Start(){

       




        AudioSource1.Play();

        Health playerHealth = Object.FindFirstObjectByType<Health>();

        if (playerHealth != null){
            playerHealth.Died.AddListener(PlayerDead);
        }

    }
    public void HitPlayer() {
        hitPlayer.Play();
    }
    public void HitBoss() {
        hitBoss.Play();
    }
    public void HitGround() {
        bossfootStep.Play();
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
