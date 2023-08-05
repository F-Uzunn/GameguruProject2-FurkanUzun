using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            EventManager.Broadcast(GameEvent.OnPassFinishLine);
            Debug.Log("finish");
        }
    }
}
