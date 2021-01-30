using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnDestroy()
    {
        print("Player has been destroyed.");
    }
}
