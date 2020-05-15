using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public List<DialogOptions> dialogs = new List<DialogOptions>();
    public TextLocalizerUI dialogText;
    public GameObject dialogCanvas;



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
        dialogText.key = dialogs[dialogIndex].key;
        dialogText.Refresh();
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
        dialogText.Refresh();
        Debug.Log("next dialog");
    }

    private void Update()
    {
    }

    public void OnWaveEntierlySpawned()
    {
        dialogIndex++;
        dialogText.key = dialogs[dialogIndex].key;
        dialogText.Refresh();
        Toggle();
    }
    public void Toggle()
    {
        dialogCanvas.SetActive(!dialogCanvas.activeSelf);
    }
}

[System.Serializable]
public class DialogOptions
{
    public string key;
}