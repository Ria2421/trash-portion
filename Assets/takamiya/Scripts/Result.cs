using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {

    
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FadeTitle()
    {
        Initiate.Fade("Title", Color.black, 1.0f);
    }
}
