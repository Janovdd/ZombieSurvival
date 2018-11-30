using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour {

    public TMP_Text coinText;
    public TMP_Text coinText2;

    public int coins;
    public int startCoins = 100;

    // Use this for initialization
    void Start () {
        coins = startCoins;
	}
	
	// Update is called once per frame
	void Update () {
        coinText.text = "" + coins;
        coinText2.text = "" + coins;

    }

    public void GetCoins(int coinsGotten)
    {
        coins += coinsGotten;
    }
}
