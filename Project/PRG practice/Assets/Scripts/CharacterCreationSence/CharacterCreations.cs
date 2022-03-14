using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreations : MonoBehaviour
{
    //角色在场景中生成与切换


    public GameObject[] CharacterPrefabs;//角色预制体的储存
    public UIInput NameInput;
    private GameObject[] CharacterGameObjects;//角色的实例化
    private int CharacterNumber;//角色的数量
    private int CharacterIndex;//正在显示的角色的标号

    void Start()
    {
        CharacterIndex = 0;
        CharacterNumber = CharacterPrefabs.Length;
        CharacterGameObjects = new GameObject[CharacterNumber];
        for (int i = 0; i < CharacterNumber; i++)
        {
            
            CharacterGameObjects[i]=Instantiate(CharacterPrefabs[i], transform.position,transform.rotation);
        }
        updateCharacter();
    }


    /// <summary>
    /// 更新角色显示
    /// </summary>
  void updateCharacter()
    {
        CharacterGameObjects[CharacterIndex].SetActive(true);
        for (int i=0;i<CharacterNumber;i++)
        {
            if (i != CharacterIndex)
            {
                CharacterGameObjects[i].SetActive(false);
            }
        }
    }


    /// <summary>
    /// 下一个角色展示
    /// </summary>
   public void NextCharacter()
    {
        CharacterIndex++;
        CharacterIndex %= CharacterNumber;
        updateCharacter();
    }


    /// <summary>
    /// 上一个角色展示
    /// </summary>
   public void PrevCharacter()
    {
        CharacterIndex--;
        if (CharacterIndex < 0)
        {
            CharacterIndex = CharacterNumber - 1;
            
        }
        updateCharacter();
    }





    /// <summary>
    /// Ok事件的点击，储存人物角色名字和角色，并加载下一个场景
    /// </summary>
    public void OnClickOk()
    {
        PlayerPrefs.SetInt("SelectCharacterIndex",CharacterIndex);// SelectCharacterIndex保存所选角色
        PlayerPrefs.SetString("SelecterCharacterName",NameInput.value); //SelecterCharacterName保存玩家输入的角色名字
        Debug.Log(PlayerPrefs.GetString("SelecterCharacterName"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
}
