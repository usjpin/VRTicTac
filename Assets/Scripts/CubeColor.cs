using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{

    public void toRed()
	{
        GetComponent<Renderer>().material.color = Color.red;
	}
    public void toGreen()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void toBlue()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
