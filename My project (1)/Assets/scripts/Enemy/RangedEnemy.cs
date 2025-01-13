using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private float range;
    [SerializeField]private int damage;
    [Header ("Ranged Attack")]
    [SerializeField]private Transform firepoint;
    [SerializeField]private GameObject[] fireballs;

    [Header ("Collider Parameters")]
    [SerializeField]private float colliderDistance;
    [SerializeField]private BoxCollider2D boxCollider;
    [Header ("Player Layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private enemyPatrol enemyPatrol;
    [Header ("Sound")]
    [SerializeField] private AudioClip fireballSound;

    private void Awake(){
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<enemyPatrol>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()){
            if(cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            anim.SetTrigger("rangedAttack");
        }
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack(){
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        fireballs[findFireBall()].transform.position = transform.position;
        fireballs[findFireBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int findFireBall(){
        for(int i = 0; i < fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    private bool PlayerInSight(){
        RaycastHit2D  hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new UnityEngine.Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, 0), 0, UnityEngine.Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new UnityEngine.Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, 0));
    }
}
