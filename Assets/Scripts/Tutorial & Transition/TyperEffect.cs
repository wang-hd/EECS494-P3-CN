using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TyperEffect : MonoBehaviour
{
    float typePerSecond = 0.1f;
    float timer = 0;
    Text TextInput;
    string message;
    int currentPos = 0;
    bool is_typing = true;

    // Start is called before the first frame update
    void Start()
    {
        TextInput = GetComponent<Text>();
        message = StaticData.message;
        TextInput.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >=typePerSecond && is_typing)
        {
            if(currentPos >= message.Length){
                is_typing = false;
                StartCoroutine(LoadNewScene());
                return;
            }
            timer = 0;
            currentPos++;
            TextInput.text = message.Substring(0,currentPos);
        }
    }
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(StaticData.nextScene);
    }
}
