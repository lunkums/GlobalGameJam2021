using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Player has been destroyed.");
    }
}
