using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {
    public StatsMenuScript sms;

    public float speed { get; set; }
    public float rotationSpeed { get; set; }
    public int level { get; set; }
    public Text levelText;
    public float souls { get; set; }
    public float maxSouls { get; set; }
    public Slider soulSlider;
    float armor;
    public float baseHP;
    public float hp;
    public float maxHp;
    public Slider hpSlider;
    float mana;
    float damage;
    float ap;

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            levelText.text = "" + level;
            MaxSouls = Mathf.Pow(level, 1.5f) * 1.2f + 10;
            HP += (maxHp - HP) * .5f;
        }
    }

    public float Souls
    {
        get
        {
            return souls;
        }
        set
        {
            souls = value;
            soulSlider.value = souls;
            if (souls >= maxSouls)
            {
                Level += 1;
                sms.TotalAttributes++;
                float overFlow = souls - maxSouls;
                Souls = overFlow;
            }
        }
    }

    public float MaxSouls
    {
        get
        {
            return maxSouls;
        }
        set
        {
            maxSouls = value;
            soulSlider.maxValue = maxSouls;
        }
    }

    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
            else if (hp <= 0)
            {
                hp = 0;
            }

            hpSlider.value = hp / maxHp;
        }
    }

    public void AttributeAddHP(float amount)
    {
        maxHp += amount;
        HP += amount;
    }
    public float Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
        }
    }
    public float AD
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public float AP
    {
        get
        {
            return ap;
        }
        set
        {
            ap = value;
        }
    }
    public float Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
        }
    }


    private void Start()
    {
        level = 1;
        baseHP = 20;
        maxHp = baseHP;
        HP = maxHp;
        souls = 0;
        speed = 10f;
        rotationSpeed = 120;
        damage = 1;
        armor = 0;

        Level = level;
        MaxSouls = maxSouls;
        Souls = souls;

        if (Hacks.hacks)
        {
            speed = 20;
            damage = 1;
        }
    }
}
