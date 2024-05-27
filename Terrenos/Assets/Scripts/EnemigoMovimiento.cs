using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMovimiento : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }
}
