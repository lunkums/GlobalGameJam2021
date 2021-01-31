using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public GameObject cheese;
    public GameObject completeLevelUI;
    public GameObject failLevelUI;

    private bool gameHasEnded = false;

    public void EndGame(bool hasWon)
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;

            if (hasWon)
            {
                completeLevelUI.SetActive(true);
                Debug.Log("You win!");
            }
            else
            {
                failLevelUI.SetActive(true);
                Debug.Log("You lose!");
            }
        }
    }
}
