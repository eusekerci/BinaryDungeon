using System;

[Serializable]
public class CharacterData
{
    private int maxHP;
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
        set
        {
            maxHP = value;
        }
    }

    private int hp;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }


}
