using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("落ちたボールの名前：" + other.gameObject.name);
        other.gameObject.SetActive(false);
    }
}
