using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerPoint : MonoBehaviour
{
    [Header("Custom Event")] [SerializeField] private UnityEvent[] customEvent;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("isTriggered", true);
        if (collision.gameObject.name == "Player")
        {
            customEvent[0].Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.SetBool("isTriggered", false);
        customEvent[1].Invoke();
    }


}
