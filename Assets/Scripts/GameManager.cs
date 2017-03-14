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
	    P1HPText.text = P1.Stats.CurrentHp + "\n-\n" + P1.Stats.MaxHp;
        P2HPText.text = P2.Stats.CurrentHp + "\n-\n" + P2.Stats.MaxHp;
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
