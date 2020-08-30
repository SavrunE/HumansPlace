using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Список настроек для врагов")]
    [SerializeField] private List<EnemyData> enemySettings;

    [Tooltip("Количество объектов в пуле")]
    [SerializeField] private int poolCount;

    [Tooltip("Ссылка на базовый префаб врагов")]
    [SerializeField] private GameObject enemyPrefab;

    [Tooltip("Время между спауном врагов")]
    [SerializeField] private float spawnTimeDelay;

    public static Dictionary<GameObject, Enemy> Enemies;

    private Queue<GameObject> currentEnemies;

    private void Start()
    {
        Enemies = new Dictionary<GameObject, Enemy>();
        currentEnemies = new Queue<GameObject>();

        for (int i = 0; i < poolCount; ++i)
        {
            var prefab = Instantiate(enemyPrefab);
            var script = prefab.GetComponent<Enemy>();
            prefab.SetActive(false);
            Enemies.Add(prefab, script);
            currentEnemies.Enqueue(prefab);
        }
        Enemy.OnEnemyOverFly += ReturnEnemy;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if (spawnTimeDelay == 0)
        {
            spawnTimeDelay = 1;
        }
        while (true)
        {
            yield return new WaitForSeconds(spawnTimeDelay);
            if (currentEnemies.Count > 0)
            {
                //получение компонентов и активация вражины
                var enemy = currentEnemies.Dequeue();
                var script = Enemies[enemy];
                enemy.SetActive(true);
                //генерация случайного EnemyData и инициализация
                int random = Random.Range(0, enemySettings.Count);
                script.Init(enemySettings[random]);
                //задаем позицию вражины
                float xPos = Random.Range(-CameraBorder.Border, CameraBorder.Border);
                enemy.transform.position = new Vector2(xPos, transform.position.y);
            }
        }

    }

    private void ReturnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        enemy.SetActive(false);
        currentEnemies.Enqueue(enemy);
    }
}
