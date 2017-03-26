using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    #endregion

    public Text P1HPText;
    public Text P2HPText;
    public Character P1;
    public Character P2;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 100;
    }

    void Start ()
    {

    }
	
	void Update ()
	{
	    P1HPText.text = "HP \n" + (int)P1.Stats.CurrentHp + "-" + (int)P1.Stats.MaxHp + "\n";
        P1HPText.text += "Stamina \n" + (int)P1.Stats.CurrentStamina + "-" + (int)P1.Stats.MaxStamina;
        P2HPText.text = "HP \n" + (int)P2.Stats.CurrentHp + "-" + (int)P2.Stats.MaxHp + "\n";
        P2HPText.text += "Stamina \n" + (int)P2.Stats.CurrentStamina + "-" + (int)P2.Stats.MaxStamina;
    }

    public static int GetFrameCount(float inp)
    {
        return Mathf.RoundToInt(inp / Time.fixedDeltaTime);
    }

    public static Direction FloatToDirection(float inp)
    {
        return inp > 0 ? Direction.Right : Direction.Left;
    }
}
