public class ToastRequest
{
    public string message;
    public float duration;
    public bool isPauseable;
    public bool isTaskComplete;
    public bool isStoryUnlock;

    public ToastRequest(string s, float _duration = 0.5f, bool _isPauseable = false, bool _isTaskComplete = false, bool _isStoryUnlock = false)
    {
        message = s;
        duration = _duration;
        isPauseable = _isPauseable;
        isTaskComplete = _isTaskComplete;
        isStoryUnlock = _isStoryUnlock;
    }

    public override string ToString()
    {
        return "Toast set to : " + message;
    }
}

public class ToastCrossOut
{
    public ToastCrossOut() {}
}

public class RemoveLine
{
    public RemoveLine() {}
}

public class TutorialProcessEvent
{
    public TutorialProcessEvent() {}
}

public class TutorialCastEvent
{
    public TutorialCastEvent() {}
}

public class fish_hooked_event
{
    // This class is not complete, feel free to change anything.
    public string message;
    //public int fishHookedID;

    public fish_hooked_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "Fish hooked event: " + message;
    }
}

public class pull_fish_event
{
    public string message;

    public pull_fish_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "Pull fish event: " + message;
    }
}

public class get_fish_event
{
    public string message;
    public int fishHookedID;

    public get_fish_event(string s, int fishID)
    {
        message = s;
        fishHookedID = fishID;
    }

    public override string ToString()
    {
        return "Get fish event: " + message + "with ID: " + fishHookedID.ToString();
    }
}

public class fish_escape_event
{
    // This class is not complete, feel free to change anything.
    public string message;

    public fish_escape_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "Fish escape event: " + message;
    }
}

public class alter_health_event
{
    public int alterHealth;

    public alter_health_event(int alterHealth)
    {
        this.alterHealth = alterHealth;
    }

    public override string ToString()
    {
        return "Alter health event: changed " + alterHealth + "health";
    }
}

public class alter_hunger_event
{
    public int alterHunger;

    public alter_hunger_event(int alterHunger)
    {
        this.alterHunger = alterHunger;
    }

    public override string ToString()
    {
        return "Alter hunger event: changed " + alterHunger + "hunger";
    }
}

public class refresh_the_day
{
    public int day;

    public refresh_the_day(int day)
    {
        this.day = day;
    }

    public override string ToString()
    {
        return "Day: " + day;
    }
}

public class use_fish_event
{
    public float weight;

    public use_fish_event(float _weight)
    {
        weight = _weight;
    }

    public override string ToString()
    {
        return "Use fish event with weight: " + weight;
    }
}

public class time_to_nignt
{
    public time_to_nignt() {}
}

public class show_instruction
{
    public string instruction_name;
    public bool is_begin;
    public show_instruction(string _instruction_name, bool _is_begin)
    {
        instruction_name = _instruction_name;
        is_begin = _is_begin;
    }
}

public class reach_level_event
{
    public int level_idx;

    public reach_level_event(int _level_idx)
    {
        level_idx = _level_idx;
    }

    public override string ToString()
    {
        return "Reach level: " + level_idx;
    }
}

public class leave_level_event
{

    public leave_level_event() {}

    public override string ToString()
    {
        return "Leave level ";
    }
}

// home
public class switch_rod_event
// this event changes selection in rod panel, but not confirm
{
    public switch_rod_event() {}

    public override string ToString()
    {
        return "Switch rod";
    }
}

public class change_rod_event
// this event confirm rod switch
{
    public change_rod_event() {}

    public override string ToString()
    {
        return "Change rod";
    }
}

public class switch_fish_event
{
    public switch_fish_event() {}

    public override string ToString()
    {
        return "Switch fish";
    }
}

public class update_task_event
{
    public int task;
    public int step;

    public update_task_event(int _task, int _step)
    {
        task = _task;
        step = _step;
    }

    public override string ToString()
    {
        return "Update task " + task + "with step" + step;
    }
}

public class fish_info_event
{
    public string message;
    public int fishInfoID;

    public fish_info_event(string s, int fishID)
    {
        message = s;
        fishInfoID = fishID;
    }

    public override string ToString()
    {
        return "Fish Info event: " + message + "with ID: " + fishInfoID.ToString();
    }
}

public class unlock_item_event
{
    public int itemID;

    public unlock_item_event(int itemID)
    {
        this.itemID = itemID;
    }

    public override string ToString()
    {
        return "Unlock Item Event: unlock item with ID: " + itemID;
    }
}

public class tutorial_get_fish_event
{
    public string message;

    public tutorial_get_fish_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "Get fish event: " + message;
    }
}

public class tutorial_fish_escape_event
{
    // This class is not complete, feel free to change anything.
    public string message;

    public tutorial_fish_escape_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "Fish escape event: " + message;
    }
}

public class get_item_event
{
    public int itemID;

    public get_item_event(int itemID)
    {
        this.itemID = itemID;
    }

    public override string ToString()
    {
        return "Get Item Event: unlock item with ID: " + itemID +" (" + FishList.GetFishWithFishID(itemID) + ")";
    }
}

public class close_caught_fish_panel_event
{

    public string message;

    public close_caught_fish_panel_event(string s)
    {
        message = s;
    }

    public override string ToString()
    {
        return "close_caught_fish_panel_event: " + message;
    }
}