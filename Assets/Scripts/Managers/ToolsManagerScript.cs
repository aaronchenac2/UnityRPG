using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManagerScript : MonoBehaviour
{
    public GameObject[] toolsGOs;
    Tool[] tools { get; set; }
    int numTools;

    void Start()
    {
        // Gets children and initializes tools
        numTools = toolsGOs.Length;
        tools = new Tool[numTools];

        // Assigns References
        for (int j = 0; j < numTools; j++)
        {
            RegisterTool(j, toolsGOs[j]);
        }

        // Deactivates children
        for (int j = 0; j < numTools; j++)
        {
            tools[j].go.SetActive(false);
        }
    }

    void RegisterTool(int index, GameObject go)
    {
        tools[index] = new Tool();
        Tool work = tools[index];
        work.go = go;
        work.name = go.name;
        work.status = false;
        work.unlocked = false;
        if (Hacks.hacks)
        {
            work.unlocked = true;
        }
        Debug.Log(index + " : " + work.name);
    }

    // If any tools are active. Basically hand mode
    public bool HasChildren()
    {
        for (int j = 0; j < numTools; j++)
        {
            if (tools[j].status == true)
            {
                return true;
            }
        }
        return false;
    }

    public bool Contains(string name)
    {
        for (int j = 0; j < numTools; j++)
        {
            if (tools[j].name == name)
            {
                return true;
            }
        }
        return false;
    }

    // Changes tool to specified tool name
    // If name is not found, all tools are disabled
    // -used for Toss
    public void ChangeTool(string toolName)
    {
        for (int j = 0; j < numTools; j++)
        {
            if (tools[j].name == toolName)
            {
                tools[j].go.SetActive(true);
                tools[j].status = true;
                tools[j].unlocked = true;
            }
            else
            {
                tools[j].go.SetActive(false);
                tools[j].status = false;
            }
        }
    }

    // Traverse to next unlocked tool
    public void NextTool()
    {
        // If tries to traverse with no tools, immediately returns
        bool noneUnlocked = true;
        for (int j = 0; j < numTools; j++)
        {
            if (tools[j].unlocked)
            {
                noneUnlocked = false;
            }
        }
        if (noneUnlocked)
        {
            return;
        }

        // Gets index of active tool
        // If no active tool, start from first tool in list
        int activeTool = GetActiveTool();
        if (activeTool == -1)
        {
            activeTool++;
        }

        // Sets active tool to false
        tools[activeTool].go.SetActive(false);
        tools[activeTool].status = false;

        // Gets the next tool to start traverse
        // If active tool was the last in array, wraps to beginning
        int startingTool = activeTool + 1;
        if (startingTool == numTools)
        {
            startingTool = 0;
        }

        // Starts traversing through 2nd half of array
        for (int j = startingTool; j < numTools; j++)
        {
            if (tools[j].unlocked && (tools[j].status == false))
            {
                tools[j].go.SetActive(true);
                tools[j].status = true;
                return;
            }
        }

        // Wraps around array and starts from beginning
        for (int j = 0; j < activeTool; j++)
        {
            if (tools[j].unlocked && (tools[j].status == false))
            {
                tools[j].go.SetActive(true);
                tools[j].status = true;
                return;
            }
        }

        // If no other tools were unlocked, re-enable the current tool
        tools[activeTool].go.SetActive(true);
        tools[activeTool].status = true;
    }

    // Returns index of active tool
    int GetActiveTool()
    {
        for (int j = 0; j < numTools; j++)
        {
            if (tools[j].status)
            {
                return j;
            }
        }
        return -1;
    }

    public string GetActiveToolName()
    {
        int activeToolIndex = GetActiveTool();
        if (activeToolIndex >= 0)
        {
            return tools[GetActiveTool()].name;
        }
        return "NoToolEquipped";
    }

    // Tool class
    public class Tool
    {
        public GameObject go { get; set; }
        public string name { get; set; }
        public bool status { get; set; }
        public bool unlocked { get; set; }

        public Tool()
        {
        }
    }

}
