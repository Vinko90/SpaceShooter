using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Editor Settings
    [SerializeField] 
    private int _playerScore = 0;
    
    [SerializeField] 
    private int _playerLife = 3;
    
    [SerializeField]
    private float _playerSpeed = 3.5f;

    [SerializeField]
    private float _speedMultiplier = 2f;

    [SerializeField]
    private float _verticalMaxBoundary = 0;

    [SerializeField]
    private float _verticalMinBoundary = -3.8f;

    [SerializeField]
    private float _horizontalMaxBoundary = 11.3f;

    [SerializeField]
    private float _horizontalMinBoundary = -11.3f;

    [SerializeField]
    private float _laserSpawnOffset = 1.05f;
    
    [SerializeField] 
    private float _fireRate = 0.15f;
    
    [SerializeField] 
    private bool _isTripleShotActive = false;

    [SerializeField] 
    private bool _isSpeedBoostActive = false;
    
    [SerializeField] 
    private bool _isShieldActive = false;
    
    [SerializeField]
    private GameObject _laserPrefab;
    
    [SerializeField]
    private GameObject _laserTripleShotPrefab;

    [SerializeField] 
    private GameObject _shieldPrefab;
    #endregion
    
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    
    /// <summary>
    /// Start is called before the first frame update.
    /// Position player to center screen.
    /// </summary>
    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
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

        if (_isTripleShotActive)
        {
            Instantiate(_laserTripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector3 spawnLocation = transform.position + new Vector3(0, _laserSpawnOffset, 0);
            Instantiate(_laserPrefab, spawnLocation, Quaternion.identity);
        }
    }

    /// <summary>
    /// Damage this player life
    /// </summary>
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldPrefab.SetActive(false);
        }
        else
        {
            _playerLife--;
            _uiManager.UpdateLifeImage(_playerLife);
        }
        
        if (_playerLife < 1)
        {
            _spawnManager.StopSpawnManager();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enable Triple Shot for limited time
    /// </summary>
    public void EnableTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    /// <summary>
    /// Enable Speed Boost power up
    /// </summary>
    public void EnableSpeedBoost()
    {
        _isSpeedBoostActive = true;
        _playerSpeed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    /// <summary>
    /// Enable Player Shield
    /// </summary>
    public void EnableShield()
    {
        _isShieldActive = true;
        _shieldPrefab.SetActive(true);
    }

    /// <summary>
    /// Add score
    /// </summary>
    /// <param name="points">The points that need to be added</param>
    public void AddScore(int points)
    {
        _playerScore += points;
        _uiManager.UpdateScore(_playerScore);
    }

    /// <summary>
    /// Disable triple shot after certain time.
    /// </summary>
    /// <returns>Enumerator yield</returns>
    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    /// <summary>
    /// Disable speed boost after certain time.
    /// </summary>
    /// <returns>Enumerator yield</returns>
    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _playerSpeed /= _speedMultiplier;
    }
}
