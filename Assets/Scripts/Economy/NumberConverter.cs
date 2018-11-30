using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberConverter : MonoBehaviour
{

    private static NumberConverter instance;

    public static NumberConverter Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance = null)
        {
            instance = this;
        }
    }

    public string GetNumberIntoString(double valueToConvert, bool smallNumber)
    {
        string converted;
        if (valueToConvert >= 1000000000000000000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000000000000000000f).ToString("f2") + " Dec";
        }
        else if (valueToConvert >= 1000000000000000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000000000000000f).ToString("f2") + " Non";
        }
        else if (valueToConvert >= 1000000000000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000000000000f).ToString("f2") + " Oct";
        }
        else if (valueToConvert >= 1000000000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000000000f).ToString("f2") + " Sep";
        }
        else if (valueToConvert >= 1000000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000000f).ToString("f2") + " Sex";
        }
        else if (valueToConvert >= 1000000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000000f).ToString("f2") + " Qui";
        }
        else if (valueToConvert >= 1000000000000000f)
        {
            converted = (valueToConvert / 1000000000000000f).ToString("f2") + " Qua";
        }
        else if (valueToConvert >= 1000000000000f)
        {
            converted = (valueToConvert / 1000000000000f).ToString("f2") + " Tri";
        }
        else if (valueToConvert >= 1000000000f)
        {
            converted = (valueToConvert / 1000000000f).ToString("f2") + " Bil";
        }
        else if (valueToConvert >= 1000000f)
        {
            converted = (valueToConvert / 1000000f).ToString("f2") + " Mil";
        }
        else if (valueToConvert >= 1000f)
        {
            converted = (valueToConvert / 1000f).ToString("f2") + " K";
        }
        else
        {
            if (smallNumber == true)
            {
                converted = "" + valueToConvert.ToString("f2");
            }
            else
            {
                converted = "" + valueToConvert.ToString("f0");
            }
        }
        return converted;
    }
}
