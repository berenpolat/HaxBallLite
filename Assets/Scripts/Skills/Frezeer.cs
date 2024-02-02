using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frezeer : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Di�er obje "Enemy" tag'ine sahip mi kontrol et
        if (other.CompareTag("Enemy"))
        {
            // E�er �arpt���m�z obje "Enemy" tag'ine sahip ise, EnemyScript bile�enini al
            EnemyScript enemyScript = other.GetComponent<EnemyScript>();

            // EnemyScript bulundu mu kontrol et
            if (enemyScript != null)
            {
                // E�er enemyScript de�i�keni bo� de�ilse, enemySpeed'i 0 yap
                enemyScript.enemySpeed = 0;
                
            }
            Destroy(gameObject);
        }
    }
}
