using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    private int clicked;
    // Start is called before the first frame update
    void Start()
    {
        clicked = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        
    }

    private void OnMouseDown()
    {
        print(++clicked);
    }
}