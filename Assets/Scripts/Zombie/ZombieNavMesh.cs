using UnityEngine;
using UnityEngine.AI;

public class ZombieNavMesh : MonoBehaviour
{
    [HideInInspector] public float speed;
    private NavMeshAgent _agent;
    private Transform _destination;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _destination = GameManager.Instance.targetPos;
        _agent.speed = speed;
        _agent.isStopped = false;
    }

    public void StopMoving()
    { 
        _agent.isStopped = true;
    }
    
    private void Update()
    {
        if (!_agent.isStopped)
            _agent.destination = _destination.position;
    }
}
