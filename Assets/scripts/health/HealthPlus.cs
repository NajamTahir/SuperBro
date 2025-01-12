using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPlus : MonoBehaviour
{
    [SerializeField]private float healthValue;
    [SerializeField]private AudioClip Pickupsound;
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
            SoundManager.instance.PlaySound(Pickupsound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            Destroy(gameObject);
        }
    }
}
