using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text P1HPText;
    public Text P2HPText;
    public CombatController P1Controller;
    public CombatController P2Controller;
    public CharacterData P1Data;
    public CharacterData P2Data;

    void Awake()
    {
        Application.targetFrameRate = 100;
    }

    void Start ()
    {
        P1Data = P1Controller.GetCharacterData();
        P2Data = P2Controller.GetCharacterData();
    }
	
	void Update ()
	{
	    P1HPText.text = P1Data.Hp + "\n-\n" + P1Data.MaxHp;
        P2HPText.text = P2Data.Hp + "\n-\n" + P2Data.MaxHp;
    }

    public static int GetFrameCount(float inp)
    {
        return Mathf.RoundToInt(inp / Time.fixedDeltaTime);
    }
}
