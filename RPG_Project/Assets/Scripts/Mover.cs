using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    Ray lstRay;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

        Debug.DrawRay(lstRay.origin, lstRay.direction * 100, Color.red);

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
