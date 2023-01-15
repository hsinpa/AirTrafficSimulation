using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AirTraffic
{

    public class AirCraftAgent : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer traceRenderer;

        private Vector3 destination;
        private Vector3 last_position;

        private float speed = 1;

        private Vector3 m_velocity;
        private Vector3 m_trace_position = new Vector3();

        private bool m_destination_reached = false;
        public bool DestinateReached => this.m_destination_reached;

        public void SetUp(Vector3 destination, float speed) {
            this.destination = destination;
            this.last_position = this.transform.position;
            this.speed = speed;
        }

        private void Update()
        {
            float distance = Vector3.Distance(destination, this.transform.position);

            if (distance < 1 || m_destination_reached) {
                m_destination_reached = true;
                return; 
            } 

            Vector3 direction = (destination - this.transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
            m_velocity = transform.position - last_position;

            last_position = transform.position;

            Vector3 tracePosition = (-direction * speed) + transform.position;
            traceRenderer.SetPosition(0, transform.position);
            traceRenderer.SetPosition(1, tracePosition);
        }
    }
}