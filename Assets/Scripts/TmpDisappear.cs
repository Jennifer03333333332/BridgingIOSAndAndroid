using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyPage());
    }


    IEnumerator DestroyPage()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
