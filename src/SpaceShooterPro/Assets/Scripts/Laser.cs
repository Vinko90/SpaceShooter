using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Editor Settings
    [SerializeField]
    private float _laserSpeed = 8.0f;
    
    [SerializeField]
    private float _autoDestroyOffset = 8.0f;
    #endregion
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        CalculateMovement();
        CheckDestroyOffset();
    }

    /// <summary>
    /// Calculate laser movement and speed.
    /// </summary>
    private void CalculateMovement()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Check if this laser object need to be destroyed.
    /// </summary>
    private void CheckDestroyOffset()
    {
        if (transform.position.y > _autoDestroyOffset)
        {
            Destroy(gameObject);
        }
    }
}
