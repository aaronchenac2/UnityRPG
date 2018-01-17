using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuScript : MonoBehaviour {
    public Stats stats;
    public GameObject[] attributeGOs;
    Attribute[] attributes;
    int totalAttributes;
    public int TotalAttributes
    {
        get
        {
            return totalAttributes;
        }
        set
        {
            totalAttributes = value;
            attributeCountText.text = "" + value;
        }

    }
    public Text attributeCountText;



    private void Start()
    {
        attributes = new Attribute[attributeGOs.Length];
        for (int j = 0; j < attributeGOs.Length; j++)
        {
            RegisterAttribute(j, attributeGOs[j]);
        }

        TotalAttributes = 1;
    }

    void RegisterAttribute(int index, GameObject go)
    {
        Attribute work = attributes[index] = new Attribute();
        work.name = attributeGOs[index].name;
        work.countText = attributeGOs[index].transform.Find("Count").GetComponent<Text>();
        work.plus = attributeGOs[index].transform.Find("Plus").GetComponent<Button>();
        work.minus = attributeGOs[index].transform.Find("Minus").GetComponent<Button>();
        work.plus.onClick.AddListener(delegate { PlusAttribute(work.name); });
        work.minus.onClick.AddListener(delegate { MinusAttribute(work.name); });
    }

    Attribute GetAttribute(string name)
    {
        for (int j = 0; j < attributes.Length; j++)
        {
            if (attributes[j].name.Equals(name))
            {
                return attributes[j];
            }
        }
        return null;
    }

    // When plus button is pressed, increase the corresponding attribute by one
    void PlusAttribute (string s)
    {
        if (TotalAttributes <= 0)
        {
            return;
        }

        TotalAttributes--;
        Attribute work = GetAttribute(s);
        work.count++;
        work.countText.text = "" + work.count;

        if (s.Equals("HP"))
        {
            stats.AttributeAddHP(1);
        }
        else if (s.Equals("Mana"))
        {
            stats.Mana += 1;
        }
        else if (s.Equals("AD"))
        {
            stats.AD += 1;

        }
        else if (s.Equals("AP"))
        {
            stats.AP += 1;

        }
        else if (s.Equals("Armor"))
        {
            stats.Armor += 1;
        }
    }

    void MinusAttribute( string s)
    {
        Attribute work = GetAttribute(s);
        if (work.count == 0)
        {
            return;
        }
        TotalAttributes++;
        work.count--;
        work.countText.text = "" + work.count;

        if (s.Equals("HP"))
        {
            stats.AttributeAddHP(-1);
        }
        else if (s.Equals("Mana"))
        {
            stats.Mana -= 1;

        }
        else if (s.Equals("AD"))
        {
            stats.AD -= 1;

        }
        else if (s.Equals("AP"))
        {
            stats.AP -= 1;

        }
        else if (s.Equals("Armor"))
        {
            stats.Armor -= 1;

        }
    }

    public class Attribute
    {
        public string name { get; set; }
        public int count { get; set; }
        public Text countText { get; set; }
        public Button plus { get; set; }
        public Button minus { get; set; }

        public Attribute()
        {
            count = 0;
        }
    }
}
