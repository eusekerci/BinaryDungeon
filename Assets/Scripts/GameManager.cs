using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = 100;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public static int GetFrameCount(float inp)
    {
        return Mathf.RoundToInt(inp / Time.fixedDeltaTime);
    }
}
