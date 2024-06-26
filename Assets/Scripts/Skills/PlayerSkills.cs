using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    #region effectler
    [SerializeField] private GameObject frezeerPowerUpEffect;
    [SerializeField] private GameObject PowerShotPowerUpEffect;
    [SerializeField] private GameObject PowerShotCarhingEffect;
    [SerializeField] private GameObject PowerShotReleaseEffect;

    [SerializeField] private Transform ball;
    private GameObject PowerShotCarhingInstance;
    private GameObject PowerShotReleaseInstance;
    private GameObject FrezeerPowerUpInstance;
    private GameObject PowerShotInstance;
    public Vector2 offset;
    #endregion

    #region Frezeer

    [SerializeField] GameObject player;
    [SerializeField] private GameObject frezeeBullet;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private float bulletSpeed;
    public Button frezeerButton;
    public bool Frezeer = false;
    public static bool frezeerUsed;

    #endregion

    #region PowerShoot

    [SerializeField] private Transform grabPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private float circleRadius = 1f;
    BallMovements ballMovementsScript;
    public PlayerMovements playerMovements;
    public Button PowerShootButton;
    public bool PowerShoot; 
    public GameObject powerShootGosterge;
    private GameObject grabbedObject;
    public  GameObject powerShootRange;
    private bool PowerShooCanUse;
    private int layerIndex;
    
       

    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovements = FindObjectOfType<PlayerMovements>();
        layerIndex = LayerMask.NameToLayer("ball");
        frezeerUsed = false;
        PowerShoot = true;
        PowerShooCanUse = false;
        powerShootRange.SetActive(false);
    }
    public void ResetSkills()
    {
        frezeerUsed = false;
        PowerShoot = true;
        PowerShooCanUse = false;
    }
    #region Freezer
    public void FrezeerButton()
    {
        if (Frezeer == true)
        {
            var Fbullet = Instantiate(frezeeBullet, FirePoint.position, FirePoint.rotation);
            Fbullet.GetComponent<Rigidbody2D>().velocity = FirePoint.up * bulletSpeed;
            Frezeer = false;
            frezeerUsed = true;
            if (frezeerUsed == true)
            {
                frezeerButton.interactable = false;
            }
            Destroy(FrezeerPowerUpInstance);
        }
    }
    public void FrezeerActive()
    {
        if (frezeerUsed == false)
        {
            Frezeer = true;
            FrezeerPowerUpInstance = Instantiate(frezeerPowerUpEffect, (Vector2)player.transform.position + offset, Quaternion.identity);
            FrezeerPowerUpInstance.transform.SetParent(player.transform);
            frezeerButton.interactable = false;
        }
    }
    #endregion
    #region PowerShot
    void OnDrawGizmos()
    {
        // Gizmos ile CircleCast'in alanını çizelim.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rayPoint.position + (Vector3)transform.right * rayDistance, circleRadius);
    }
    public void PowerShot()
    {     
        if(PowerShoot == true)
        {
            HoldButtonDetector.PowerShotUselable = true;
            PowerShooCanUse = true;
            PowerShootButton.interactable = false;
            PlayerMovements.CanShoot = false;
            PowerShotInstance = Instantiate(PowerShotPowerUpEffect, (Vector2)player.transform.position + offset, Quaternion.identity);
            PowerShotInstance.transform.SetParent(player.transform.GetChild(0));
            powerShootRange.SetActive(true);

        }

    }
    public void PowerShootCharging()
    {
        if(PowerShooCanUse == true)
        {
            // Yuvarlak alanda tarama yapılır
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(rayPoint.position, circleRadius);
            powerShootRange.SetActive(false); 
            powerShootGosterge.SetActive(true);

            // Tarama sonucunda çarpışan objeler kontrol edilir
            foreach (Collider2D collider in hitColliders)
            {
                // Eğer bir çarpışma olduysa ve çarpışan obje "ball" tag'ine sahip ise
                if (collider.CompareTag("Ball"))
                {
                    grabbedObject = collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    grabbedObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                    grabbedObject.transform.position = grabPoint.position;
                    grabbedObject.transform.SetParent(transform);
                    Vector3 spawnPosition = new Vector3(ball.position.x + 0.5f, ball.position.y, ball.position.z);
                    Quaternion spawnRotation = Quaternion.identity; // 2D oyunlarda genellikle Quaternion.identity kullanılır
                    GameObject PowerShotCarhingInstance = Instantiate(PowerShotCarhingEffect, spawnPosition, spawnRotation);
                    ;
                    break; // Bir obje bulunduğunda döngüden çıkılır
                    
                }
            }
        }
    }
    public void PowerShootRelease()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
    
        // Additional code
        PowerShootButton.interactable = false;
        PowerShooCanUse = false;
        PlayerMovements.CanShoot = true;
        playerMovements.powerShot();
        Destroy(PowerShotInstance);
        powerShootGosterge.SetActive(false);
    }

    #endregion

}
