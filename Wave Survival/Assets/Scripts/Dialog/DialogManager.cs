using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public List<DialogOptions> dialogs = new List<DialogOptions>();
    public TextLocalizerUI dialogText;
    public GameObject dialogCanvas;
    public GameObject AnswerPanel;

    public List<TextLocalizerUI> answerText = new List<TextLocalizerUI>();



    private int dialogIndex = 0;

    private void Start()
    {
        /*dialogs.Sort(delegate (DialogOptions a, DialogOptions b)
        {
            return (a.waveIndexToStart).CompareTo(b.waveIndexToStart);
        });
        var temp = new DialogOptions();
        temp.InitLast();
        dialogs.Add(temp);
        if (dialogs[dialogIndex].state == DialogOptions.currentState.Start)
        {
            Toggle(true);
            dialogText.key = dialogs[dialogIndex].key;
            dialogText.Refresh();
        }

        nextDialogState = dialogs[dialogIndex + 1].state;*/

        Toggle();
        dialogText.isQuest = dialogs[dialogIndex].isQuest;
        dialogText.key = dialogs[dialogIndex].key;
        if (dialogs[dialogIndex].isQuest)
        {
            AnswerPanel.SetActive(true);
            for (int i = 0; i < dialogs[dialogIndex].answersKeys.Count; i++)
            {
                answerText[i].key = dialogs[dialogIndex].answersKeys[i];
                answerText[i].isQuest = true;
                answerText[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(true);
            }
            for (int i = dialogs[dialogIndex].answersKeys.Count; i < answerText.Count; i++)
            {
                answerText[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(false);
            }
        }
        else
        {
            if (dialogIndex != 0)
            {
                for (int i = 0; i < dialogs[dialogIndex - 1].answersKeys.Count; i++)
                {
                    answerText[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(false);
                }

            }
            AnswerPanel.SetActive(false);
        }
        dialogText.Refresh(dialogs[dialogIndex].isQuest);
    }

    public void NextDialog()
    {
        if (dialogIndex == dialogs.Count - 1)
        {
            Toggle();
            return;
        }
        dialogIndex++;
        dialogText.key = dialogs[dialogIndex].key;
        if (dialogs[dialogIndex].isQuest)
        {
            AnswerPanel.SetActive(true);
            for (int i = 0; i < dialogs[dialogIndex].answersKeys.Count; i++)
            {
                answerText[i].key = dialogs[dialogIndex].answersKeys[i];
                answerText[i].isQuest = true;
                answerText[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(true);
            }
            for (int i = dialogs[dialogIndex].answersKeys.Count; i < answerText.Count; i++)
            {
                answerText[i].gameObject.SetActive(false);
            }
        }
        else
        {
            /*for (int i = 0; i < dialogs[dialogIndex - 1].answersKeys.Count; i++)
            {
                answerText[i].gameObject.GetComponentInParent<Button>().gameObject.SetActive(false);
            }*/
            AnswerPanel.SetActive(false);
        }
        dialogText.Refresh(dialogs[dialogIndex].isQuest);
        Debug.Log("next dialog");
    }

    private void Update()
    {
    }

    public void OnWaveEntierlySpawned()
    {
        dialogIndex++;
        dialogText.key = dialogs[dialogIndex].key;
        dialogText.Refresh(dialogs[dialogIndex].isQuest);
        Toggle();
    }
    public void Toggle()
    {
        dialogCanvas.SetActive(!dialogCanvas.activeSelf);
    }


    #region questAnswer

    public void Button0Clicked()
    {
        QuestAnswer(dialogs[dialogIndex].key, 0);
    }
    public void Button1Clicked()
    {
        QuestAnswer(dialogs[dialogIndex].key, 1);
    }
    public void Button2Clicked()
    {
        QuestAnswer(dialogs[dialogIndex].key, 2);
    }
    public void QuestAnswer(string key, int answerIndex)
    {
        if (key == "Q1")
        {
            AnswerQ1(answerIndex);
        }

        NextDialog();
    }

    private void AnswerQ1(int answerIndex)
    {
        if (answerIndex == 0)
        {
            GameManager.instance.ResourcesGain(GameManager.currency.Wood, 10);
        }
        else if (answerIndex == 1)
        {
            GameManager.instance.RemoveCurrency(GameManager.currency.Wood, 10);
        }
    }
    #endregion
}

[System.Serializable]
public class DialogOptions
{
    public string key;
    public bool isQuest;

    [Header("Optional")]
    public List<string> answersKeys = new List<string>();
}