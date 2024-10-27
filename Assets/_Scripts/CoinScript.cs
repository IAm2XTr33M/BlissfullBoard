using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //variables
    [SerializeField] float collectForce = 0.8f;
    [SerializeField] float collectTime = 0.5f;

    [SerializeField] float rotationSpeed = 180;


    bool collected = false;

    private void Update()
    {
        //rotate the coin
        transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if coin gets triggered by the player
        if (other.CompareTag("Player") && !collected) {
            collected = true;
            GetComponent<AudioSource>().Play();
            StartCoroutine(CollectCoin());
        }
    }

    IEnumerator CollectCoin()
    {
        //Coin collection logic calls
        PlayerController.instance.collectCoin();
        CoinController.instance.nextCoin();

        //move the coin
        for (int i = 0; i < 30; i++)
        {
            transform.position += new Vector3(0, collectForce / 20, 0);
            transform.localScale /= 0.7f;
            yield return new WaitForSeconds(collectTime / 30);
        }
        foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
        {
            rend.enabled = false;
        }
        yield return new WaitForSeconds(1.5f);
        //get rid of the coin
        Destroy(gameObject);
    }
}
