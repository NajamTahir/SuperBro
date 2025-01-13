using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [Header("iFrames")]
    [SerializeField]private float iFramesDuration;
    [SerializeField]private float numofFlashes;
    private SpriteRenderer spriteRend;
    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    private bool invulnerable;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            // Player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            // Player dead
            if (!dead)
            {
                //will disable all the components
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respawn(){
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
    private IEnumerator Invunerability(){
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //Duration of not been hurt
        for (int i = 0; i < numofFlashes; i++){
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numofFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numofFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
