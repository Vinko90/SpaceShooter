using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed = 3.5f;

    public float VerticalMaxBoundary = 0;

    public float VerticalMinBoundary = -3.8f;

    public float HorizontalMaxBoundary = 11.3f;

    public float HorizontalMinBoundary = -11.3f;
    
    /// <summary>
    /// Start is called before the first frame update.
    /// Position player to center screen.
    /// </summary>
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        CalculateMovement();
    }

    /// <summary>
    /// Get vertical and horizontal movement value from key manager
    /// and position player within boundaries.
    /// </summary>
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 newPlayerPosition = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(newPlayerPosition * PlayerSpeed * Time.deltaTime);
        
        //Clamp Y Boundaries
        transform.position = new Vector3
           {
               x = transform.position.x,
               y = Mathf.Clamp(transform.position.y, VerticalMinBoundary, VerticalMaxBoundary),
               z = 0
           };
        
        //Check X Boundaries
        if (transform.position.x > HorizontalMaxBoundary)
        {
            transform.position = new Vector3(HorizontalMinBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < HorizontalMinBoundary)
        {
            transform.position = new Vector3(HorizontalMaxBoundary, transform.position.y, 0);
        }
    }
}
