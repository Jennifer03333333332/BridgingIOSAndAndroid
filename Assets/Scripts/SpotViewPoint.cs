using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotViewPoint : MonoBehaviour
{
    private bool init;
    public GameObject MeshPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            
            EnableObjects();

            init = true;
        }
    }

    public void EnableObjects()
    {
        if (MeshPart.transform.childCount > 0)
        {
            for (int i = 0; i < MeshPart.transform.childCount; i++)
            {
                print(MeshPart.transform.GetChild(i).gameObject);
                MeshPart.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
