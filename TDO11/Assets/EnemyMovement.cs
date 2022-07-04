using UnityEngine;


//on a un script a besoin de cette component pour travailler telque speed
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNexWaypoint();
        }
        enemy.speed = enemy.startSpeed;
    }
    private void GetNexWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }
    private void EndPath()
    {
        PlayerStats.lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
