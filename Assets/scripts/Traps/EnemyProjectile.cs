using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage //Will damage the player
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTimer;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return; // Prevent further movement after hitting the target
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if (lifetime >= resetTimer)
        {
            Deactivate();
        }
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return; // Prevent multiple triggers for the same collision

        base.OnTriggerEnter2D(collision); // Execute the base class method
        if (collision.CompareTag("Player"))
        {
            hit = true;
            boxCollider.enabled = false;

            if (anim != null)
            {
                anim.SetTrigger("explode"); // Trigger explosion animation
                StartCoroutine(DeactivateAfterAnimation());
            }
            else
            {
                Deactivate();
            }
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        // Wait for the animation to finish before deactivating
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Deactivate();
    }
}
