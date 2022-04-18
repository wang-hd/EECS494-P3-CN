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
        "K-11B: 初始鱼竿。\n适用于大多数钓鱼场合。",
        "K-1C: 对钓上大鱼很有帮助。",
        "Axiom-9000: 为懒人量身定制的鱼竿。不过要小心懒可能会付出代价。",
        "K-1D: 对钓上小鱼很有帮助。",
        "K-11C: K-11B的上位替代产品。",
        "Horizontal-001: 这个超强的鱼竿重新定义了钓鱼。",
        "Ganesha: 想要一箭双雕吗？来试试这个鱼竿吧。",
        "Reverse-zero: 这个鱼竿用反重力材料制成。这种新材料目前噱头的成分居多。"
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
        "长春谷: 我的小屋附近的一处池塘。",
        "未来道具研究所: 我曾经工作过的地方。年久失修，这里的大部分设施都已经没法用了。",
        "黑龙港: 被遗弃的滨海军事基地。第一场投入使用轻型核武器的战争就发生在这里。",
        "州湖: 它曾是个美丽的地方。然后一颗核弹在这里试爆，然后它变成了一片沙漠。多年之后它又恢复了美丽——但已不是以前那种美丽了。",
        "前海: 实际上它位于岛的“后”方。被海浪冲上岸又堆积如山的生活垃圾提醒我海的那边还有人活着，无论是敌人还是朋友。",
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
          return "小屋: 岛上唯一温暖的地方。我属于这里。我爱这里。";
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