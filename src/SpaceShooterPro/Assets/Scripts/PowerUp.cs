using UnityEngine;

public class PowerUp : MonoBehaviour
{
    #region Editor Settings
    [SerializeField]
    private float _dropSpeed = 3.0f;
    
    [SerializeField]
    private float _bottomClearOffset = -4.5f;
    #endregion
    
    /// <summary>
    /// Update is called once per frame 
    /// </summary>
    private void Update()
    {
        CalculateMovement();
    }

    /// <summary>
    /// Calculate the movement down for the powerup
    /// </summary>
    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * _dropSpeed * Time.deltaTime);

        if (transform.position.y < _bottomClearOffset)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Trigger only when colliding with player
    /// </summary>
    /// <param name="other">The object colliding with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.EnableTripleShot();
            }
            Destroy(gameObject);
        }
    }
}
