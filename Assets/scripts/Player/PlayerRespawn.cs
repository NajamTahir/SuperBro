using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //sound when player reaches checkpoint
    private Transform currentcheckPoint;
    private Health PlayerHealth;
    private UiManager uiManager;

    private void Awake(){
        PlayerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UiManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckRespawn(){
        if(currentcheckPoint == null){
            uiManager.GameOver();
            return;
        }

        transform.position = currentcheckPoint.position; //respawn player at checkpoint
        PlayerHealth.Respawn(); //reset player health
        Camera.main.GetComponent<CameraController>().MoveCamera(currentcheckPoint.parent); //set camera to follow player
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.transform.tag == "Checkpoint"){
            currentcheckPoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound); //play checkpoint sound
            collision.GetComponent<Collider2D>().enabled = false; //disable checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear"); //play checkpoint animation
        }
    }
}
