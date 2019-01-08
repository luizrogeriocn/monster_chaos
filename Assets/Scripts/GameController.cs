using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject foodPrefab;

    private GameObject player;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject food = null;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
      

        SpawnEnemy();
        SpawnFood();
    }

    // Update is called once per frame
    void Update() { }

    public void SpawnFood()
    {
        if (food == null)
            food = Instantiate(foodPrefab, RandomPosition(), Quaternion.identity);
        else
            food.transform.position = RandomPosition();
    }

    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, RandomPositionAwayFrom(player.transform.position, 1f), Quaternion.identity);
        enemies.Add(enemy);
    }

    private Vector2 RandomPosition()
    {
        float spawnY = Random.Range(
            Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y
        );
        float spawnX = Random.Range(
            Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x
        );

        return new Vector2(spawnX, spawnY);
    }

    private Vector2 RandomPositionAwayFrom(Vector3 position, float threshold)
    {
        Vector2 randomPosition2D = RandomPosition();
        Vector3 randomPosition3D = new Vector3(randomPosition2D.x, randomPosition2D.y);
        if (Vector3.Distance(position, randomPosition3D) >= threshold)
            return randomPosition2D;
        else
            return RandomPositionAwayFrom(position, threshold);
    }
}
