using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public static Controller controllerInstance;
    public static Raycast raycastInstance;
    public static Controller controller;
    public GameObject player;
    private NavMeshAgent _nav;
    private Transform _player;

    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Debug.Log("In the Update");
        _nav.SetDestination(_player.position);
    }
}