using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class Product : ScriptableObject
{
    public string productName;
    public int weight;
    public int value;
    public Material material;
    public void Init()
    {
        this.weight = UnityEngine.Random.Range(1, 20);
        this.value = UnityEngine.Random.Range(1, 20); 
    }
}
