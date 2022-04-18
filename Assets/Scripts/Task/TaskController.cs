using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum StatusOfPanel
{
    close = 0,
    TodoTask = 1,
    FinishedTask = 2,
}

public class TaskController : MonoBehaviour
{
    [SerializeField] GameObject TodoTaskButton;
    [SerializeField] GameObject FinishedButton;
    [SerializeField] GameObject CloseButton;
    [SerializeField] GameObject OpenButton;

    [SerializeField] GameObject TodoTaskText;
    [SerializeField] GameObject FinishedText;

    [SerializeField] GameObject TodoTaskPanel;
    [SerializeField] GameObject FinishedTaskPanel;

    Text TodoTaskTextContent;
    Text FinishedTaskContent;

    string[,] TaskLists = {
        {"生存", "钓上第一条鱼", "用操作台把鱼换成鱼骨", "被鱼攻击"},
        {"探索", "解锁一个新鱼竿", "在一天之内钓上2条bombat", "钓上一条超超稀有的鱼"},
        {"收获", "累计获得30个鱼骨","存活到第9天", "吃掉一条恢复10点以上生命的鱼"},
        {"征服", "钓上一条Yang", "钓上一条Orca.", "钓上一条Crocodile."},
    };

    Subscription<update_task_event> update_task_sub;
    Subscription<get_fish_event> get_fish_sub;
    Subscription<alter_health_event> alter_health_sub;


    StatusOfPanel status = StatusOfPanel.close;

    bool is_first_fish = true;
    bool is_first_SSR = true;
    bool is_first_killer_whale = true;
    int is_first_2_bombat = 0;
    bool is_first_cubone = true;
    bool is_first_yang = true;
    bool is_first_crocodile = true;
    bool is_first_anglerfish = true;


    void Awake()
    {
        update_task_sub = EventBus.Subscribe<update_task_event>(_handleUpdateTask);
        get_fish_sub = EventBus.Subscribe<get_fish_event>(_handleGetFish);
        alter_health_sub = EventBus.Subscribe<alter_health_event>(_handleCureFish);

        TodoTaskButton.GetComponent<Button>().onClick.AddListener(_TodoTaskOnclick);
        FinishedButton.GetComponent<Button>().onClick.AddListener(_FinishButtonOnclick);
        CloseButton.GetComponent<Button>().onClick.AddListener(_CloseButtonOnclick);
        OpenButton.GetComponent<Button>().onClick.AddListener(_OpenButtonOnclick);

        TodoTaskTextContent = TodoTaskText.GetComponent<Text>();
        FinishedTaskContent = FinishedText.GetComponent<Text>();
        if(StaticData.day == 10){
            EventBus.Publish<update_task_event>(new update_task_event(3,2));
        }
    }

    // load the content of to-do task everytime the button is clicked.
    void LoadTodoTask()
    {
        TodoTaskTextContent.text = "";
        int task_num = 0;
        // filter the Static Data, show every Todo Task in bullet
        // use different signal to show finished step and unfinished step.
        // if no task then show" no task"⎷
        for(int i = 0; i < StaticData.task_number; i++)
        {
            if(!StaticData.task_status[i,0])
            {
                task_num++;
                TodoTaskTextContent.text += $"<size=42><b>Task {TaskLists[i,0]}</b></size>\n";
                for(int j = 1; j <= StaticData.step_number; j++)
                {
                    if(StaticData.task_status[i,j])
                    {
                        TodoTaskTextContent.text += $"   ☑ {TaskLists[i,j]}\n";
                    }else
                    {
                        TodoTaskTextContent.text += $"   ☐ {TaskLists[i,j]}\n";
                    }
                }
                TodoTaskTextContent.text += $"\n";
                
            }
        }
        if(task_num == 0)
        {
            TodoTaskTextContent.text = "你已经完成了所有的任务。";
        }
    }

    void LoadFinishedTask()
    {
        // filter the Static Data, show every finished Task in bullet
        // if no task then show" no task"
        int task_num = 0;
        FinishedTaskContent.text = "";
        for(int i = 0; i < StaticData.task_number; i++)
        {
            if(StaticData.task_status[i,0])
            {
                task_num++;
                FinishedTaskContent.text += "<size=42><b>Task " + TaskLists[i,0] +"</b></size>\n";
                for(int j = 1; j <= StaticData.step_number; j++)
                {
                    FinishedTaskContent.text += "     ·"+TaskLists[i,j]+"\n";
                }
                FinishedTaskContent.text += $"\n";
            }
        }
        if(task_num == 0)
        {
            FinishedTaskContent.text = "你还没有已完成的任务。";
        }
    }


