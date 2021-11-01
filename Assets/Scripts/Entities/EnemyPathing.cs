using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class EnemyPathing : MonoBehaviour
    {
        private WaveConfig waveConfig;
        private List<Transform> waypoints;
        private Transform currentWaypoint;
        private int currentWaypointIndex = 0;

        public EnemyPathing Init(WaveConfig waveConfig)
        {
            this.waveConfig = waveConfig;
            return this;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            waypoints = waveConfig.GetWaypoints();
            currentWaypoint = waypoints[currentWaypointIndex];
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLocation();
        }

        private void UpdateLocation()
        {
            float step = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, step);

            if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.001f)
            {
                if (currentWaypointIndex >= waypoints.Count - 1)
                {
                    Destroy(gameObject);
                
                    return;
                }
            
                currentWaypointIndex++;
                currentWaypoint = waypoints[currentWaypointIndex];
            }
        }
    }
}
