using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header ("Firetrap Timer")]
    [SerializeField]private float activationDelay;
    [SerializeField]private float activeTime;
    [Header ("SFX")]
    [SerializeField] private AudioClip firetrapSound;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;//when trap is triggered
    private bool active;//when trap is active and hurts the player
    private Health playerHealth;

    private void Awake(){
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerHealth != null && active){
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")){
            playerHealth = collision.GetComponent<Health>();
            if(!triggered){
                StartCoroutine(Activatefiretrap());
            }
            if(active){
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Player"){
            playerHealth = null;
            
        }
    }
    private IEnumerator Activatefiretrap(){
        triggered = true;
        spriteRend.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated",false);
    }
}
