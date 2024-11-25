using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public void SceneToSave()
    {
        DataManager.instance.previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("SAVE_SCENE");
    }
    public void SceneToNext()
    {
        SceneManager.LoadScene("AISLE2");
    }
}
