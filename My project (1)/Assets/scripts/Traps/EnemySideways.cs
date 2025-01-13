using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField]private float damage;
    private bool moving;
    private float left;
    private float right;
    private void Awake(){
        left = transform.position.x - distance;
        right = transform.position.x + distance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            if(transform.position.x > left){
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                moving = false;
            }
        }
        else{
            if(transform.position.x < right){
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                moving = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