    void RefreshPanel()
    {
        if(status == StatusOfPanel.TodoTask)
        {
            LoadTodoTask();
        }else if(status == StatusOfPanel.FinishedTask)
        {
            LoadFinishedTask();
        }
    }

    void CloseOriginalPanel()
    {
        if(status == StatusOfPanel.TodoTask)
        {
            TodoTaskPanel.SetActive(false);
        }else if(status == StatusOfPanel.FinishedTask)
        {
            FinishedTaskPanel.SetActive(false);
        }
    }
    
    void _handleUpdateTask(update_task_event e)
    {
        if(StaticData.task_status[e.task - 1, e.step])
        {
            Debug.Log("Task "+e.task + "Step" + e.step + "has duplicated true");
        }else
        {
            StaticData.task_status[e.task - 1, e.step] = true;
            bool flag = true;
            for(int i = 1; i <= StaticData.step_number; i++)
            {
                flag = flag && StaticData.task_status[e.task - 1, i];
            }
            EventBus.Publish<ToastRequest>(new ToastRequest(TaskLists[e.task - 1, e.step], 3.0f, false, true, false));
            if (flag)
            {
                StaticData.task_status[e.task - 1, 0] = true;
                StaticData.story_progress++;
                EventBus.Publish<ToastRequest>(new ToastRequest("任务完成...新故事碎片已解锁", 3.0f, false, false, true));
            }
            RefreshPanel();
        }
    }

    void _TodoTaskOnclick()
    {
        //UseClose original Panel
        // Active the Todo Panel
        //Load Todo Panel;
        if(status == StatusOfPanel.TodoTask)
        {
            return;
        }
        CloseOriginalPanel();
        status = StatusOfPanel.TodoTask;
        TodoTaskPanel.SetActive(true);
        LoadTodoTask();
    }

    void _FinishButtonOnclick()
    {
        //UseClose original Panel
        // Active the FinishButtonOnclick
        //Load FinishedPanel
        if(status == StatusOfPanel.FinishedTask)
        {
            return;
        }
        CloseOriginalPanel();
        status = StatusOfPanel.FinishedTask;
        FinishedTaskPanel.SetActive(true);
        LoadFinishedTask();

    }

    void _CloseButtonOnclick()
    {
        //change status to 0;
        //Set the status of PanelController to false
        CloseOriginalPanel();
        status = StatusOfPanel.close;
        StaticData.has_open_panel = false;
    }

    void _OpenButtonOnclick()
    {
        //change status to 1
        //activate the todo panel;
        //refresh todo panel;
        //set the status of PanelController to true;
        StaticData.has_open_panel = true;
        status = StatusOfPanel.TodoTask;
        TodoTaskPanel.SetActive(true);
        RefreshPanel();
    }

    void _handleGetFish(get_fish_event e)
    {
        string fish_name = FishList.GetFishWithFishID(e.fishHookedID).getFishName();
        if(is_first_fish)
        {
            EventBus.Publish<update_task_event>(new update_task_event(1,1));
            is_first_fish = false;
        }
        if(is_first_SSR)
        {
            if(FishList.GetFishWithFishID(e.fishHookedID).getRarity() == FishRarity.SSR)
            {
                EventBus.Publish<update_task_event>(new update_task_event(2,3));
                is_first_SSR = false;
            }
        }
        if(is_first_killer_whale)
        {
            if(fish_name == "Orca")
            {
                EventBus.Publish<update_task_event>(new update_task_event(4,2));
                is_first_killer_whale = false;
                return;
            }
        }
        if(is_first_2_bombat < 2)
        {
            if(fish_name == "Bombat")
            {   
                is_first_2_bombat++;
                if(is_first_2_bombat == 2)
                {
                    EventBus.Publish<update_task_event>(new update_task_event(2,2));
                }
                return;
            }
        }
        if(is_first_yang)
        {
            if(fish_name == "Yang")
            {
                EventBus.Publish<update_task_event>(new update_task_event(4,1));
                is_first_yang = false;
                return;
            }
        }
        if(is_first_crocodile)
        {
            if(fish_name == "Crocodile")
            {
                EventBus.Publish<update_task_event>(new update_task_event(4,3));
                is_first_crocodile = false;
                return;
            }
        }
    }
    
    void _handleCureFish(alter_health_event e)
    {
        if(e.alterHealth >= 10)
        {
            EventBus.Publish<update_task_event>(new update_task_event(3,3));
        }
    }
    void OnDestroy()
    {
        EventBus.Unsubscribe<update_task_event>(update_task_sub);
        EventBus.Unsubscribe<get_fish_event>(get_fish_sub);
        EventBus.Unsubscribe<alter_health_event>(alter_health_sub);

    }
}
