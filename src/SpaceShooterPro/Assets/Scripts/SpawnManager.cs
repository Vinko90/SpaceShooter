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
    private GameObject[] _powerUpPrefabs;
    
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

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpsRoutine());
    }
    
    /// <summary>
    /// Spawn coroutine to auto spawn enemies
    /// </summary>
    /// <returns>Routine yield</returns>
    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
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
    /// Spawn coroutine to auto spawn random power ups
    /// </summary>
    /// <returns>Routine yield</returns>
    private IEnumerator SpawnPowerUpsRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            var positionToSpawn = new Vector3
            {
                x = Random.Range(_randomSpawnMinOffset, _randomSpawnMaxOffset),
                y = _verticalSpawnOffset,
                z = 0
            };

            var randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUpPrefabs[randomPowerUp], positionToSpawn, Quaternion.identity);
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
