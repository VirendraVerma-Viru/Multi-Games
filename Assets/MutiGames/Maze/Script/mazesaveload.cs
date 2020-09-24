﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

public class mazesaveload : MonoBehaviour
{
    public static int highscore = 0;
    public static int currentscore = 0;
    public static int level = 1;
    public static int adsFrequency = 0;
	public static int adShown = 0;
    public static int isreturn = 0;
	
    public static string filename = "infomaze.dat";
    public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
        Maze_Data data = new Maze_Data();
		

		data.Highscore = highscore;
        data.CurrentScore = currentscore;
        data.Level = level;
        data.Isreturn = isreturn;
        data.AdsFrequency = adsFrequency;
		data.AdsSshown = adShown;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
	{

        if (File.Exists(Application.persistentDataPath + "/" + filename))
        {
			BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            Maze_Data data = (Maze_Data)bf.Deserialize(file);

			highscore = data.Highscore;
            currentscore=data.CurrentScore;
            level = data.Level;
            isreturn = data.Isreturn;
            adsFrequency = data.AdsFrequency;
			adShown = data.AdsSshown;

            file.Close();

        }
        else
            mazesaveload.Save();
    }

}


[Serializable]
class Maze_Data
{
    public int Highscore;
    public int CurrentScore;
    public int Level;
    public int Isreturn;
    public int AdsFrequency;
	public int AdsSshown;
}


