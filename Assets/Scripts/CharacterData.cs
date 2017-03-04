using System;

[Serializable]
public class CharacterData
{
    private int _maxHp;
    public int MaxHp
    {
        get
        {
            return _maxHp;
        }
        set
        {
            _maxHp = value;
        }
    }

    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }


}
