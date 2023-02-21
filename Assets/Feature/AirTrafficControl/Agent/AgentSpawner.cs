using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirTraffic
{
    public class AgentSpawner : MonoBehaviour
    {
        [SerializeField]
        private AirCraftAgent airCraftAgentPrefab;

        [SerializeField]
        private int max_plane_on_screen;

        [SerializeField]
        private Vector3 map_size;

        private float time_record;
        private float time_delay = 1;

        private Camera m_camera;
        private List<AirCraftAgent> airCraftAgents = new List<AirCraftAgent>();
        private Bounds m_map_bound;

        private void Start()
        {
            this.m_map_bound = new Bounds();
            this.m_map_bound.center = this.transform.position;
            this.m_map_bound.size = map_size;

            this.m_camera = Camera.main;
        }

        private void LateUpdate()
        {
            int agent_lens = airCraftAgents.Count;

            if (time_record < Time.time && agent_lens < max_plane_on_screen) {

                var spawn_agent = RandomSpawn();
                airCraftAgents.Add(spawn_agent);

                time_record = Time.time + time_delay;
            }

            RemoveReachedAirCraft(airCraftAgents);
        }

        private AirCraftAgent RandomSpawn() {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float camHalfHeight = this.m_camera.orthographicSize;
            float camHalfWidth = screenAspect * camHalfHeight;
            float camWidth = 2.0f * camHalfWidth;

            float r_x = Random.Range(-1f, 1f);
            float r_y = Random.Range(-1f, 1f);
            Vector2 offset = new Vector2(r_x, r_y);
                    offset.Normalize();

            offset.x *= camHalfWidth * 2;
            offset.y *= camHalfHeight * 2;

            var spawnAgent = GameObject.Instantiate<AirCraftAgent>(airCraftAgentPrefab, offset, Quaternion.identity, this.transform);

            spawnAgent.SetUp(-offset, speed: Random.Range(0.5f, 2.5f));

            return spawnAgent;
        }

        private int RemoveReachedAirCraft(List<AirCraftAgent> airCraftAgents) {
            int l = airCraftAgents.Count;
            int reached = 0;

            for (int i = l -1; i >= 0; i--) {
                if (!this.m_map_bound.Contains(airCraftAgents[i].transform.position)) {

                    GameObject.Destroy(airCraftAgents[i].gameObject);

                    airCraftAgents.RemoveAt(i);
                }
            }

            return reached;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(this.transform.position, map_size);
        }
    }
}