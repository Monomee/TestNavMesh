using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public int capacity;
    [SerializeField] GameObject[] listOfProduct;
    int[] W;
    int[] V;
    int numOfObj;
    int[,] DP;

    public List<GameObject> thiefBag = new List<GameObject>();
    public TextMeshPro sumary;
    // Start is called before the first frame update
    void Start()
    {     
        numOfObj = listOfProduct.Length;
        DP = new int[numOfObj + 1, capacity + 1];
        W = new int[numOfObj];
        V = new int[numOfObj];
        for (int i = 0; i < numOfObj; i++)
        {
            W[i] = listOfProduct[i].GetComponent<ProductDetail>().weight;
            V[i] = listOfProduct[i].GetComponent<ProductDetail>().value;
        }
        SetSumaryText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetMaxValue()
    {
        for (int i = 1; i <= numOfObj; i++)
        {
            for (int j = 0; j <= capacity; j++)
            {
                if (W[i - 1] > j)
                {
                    DP[i, j] = DP[i - 1, j];
                }
                else
                {
                    DP[i, j] = Mathf.Max(DP[i - 1, j], DP[i - 1, j - W[i - 1]] + V[i - 1]);
                }
            }
        }
        return DP[numOfObj, capacity];
    }

    public int GetWeightInBag()
    {
        int weight = 0;
        if (thiefBag.Count > 0)
        {
            for (int i = 0; i < thiefBag.Count; i++)
            {
                weight += thiefBag[i].GetComponent<ProductDetail>().weight;
            }
        }       
        return weight;
    }
    public int GetValueInBag()
    {
        int value = 0;
        if (thiefBag.Count > 0)
        {
            for (int i = 0; i < thiefBag.Count; i++)
            {
                value += thiefBag[i].GetComponent<ProductDetail>().value;
            }
        }
        return value;
    }
    public void SetSumaryText()
    {
        string str = "";
        str = "Capacity: " + capacity +
            "\nMax Value Can Take: " + GetMaxValue() +
            "\nCurrent Value: " + GetValueInBag() +
            "\nItems taken: ";
        if (thiefBag.Count > 0)
        {
            for(int i = 0;i < thiefBag.Count; i++)
            {
                str += "\n" + thiefBag[i].name;
            }
        }
        else
        {
            str += "None";
        }
        sumary.text = str;
    }
}
