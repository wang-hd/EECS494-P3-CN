using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionScreenUsage
{
    to_dead = 1,
    to_customize = 2,
    to_home = 3,
    to_finish = 4,
}


public static class StaticData
{

    public static int day = 1;
    public static int ini_time = 480;

    public static List<int> inventory = new List<int>();

    //test code
    //public static List<int> inventory = new List<int>{0, 1};

    public static int fish_list_num = 0;
    public static float fish_weight_sum = 0.0f;

    public static int health = 100;

    public static int hunger = 100;

    public static TransitionScreenUsage usage = 0;
    // 1: dead: end the game, clear the StaticData and load the start scene;
    // 2: continue: Pass the message on Screen the load the Fishing Scene;
    // 3: Come Home: Load the ComeHome panel;
    public static string message = "You Are Dead";
    public static string nextScene = "Begin";

    //Task
    public static int task_number = 4;
    public static int step_number = 3;
    public static int story_progress = 0;
    public static bool[,] task_status = new bool[task_number, step_number + 1];

    //Map
    public static int map_progress = 0;
    //public static int map_update = 0;

    private static bool[] index = { };

    // ending
    // 0=not ending, 1=normal ending, 2=true ending
    public static int ending = 1;

    // tutorial
    // 0=finished or skipped, other=step
    public static int tutorial_step = 1;


    // home scene
    public static bool has_open_panel = false;
    public static int bones = 0;
    public static int bones_accrued;
    public static List<string> rods = new List<string> { "K-11B", "K-1C" , "Axiom-9000", "K-1D", "K-11C", "Horizontal-001", "Ganesha", "Reverse-zero" };
    public static List<bool> rod_unlock_status = new List<bool> { true, false, false, false, false, false, false, false };
    public static List<int> rod_unlock_values = new List<int> { 0, 5, 1, 5, 8, 13, 11, 1};
    public static int current_rod_index = 0;
    public static List<string> rod_descriptions = new List<string>
    {
        "K-11B: Most common fishing rod.\nSuitable for various fishing situations.",
        "K-1C: Great for catching large fishes.",
        "Axiom-9000: A rod for lazy people, but keep in mind that laziness has consequences",
        "K-1D: Great for catching small fishes.",
        "K-11C: Improved version of K-11B.",
        "Horizontal-001: This rod really likes the smallest fish.",
        "Ganesha: Want more fishes? Try this one.",
        "Reverse-zero: This rod is built with anti-gravity material. This new technology is still experimental."
    };

    public static void setIndexSize(int size)
    {
        index = new bool[size];
        for (int i = 0; i < size; ++i)
        {
            index[i] = false;
        }
    }

    public static void unlockItem(int itemID)
    {
        index[itemID] = true;
    }

    public static int getIndexLength()
    {
        return index.Length;
    }

    public static bool checkIndex(int itemID)
    {
        return index[itemID];
    }

    public static List<string> levels = new List<string> { "Long Spring Valley", "Future Gadget Laboratory", "Black Dragon Harbor", "State Lake", "Upper Sea" };
    public static List<List<int>> fishInEachLevel = new List<List<int>>() {
        new List<int> { 0, 1, 4, 5, 16 } , 
        new List<int> { 0 ,1 , 2, 3, 6, 18 } , 
        new List<int> { 0 ,1 , 2, 3, 7, 17 }, 
        new List<int> { 0, 1, 8, 9, 10, 11, 19 } , 
        new List<int> { 0, 1, 12, 13, 14, 15, 20, 4, 6, 8 } 
    };
    public static List<string> level_stories = new List<string>
    {
        "Long Spring Valley: A pond in foreset near the house.",
        "Future Gadget Laboratory: A lab I used to work at. Without maintenance, most facilities there can no longer be used.",
        "Black Dragon Harbor: An abandoned military base. A war happened there 8 years ago, where light nuclear weapons are first put into use.",
        "State Lake: It was a beautiful place. Then nuclear explosion test happens and it becomes a desert. Years later it is a beautiful place again.",
        "Upper Sea: Actually it is 'down' in the map. Piled up garbages hint me that someone is still alive on the other side of the sea, whether friend or enemy.",
    };

    public static int currentLevelIdx = 0;
    public static string currentLevel = levels[currentLevelIdx];
    public static string getLevelNameByLevelIdx(int levelIdx)
    {
        return levels[levelIdx];
    }

    public static string getLevelStoryByLevelIdx(int levelIdx)
    {
        if(levelIdx == -2){
          return "Home: The warmest place here. I belong to here. I love here.";
        }
        return level_stories[levelIdx];
    }



    public static int[] mini_game_count_in_each_level= new int[]{0,0,0};



    public static void Refresh()
    {
        day = 1;
        inventory = new List<int>();
        fish_list_num = 0;
        fish_weight_sum = 0.0f;
        usage = 0;
        nextScene = "Begin";

        health = 100;
        hunger = 100;

        bones = 0;
        bones_accrued = 0;

        //story_progress = 0;
        //task_status = new bool[task_number, step_number + 1];
        map_progress = 0;

        currentLevelIdx = 0;
        mini_game_count_in_each_level = new int[] { 0, 0, 0 };

        ending = 0;
    }

    public static void clearInventory()
    {
        inventory.Clear();
    }


    // special items
    // fishID to unlock status
    // reference: https://docs.google.com/spreadsheets/d/1EwO5oT_k5SGgUuexpd1Fh32wK2tTW5QwVMDWuuYFngU/edit#gid=667286017
    public static Dictionary<int, bool> special_item_unlock_status = new Dictionary<int, bool> {
        {16, false}, {17, false}, {18, false}, {19, false}, {20, false}
        //{16, true}, {17, true}, {18, true}, {19, true}, {20, true}
    };
}