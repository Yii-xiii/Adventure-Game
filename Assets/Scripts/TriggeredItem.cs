using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredItem : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private bool isTriggered;

    private void Update()
    {
        if (!isTriggered)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[0].transform.position, speed * Time.deltaTime);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[1].transform.position, speed * Time.deltaTime);
        }
    }

    public void Triggered()
    {
        isTriggered = true;
    }

    public void ExitTriggered()
    {
        isTriggered = false;
    }
}
