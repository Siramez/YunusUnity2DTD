using System.Collections;
using UnityEngine;

public class HordeZombielingsMovement : MonoBehaviour
{
    public int currentWaypoint = 0;
    private float speed;
    public Transform[] waypoints;
    public Transform[] Waypoints
    {
        get { return waypoints; }
        set { waypoints = value; }
    }

    void Start()
    {

        if (waypoints == null || waypoints.Length == 0)
        {
            FindWaypoints();
        }
    }
    void FindWaypoints()
    {
        GameObject spawner = GameObject.Find("Spawner");

        if (spawner != null)
        {
            waypoints = spawner.GetComponent<EnemyWaveSpawner>().EnemyPaths;
        }
    }

    public void SetWaypoints(Transform[] newWaypoints, int newCurrentWaypoint)
    {
        waypoints = newWaypoints;
        currentWaypoint = newCurrentWaypoint;
    }



    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        MoveTowardsWaypoint();

        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    void MoveTowardsWaypoint()
    {
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        direction.Normalize();

        transform.Translate(direction * Time.deltaTime);
    }
}
