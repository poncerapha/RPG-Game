﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;
    public string[] dialogLines;
    public int currentLine;
    private bool justStarted;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);
                        GameManager.instance.dialogActive = false;
                    }
                    else
                    {
                        CheckName();
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }    
            }
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson)
    {
        
        dialogLines = newLines;
        currentLine = 0;
        CheckName();
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
        justStarted = true;
        nameBox.SetActive(isPerson);
        GameManager.instance.dialogActive = true;
    }

    public void CheckName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-","");
            currentLine++;
        }
    }
}
