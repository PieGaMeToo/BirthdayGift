using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsLogic : MonoBehaviour
{
    public GameObject clickables;
    private int litCandlesCount = 7;
    private int currentCounter = 0;

    private void Update()
    {
        currentCounter = 0;
        foreach (Transform candle in clickables.transform)
        {
            if (candle.gameObject.activeSelf)
            {
                currentCounter++;
            }
        }

        if(currentCounter != litCandlesCount)
        {
            litCandlesCount -= 1;
            gameObject.transform.GetChild(currentCounter).gameObject.SetActive(true);
        }
    }
}
