using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllingModels : MonoBehaviour
{
    // Start is called before the first frame update
    void UpdateModel(Vector3 touchedPos)
    {
        touchedPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
