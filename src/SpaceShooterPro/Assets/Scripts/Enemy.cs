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
    private void OnTriggerEnter(Collider other)
    {
        //Damage player if this enemy hit
        if (other.tag == "Player")
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(gameObject);
        }

        //Destroy this enemy and laser if they collide
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
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
