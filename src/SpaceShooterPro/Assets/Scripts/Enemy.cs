using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Editor Settings
    [SerializeField]
    private float _enemySpeed = 4.0f;

    [SerializeField] 
    private float _bottomScreenOffset = -5f;
    
    [SerializeField] 
    private float _spawnYOffset = 7.0f;

    [SerializeField]
    private float _randomSpawnMinOffset = -8f;

    [SerializeField]
    private float _randomSpawnMaxOffset = 8f;
    #endregion

    private Player _player;
    private Animator _animator;

    /// <summary>
    /// Execute on the first frame run
    /// </summary>
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL!!");
        }

        if (_animator == null)
        {
            Debug.LogError("Enemy animator is NULL!!");
        }
    }
    
    /// <summary>
    /// Update is called once per frame 
    /// </summary>
    private void Update()
    {
        CalculateMovement();
    }
    
    /// <summary>
    /// Trigger when this object enter in a collision.
    /// </summary>
    /// <param name="other">The object this collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Damage player if this enemy hit
        if (other.tag == "Player")
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(gameObject, 2.50f);
        }

        //Destroy this enemy and laser if they collide
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemySpeed = 0;
            Destroy(gameObject, 2.50f);
        }
    }

    /// <summary>
    /// Calculate enemy movement
    /// </summary>
    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);
        
        //If bottom of screen respawn at top with random X
        if (transform.position.y < _bottomScreenOffset)
        {
            float randomX = Random.Range(_randomSpawnMinOffset, _randomSpawnMaxOffset);
            transform.position = new Vector3(randomX, _spawnYOffset, 0);
        }
    }
}
