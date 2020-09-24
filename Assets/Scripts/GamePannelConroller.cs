using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePannelConroller : MonoBehaviour
{
    private string createAccountStrings = "http://kreasaard.atwebpages.com/MultiGames/createaccount.php";
    public GameObject GamePannel;
    public GameObject LoadingPannel;
    public Image LoadingBar;
    public Text LoadingGameName;

    void Awake()
    {
        saveload.Load();
    }

    void Start()
    {
        
        if (saveload.isGamePannelOn>0)
        {
            GamePannel.SetActive(true);
            saveload.isGamePannelOn = 0;
            saveload.Save();
        }

        GamePannel.SetActive(true);
        checkandcreateaccount();
    }

    public void CorsairGameButtonPressed()
    {
        StartCoroutine(WaitForCorsairFlashDelay("Corsair","Corsairgame"));
    }

    public void TapTapGameButtonPressed()
    {
        StartCoroutine(WaitForCorsairFlashDelay("Tap Tap","taptap"));
    }

    public void MazeRunnerGameButtonPressed()
    {
        StartCoroutine(WaitForCorsairFlashDelay("Maze Runner", "Maze"));
    }

    public void ColorCollectorGameButtonPressed()
    {
        StartCoroutine(WaitForCorsairFlashDelay("Color Collector", "colorcollector"));
    }

    IEnumerator WaitForCorsairFlashDelay(string gamename,string sceneName)
    {
        LoadingPannel.SetActive(true);
        LoadingGameName.text = gamename;
        AsyncOperation game = SceneManager.LoadSceneAsync(sceneName);

        while (game.progress < 1)
        {
            LoadingBar.rectTransform.localScale = new Vector3(game.progress, 1, 1);
            yield return new WaitForEndOfFrame();
        }
    }

    void checkandcreateaccount()
    {
        if (saveload.accountID == " ")
        {
            //create account
            int random = Random.Range(11111, 9999999);
            StartCoroutine(CreatePlayerAccount(random.ToString()));
        }
    }

    IEnumerator CreatePlayerAccount(string name)
    {
        WWWForm form1 = new WWWForm();
        form1.AddField("name",name);
        
        WWW www = new WWW(createAccountStrings, form1);
        yield return www;

        if (www.text.Contains("Created"))
        {
            string id = GetDataValue(www.text, "Created:");
            saveload.accountID = id;
            saveload.Save();
            print("sabe");
        }
        else if (www.text.Contains("error"))
        {
           // saveload.accountID = " ";
            //checkandcreateaccount();
        }
        else
        {

        }
        print(www.text);
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains(":"))
            value = value.Remove(value.IndexOf(":"));
        return value;
    }
}
