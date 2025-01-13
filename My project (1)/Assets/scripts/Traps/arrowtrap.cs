using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowtrap : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField]private Transform firepoint;
    [SerializeField]private GameObject[] arrows;
    private float cooldownTimer;
    [Header ("SFX")]
    [SerializeField]private AudioClip arrowSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= attackCooldown){
            Attack();
        }   
    }
    private int Findarrow(){
        for(int i = 0; i < arrows.Length; i++){
            if(!arrows[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }
    private void Attack(){
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(arrowSound);

        arrows[Findarrow()].transform.position = firepoint.position;
        arrows[Findarrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
}
