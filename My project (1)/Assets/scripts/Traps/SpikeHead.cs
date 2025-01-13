using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header ("SpikeHead")]
    [SerializeField]private float speed;
    [SerializeField]private float range;
    [SerializeField]private float checkDelay;
    [SerializeField]private LayerMask PlayerLayer;
    private float checkTimer;
    private UnityEngine.Vector3 destination;
    private bool attacking;
    private UnityEngine.Vector3[] directions = new UnityEngine.Vector3[4];
    [Header("SFX")]
    [SerializeField] private AudioClip attackSFX;

    private void onEnable(){
        Stop();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (attacking){
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else{
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay){
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer(){ // the enemy(spike head) will check for the player in the range and if it finds the player it will move towards it
        calculateDirections();
        // check if spike head is in range of the player and check in all the 4 directions
        for (int i = 0; i < directions.Length; i++){
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, PlayerLayer);
            if (hit.collider != null && !attacking){
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void calculateDirections(){
        directions[0] = transform.right * range;//Right direction
        directions[1] = -transform.right * range;//Left direction
        directions[2] = transform.up * range;//Up direction
        directions[3] = -transform.up * range;//Down direction
    }
    private void Stop(){
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision){
        SoundManager.instance.PlaySound(attackSFX);
        base.OnTriggerEnter2D(collision);
        Stop(); //Stop
    }
}
