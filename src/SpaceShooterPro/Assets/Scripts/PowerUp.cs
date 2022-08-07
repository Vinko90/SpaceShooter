using UnityEditor.PackageManager;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    #region Editor Setting
    [SerializeField] 
    private PowerUpType _type;
    
    [SerializeField]
    private float _dropSpeed = 3.0f;
    
    [SerializeField]
    private float _bottomClearOffset = -4.5f;
    
    [SerializeField]
    private AudioClip _audioClip;
    #endregion

    /// <summary>
    /// Supported Power Up Types
    /// </summary>
    private enum PowerUpType
    {
        Triple = 0,
        Speed = 1,
        Shield = 2
    }
    
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
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            if (player != null)
            {
                switch (_type)
                {
                    case PowerUpType.Triple: player.EnableTripleShot();
                        break;
                    case PowerUpType.Speed: player.EnableSpeedBoost();
                        break;
                    case PowerUpType.Shield: player.EnableShield();
                        break;
                    default: Debug.LogError("Unsupported Power type!!");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
