using UnityEngine;

public class ControlerMusica : MonoBehaviour{

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;

    private bool musica1Tocada = false;
    void Start(){
        AudioSource1.Play();
    }
    public void TocarMusicaFase2(){
        // Para a música 1
        if (AudioSource1.isPlaying){
            AudioSource1.Stop();
        }

        // Toca a música 2
        AudioSource2.Play();
    }
}
