using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public static class AutomaticDialogTextAutoSize
{
    public static void AutosizeDialog(TMP_Text dialogText, string[] sentences)
    {

        if (dialogText != null && sentences != null & sentences.Length > 0)
        {
            string current = dialogText.text;
            float minSize = float.MaxValue;
            dialogText.enableAutoSizing = true;
            for (int i = 0; i < sentences.Length; i++)
            {
                dialogText.text = sentences[i];
                dialogText.ForceMeshUpdate();
                if (minSize > dialogText.fontSize)
                {
                    minSize = dialogText.fontSize;
                }
            }
            dialogText.enableAutoSizing = false;
            dialogText.fontSize = minSize;
            dialogText.text = current;
        }
    }

    public static void AutoSizeIndex(TMP_Text indexText, string maxNum)
    {
        if (indexText != null)
        {
            string current = indexText.text;
            indexText.enableAutoSizing = true;
            indexText.text = maxNum;
            indexText.ForceMeshUpdate();
            indexText.enableAutoSizing = false;
            indexText.text = current;

        }
    }
}
