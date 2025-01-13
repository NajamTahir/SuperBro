using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField]private Transform leftEdge;
    [SerializeField]private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField]private Transform enemy;
    [Header ("movement parameters")]
    [SerializeField]private float speed;
    private Vector3 initScale;
    [Header ("Idle Time")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private bool movingleft;
    [Header ("Enemy Animator")]
    [SerializeField]private Animator anim;


    private void Awake(){
        initScale = enemy.localScale;
    }
    private void OnDisable(){
        anim.SetBool("moving", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(movingleft){
            if(enemy.position.x >= leftEdge.position.x){
               MoveInDirection(-1); 
            }
            else{
                //Change direction
                directionChange();
            }
        }
        else{
            if(enemy.position.x <= rightEdge.position.x){
            MoveInDirection(1);
            }
            else{
                //Change direction
                directionChange();
            }
        }
    }
    private void directionChange(){
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if(idleTimer >= idleDuration){
            movingleft = !movingleft;
        }
    }
    private void MoveInDirection(int _direction){
        idleTimer = 0; //reset idle timer
        anim.SetBool("moving", true);
        //enemy moves in direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + _direction * speed * Time.deltaTime * speed, enemy.position.y, enemy.position.z); 
    }
}
