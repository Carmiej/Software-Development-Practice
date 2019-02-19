﻿using UnityEngine;
using System;
using System.Xml;

public class Room : ScriptableObject
{
    private GameObject floorTile;
    private GameObject wallTile;
    private GameObject nothing;
    public int width;
    public int height;

    private XmlNode thisRoom;
    private GameObject room;
    private Transform roomHolder;

    public void with(XmlNode thisRoom, bool n, bool s, bool e, bool w, GameObject floorTile, GameObject wallTile, GameObject nothing)
    {
        this.thisRoom = thisRoom;
        height = Int32.Parse(thisRoom.Attributes["HEIGHT"].InnerText);
        width = Int32.Parse(thisRoom.Attributes["WIDTH"].InnerText);
        this.floorTile = floorTile;
        this.wallTile = wallTile;
        this.nothing = nothing;

        string whatToMake = "Creating room " + thisRoom.Attributes["ID"].InnerText +
            " with layout:\n" +
            thisRoom["LAYOUT"].InnerText;
        Debug.Log(whatToMake);

        Start();
    }

    public GameObject getRoom() {
        return room;
    }

    public void Start()
    {
        room = new GameObject("Room");
        roomHolder = room.transform;

        // problem block
        int stringIndex = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject toInstantiate = nothing;
                switch (thisRoom["LAYOUT"].InnerText[stringIndex]) // look at the character in a specific place in the layout text
                {
                    case 'W':
                        toInstantiate = wallTile;
                        break;
                    case 'F':
                        toInstantiate = floorTile;
                        break;
                    default:
                        break;
                }

                

                GameObject instance = Instantiate(toInstantiate, new Vector3(j, i, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(roomHolder);

                stringIndex++; // move to the next character in the string
            }
        }
    }
}
