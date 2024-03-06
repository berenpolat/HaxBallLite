using Unity.Mathematics;
using UnityEngine;

public class Frezeer : MonoBehaviour
{
    [SerializeField] private GameObject FrezeerBlowUp;
    private GameObject FrezeerBlowUpInstance;
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

            FrezeerBlowUpInstance = Instantiate(FrezeerBlowUp, transform.position, quaternion.identity);
            Destroy(FrezeerBlowUpInstance, 1f);
            Destroy(gameObject);
        }
        if(other.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
