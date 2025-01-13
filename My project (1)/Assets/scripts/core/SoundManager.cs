using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance{ get; private set;}
    private AudioSource source;
    private AudioSource musicSource;


    private void Awake(){
        instance = this;
        source = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        if(instance == null){ // keeps the sound manager from being destroyed when loading a new scene
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this){ // destroys the duplicate sound manager
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip _sound){
        source.PlayOneShot(_sound);
    }
    public void ChangeSoundVolume(float _change){
        //Get initial value of the volume and change it
        float currentVolume = PlayerPrefs.GetFloat("SoundVolume");
        currentVolume += _change;
        //check if the volume is high or low
        if(currentVolume > 1){
            currentVolume = 0;
        }
        else if(currentVolume < 0){
            currentVolume = 1;
        }
        //Apply the new volume
        source.volume = currentVolume;
        PlayerPrefs.SetFloat("SoundVolume", currentVolume);
    }
    public void ChangeMusicVolume(float _change){
        //Get initial value of the volume and change it
        float currentVolume = PlayerPrefs.GetFloat("MusicVolume");
        currentVolume += _change;
        //check if the volume is high or low
        if(currentVolume > 1){
            currentVolume = 0;
        }
        else if(currentVolume < 0){
            currentVolume = 1;
        }
        //Apply the new volume
        musicSource.volume = currentVolume;
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
    }
}
