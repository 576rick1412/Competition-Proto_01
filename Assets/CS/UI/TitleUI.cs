using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleUI : MonoBehaviour
{
    public GameObject rnakUI;
    public GameObject[] rankWindow;
    RankUI[] rank = new RankUI[10];
    bool isRankPopup;   // 랭크 창이 띄워져 있는지 여부

    void Start()
    {
        for (int i = 0; i < rankWindow.Length; i++)
        {
            rank[i].setName = rankWindow[i].transform.Find("NameTMP").GetComponent<TextMeshProUGUI>();
            rank[i].setScore = rankWindow[i].transform.Find("ScoreTMP").GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        
    }

    public void GameStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void RankPopup()
    {
        isRankPopup = !isRankPopup;
        
        for (int i = 0; i < rankWindow.Length; i++)
        {
            rank[i].setName.text = GameManager.GM.data.ranks[i].setName;
            rank[i].setScore.text = GameManager.GM.TenZeroCommaText(GameManager.GM.data.ranks[i].setScore);
        }

        rnakUI.SetActive(isRankPopup);
    }

    public void RankPopupClose()
    {
        isRankPopup = !isRankPopup;

        rnakUI.GetComponent<Animator>().SetTrigger("popup");

        Invoke("LateClose", 1f);
    }

    void LateClose()
    {
        rnakUI.SetActive(isRankPopup);
    }

    [System.Serializable]
    public struct RankUI
    {
        public TextMeshProUGUI setName;
        public TextMeshProUGUI setScore;
    }
}
