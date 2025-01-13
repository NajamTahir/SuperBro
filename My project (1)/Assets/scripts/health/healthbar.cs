using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UnityEngine.UI.Image currentHealthbar;
    [SerializeField] private UnityEngine.UI.Image totalHealthbar;
    // Start is called before the first frame update
    void Start()
    {

        totalHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}