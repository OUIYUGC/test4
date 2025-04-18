using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations = new List<Transform>(); // Инициализируем список
    private int locationIndex = 0; 
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get { return _lives; }

        private set
        {
            _lives = value;

            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;

        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    private void Update()
    {
        if(agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }
    void MoveToNextPatrolLocation()
    {
        if(locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;
        
        locationIndex = (locationIndex + 1) % locations.Count;
        Debug.LogFormat("{0}", locationIndex);
    }
    
    void InitializePatrolRoute()
    {
        
        if (patrolRoute != null) // Проверяем, что patrolRoute не равен null
        {
            
            foreach (Transform child in patrolRoute)
            {
                locations.Add(child);
            }
        }
        else
        {
            Debug.LogWarning("Patrol route не задан!"); // Логируем предупреждение если patrolRoute отсутствует
        }

        
    }
    void OnTriggerEnter(Collider other)
    {
        {
            
            if (other.name == "Player")
            {
                agent.destination = player.position;
                
                Debug.Log("Player detected - attack!");
            }
        }
        
        
    }
    void OnTriggerExit(Collider other)
    {

        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }
}



