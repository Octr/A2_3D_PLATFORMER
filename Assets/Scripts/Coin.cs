using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

    public CoinManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime * rotateSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            manager.coinsCollected += 1;
            manager.coinDisplay.text = "Coins: " + manager.coinsCollected;

            if(manager.coinsCollected >= manager.requiredCoins)
            {
                manager.winScreen.SetActive(true);
            }

            gameObject.SetActive(false);

        }
    }
}
