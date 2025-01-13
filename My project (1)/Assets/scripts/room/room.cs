using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    [SerializeField]private GameObject[] enemies; //array of enemies
    private UnityEngine.Vector3[] spawnPoints; //array of spawn points

    private void Awake(){
        spawnPoints = new UnityEngine.Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
                spawnPoints[i] = enemies[i].transform.position;
            }
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
    public void ActivateRoom(bool _status){
        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
                enemies[i].SetActive(_status);
                enemies[i].transform.position = spawnPoints[i];
            }
        }
    }
}
