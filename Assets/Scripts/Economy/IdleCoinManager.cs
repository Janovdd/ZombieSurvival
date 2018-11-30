using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleCoinManager : MonoBehaviour {

    public TMP_Text coinText;
    public TMP_Text survivorText;
    public TMP_Text diamondText;
    public float coins;
    public float diamonds;
    public float cps;
    public float survivors;
    public float moneyPerServivor;
    private NumberConverter NC;

    void Start()
    {
        StartCoroutine(AutoTick());
        NC = new NumberConverter();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = NC.GetNumberIntoString(coins, false);
        survivorText.text = NC.GetNumberIntoString(survivors, false);
        diamondText.text = NC.GetNumberIntoString(diamonds, false);
    }

    public float GetCPS()
    {
        cps = survivors * moneyPerServivor;
        return cps;
    }

    public void AutoCPS()
    {
        coins += GetCPS() / 10;
    }

    IEnumerator AutoTick()
    {
        while (true)
        {
            AutoCPS();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
