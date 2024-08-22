using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
   [SerializeField] float maxHealth = 100f;
   float currentHealth;

    private void Awake(){
        currentHealth = maxHealth;
    }
   public void TakeDamage(float damage){
    currentHealth -= damage;
    Debug.Log("Current object Health: " + currentHealth);
    if(currentHealth <= 0){
        Die();
    }
   }

   private void Die(){
    Debug.Log("AAAAAAH, i've died!");
    Destroy(gameObject);
   }
}
