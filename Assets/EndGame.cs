using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
