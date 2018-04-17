using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [Tooltip("Whether something is inside the spawn point")]
    public bool SpaceIsOccupied = false;
    void OnTriggerStay(Collider other)
    {
        SpaceIsOccupied = true;
    }
    void OnTriggerExit(Collider other)
    {
        SpaceIsOccupied = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(3, 3, 3));
    }
}
