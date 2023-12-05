using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Experimental.GraphView;
//using System.Runtime.InteropServices.WindowsRuntime;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    int totalStage = 3;
    int[] itemCount = { 5, 5, 5 };

    static int stageNum;
    static int stageScore;
    static int totalScore;
    static int getItemCount;
    

    static TextMeshProUGUI stageNumber;
    static TextMeshProUGUI stageScoreText;
    static TextMeshProUGUI totalScoreText;
    static TextMeshProUGUI gameoverText;

    static bool isGameover = false;

    public bool GameOver {
        get => isGameover;
        set => isGameover = value;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        stageNumber = GameObject.FindGameObjectWithTag("StageNumber").GetComponent<TextMeshProUGUI>();
        stageScoreText = GameObject.FindGameObjectWithTag("StageScore").GetComponent<TextMeshProUGUI>();
        totalScoreText = GameObject.FindGameObjectWithTag("TotalScore").GetComponent<TextMeshProUGUI>();

        stageScore = 0;
        getItemCount = 0;
    }

    void Start() {
        stageNum = 1;
        totalScore = 0;
    }

    void FixedUpdate() {
        if (stageNum > totalStage) {
            stageNumber.text = "STAGE : [END]";
        }
        else {
            stageNumber.text = "STAGE : " + stageNum.ToString();
        }
        stageScoreText.text = "SCORE : " + stageScore.ToString();
        totalScoreText.text = "TOTAL : " + totalScore.ToString();
    }

    void Update() {
        if (isGameover) {
            if (Input.GetKeyDown(KeyCode.R)) {
                stageNum = 1;
                //stageScore = 0;
                totalScore = 0;

                Destroy(GameObject.Find("DontDistroyOnLoad")); ///////////////

                GameOver = false;
                gameoverText = GameObject.FindGameObjectWithTag("Gameover").GetComponent<TextMeshProUGUI>();
                gameoverText.gameObject.SetActive(false);
                
                SceneManager.LoadScene(0);
            }
        }
    }

    public void IncreaseScore() {
        getItemCount++;
        stageScore++;
        totalScore++;
    }

    public void FinishPoint() {
        if (getItemCount >= itemCount[stageNum - 1]) {
            stageNum++;
        }
        else {
            totalScore -= stageScore;
        }

        string nextScene;

        if (stageNum > totalStage) {
            nextScene = "Last";
        }
        else {
            nextScene = "Scene_" + stageNum.ToString();
        }
        
        StartCoroutine(NextScene(nextScene));
    }

    IEnumerator NextScene(string next) {
        yield return new WaitForSeconds(1f);

        stageScore = 0;
        getItemCount = 0;

        if (stageNum > totalStage) {
            GameObject.Find("Canvas").transform.Find("Gameover").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Gameover").GetComponent<TextMeshProUGUI>().text = "너 게임 쪼까 할 줄 안다이~~!!";
            GameOver = true;
        }
        
        SceneManager.LoadScene(next);
    }
}
