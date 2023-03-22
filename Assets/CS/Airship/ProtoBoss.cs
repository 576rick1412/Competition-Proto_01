using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBoss : MonoBehaviour
{
    public GameObject ins;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            var temp = Instantiate(ins,transform.position,Quaternion.identity);
            temp.GetComponent<Pattern>().Start_DrawLine(6, 5, 0, 0.2f, 1, 1.5f, 30);
        }
    }
}
