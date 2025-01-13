using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(collision.transform.position.x < transform.position.x){
                cam.MoveCamera(nextRoom);
                nextRoom.GetComponent<room>().ActivateRoom(true);
                previousRoom.GetComponent<room>().ActivateRoom(false);
            }
            else{
                cam.MoveCamera(previousRoom);
                previousRoom.GetComponent<room>().ActivateRoom(true);
                nextRoom.GetComponent<room>().ActivateRoom(false);
            }
        }
    }
}
