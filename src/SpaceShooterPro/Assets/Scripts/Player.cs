using UnityEngine;

public class Player : MonoBehaviour
{
    #region Editor Settings
    [SerializeField]
    private float _playerSpeed = 3.5f;

    [SerializeField]
    private float _verticalMaxBoundary = 0;

    [SerializeField]
    private float _verticalMinBoundary = -3.8f;

    [SerializeField]
    private float _horizontalMaxBoundary = 11.3f;

    [SerializeField]
    private float _horizontalMinBoundary = -11.3f;

    [SerializeField]
    private float _laserSpawnOffset = 0.8f;
    
    [SerializeField] 
    private float _fireRate = 0.15f;

    [SerializeField]
    private GameObject _laserPrefab;
    #endregion
    
    private float _canFire = -1f;
    
    /// <summary>
    /// Start is called before the first frame update.
    /// Position player to center screen.
    /// </summary>
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        CalculateMovement();
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    /// <summary>
    /// Get vertical and horizontal movement value from key manager
    /// and position player within boundaries.
    /// </summary>
    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 newPlayerPosition = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(newPlayerPosition * _playerSpeed * Time.deltaTime);
        
        //Clamp Y Boundaries
        transform.position = new Vector3
           {
               x = transform.position.x,
               y = Mathf.Clamp(transform.position.y, _verticalMinBoundary, _verticalMaxBoundary),
               z = 0
           };
        
        //Check X Boundaries
        if (transform.position.x > _horizontalMaxBoundary)
        {
            transform.position = new Vector3(_horizontalMinBoundary, transform.position.y, 0);
        }
        else if (transform.position.x < _horizontalMinBoundary)
        {
            transform.position = new Vector3(_horizontalMaxBoundary, transform.position.y, 0);
        }
    }

    /// <summary>
    /// Instantiate laser capsule when pressing space-bar
    /// </summary>
    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 spawnLocation = transform.position + new Vector3(0, _laserSpawnOffset, 0);
        Instantiate(_laserPrefab, spawnLocation, Quaternion.identity);
    }
}
