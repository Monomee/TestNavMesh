using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAndSharedMaterialEg : MonoBehaviour
{
    public Renderer sphere1;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterial();
        //ChangeSharedMaterial();
    }

    void ChangeMaterial()  //actually that is creating new material and set into object
    {
        sphere1.material.color = material.color;
        sphere1.material.color = Color.cyan;
    }
    void ChangeSharedMaterial()
    {
        sphere1.sharedMaterial.color = material.color;
    }
}
