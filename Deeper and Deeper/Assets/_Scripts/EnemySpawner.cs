using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] private float minAttackDistance;

    private float spawnDelay;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        spawnDelay = Random.Range(4f, 5f);
        yield return new WaitForSeconds(spawnDelay);

        float distance = Vector2.Distance(Player.S.playerTransform.position, transform.position);
        if (distance < maxAttackDistance && distance > minAttackDistance)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            float scale = Random.Range(1f, 1.5f);

            enemy.transform.localScale = new Vector3(scale, scale, 1);
        }
        StartCoroutine(Spawn());
    }
}
