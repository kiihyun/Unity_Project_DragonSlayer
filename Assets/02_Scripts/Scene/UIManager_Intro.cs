using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Intro : MonoBehaviour
{
    private bool _pressAnyButton = false;
       // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButton(0))
        {
            _pressAnyButton = true;
        }
    }
}
