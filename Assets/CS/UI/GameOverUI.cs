using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("���ӿ��� ����Ÿ��Ʋ")]
    public TextMeshProUGUI subTitleTMP;
    public bool isNoHP;

    [Header("���� �ؽ�Ʈ")]
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI timeTMP;
    public TextMeshProUGUI destroyTMP;

    [Header("�̴ϼ� �Է�")]
    public TMP_InputField inputField;
    string inputString = null;

    void Start()
    {
        TMPLoad();

        inputString = inputField.GetComponent<TMP_InputField>().text;
    }

    void Update()
    {
        
    }

    public void TMPLoad()
    {
        if(isNoHP)
        {
            subTitleTMP.text = "===< No HP >===";
        }
        else
        {
            subTitleTMP.text = "===< No Oil >===";
        }

        scoreTMP.text = CommaText(GameManager.GM.score);
        timeTMP.text = CommaText((uint)GameManager.GM.runTime);
        destroyTMP.text = CommaText(GameManager.GM.destroyCount);
    }

    public void InputName()
    {
        inputString = inputField.text.Trim();
        Debug.Log(inputString);
    }


    public void GoMain()
    {
        MoveScene("MainScene");
    }
    public void Retry()
    {
        MoveScene("GameScene");
    }

    void MoveScene(string moveSceneName)
    {
        GameManager.GM.RankArrange(inputString);
        SceneManager.LoadScene(moveSceneName);
    }

    string CommaText(uint value)
    {
        return string.Format("{0:0000000000}", value);
    }
}
