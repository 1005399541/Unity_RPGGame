using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
   //进入选角色的场景或读取数据并进入游戏场景




    /// <summary>
    /// 进入选择角色的场景,DataFromSave表示来自保存的数据
    /// </summary>
   public void OnNewGame()
    {

        PlayerPrefs.SetInt("DataFromSave", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }


    /// <summary>
    /// 加载存储的数据并直接进入游戏场景，DataFromSave表示来自保存的数据
    /// </summary>
   public void OnLoadGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
