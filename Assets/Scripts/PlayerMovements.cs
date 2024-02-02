using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    #region Joytick

    public VariableJoystick variableJoystick;

    #endregion

    #region Player variables

    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform playerStartPoint;
    private Rigidbody2D rb;
    
    #endregion

    #region Vectors

    private Vector3 input;
    private Vector2 direction;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = playerStartPoint.position;
    }
    private void FixedUpdate()
    {
        Vector2 targetDirection = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
        targetDirection.Normalize();

        // D�n�� a��s�n� hesapla
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Karakterin d�nmesi
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

        // Hareketi uygula
        rb.velocity = targetDirection * speed;
    }
    #endregion
    
    public void ShootButton()
    {
   
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D coll in colls)
        {
            if(coll.TryGetComponent(out BallMovements ball))
            {
                Vector3 dir = coll.transform.position - transform.position;
                ball.Shoot(dir);
                break;
            }
        }
    }
    
}