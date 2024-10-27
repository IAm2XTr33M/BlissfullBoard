using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    //singleton
    public static CoinController instance;

    //singleton logic
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //variables
    [SerializeField] List<GameObject> coins = new List<GameObject>();

    int index = 0;

    private void Start()
    {
        //turn each coin off
        foreach (GameObject coin in coins)
        {
            coin.SetActive(false);
        }
    }

    public void nextCoin()
    {
        //pick a random next coin to go to and turn the previous one off
        if (coins.Count > index)
        {
            coins.RemoveAt(index);
        }
        if (coins.Count > 0)
        {
            index = Random.Range(0, coins.Count);
            coins[index].SetActive(true);
        }
    }
}
