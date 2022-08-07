using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Editor Settings
    [SerializeField]
    private GameObject _enemyPrefab;
    
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    private GameObject _tripleShotPrefab;
    
    [SerializeField] 
    private float _spawnRate = 5.0f;

    [SerializeField] 
    private float _randomSpawnMinOffset = -8f;
    
    [SerializeField] 
    private float _randomSpawnMaxOffset = 8f;
    
    [SerializeField] 
    private float _verticalSpawnOffset = 7f;
    #endregion

    private bool _stopSpawning = false;
    
    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnTripleShotRoutine());
    }
    
    /// <summary>
    /// Spawn coroutine to auto spawn enemies
    /// </summary>
    /// <returns>Routine yield</returns>
    private IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            var positionToSpawn = new Vector3
            {
                x = Random.Range(_randomSpawnMinOffset, _randomSpawnMaxOffset),
                y = _verticalSpawnOffset,
                z = 0
            };

            var newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    /// <summary>
    /// Spawn coroutine to auto spawn triple shot power up
    /// </summary>
    /// <returns>Routine yield</returns>
    private IEnumerator SpawnTripleShotRoutine()
    {
        while (_stopSpawning == false)
        {
            var positionToSpawn = new Vector3
            {
                x = Random.Range(_randomSpawnMinOffset, _randomSpawnMaxOffset),
                y = _verticalSpawnOffset,
                z = 0
            };
            Instantiate(_tripleShotPrefab, positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    
    /// <summary>
    /// Stop Spawn Manager.
    /// </summary>
    public void StopSpawnManager()
    {
        _stopSpawning = true;
    }
}
