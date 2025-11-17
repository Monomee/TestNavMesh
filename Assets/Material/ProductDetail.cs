using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductDetail : MonoBehaviour
{
    public Product product;
    public int weight;
    public int value;

    bool canTake = false;
    public Thief thief;
    public LayerMask layer;
    public TextMeshPro text;
    // Start is called before the first frame update
    void Awake()
    {
        product.Init();
        weight = product.weight;
        value = product.value;
        this.GetComponent<Renderer>().material = product.material;
        string detail = "Weight :" + weight + "\nValue: " + value;
        text.text = detail;
    }

    private void Update()
    {
        if (canTake) TakeObject();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTake = true;
            Debug.Log("Can Take: " + this.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTake = false;
        }
    }

    public void TakeObject()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (thief.GetWeightInBag() + this.weight > thief.capacity)
            {
                Debug.Log("Overweight! Can Take More!");
            }
            else
            {
                thief.thiefBag.Add(this.gameObject);
                this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
                this.gameObject.layer = LayerMask.NameToLayer("Product");
                thief.SetSumaryText();
            }             
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (thief.thiefBag.Count > 0)
            {
                thief.thiefBag.Remove(this.gameObject);
                this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                thief.SetSumaryText();
            }               
        }
        
    }
}
