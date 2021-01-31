using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == PlayerManager.instance.player)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerManager.instance.EndGame(true);
    }
}
