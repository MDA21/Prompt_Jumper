using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChange : MonoBehaviour
{
    [SerializeField] private TMP_Text tmptext;

    [SerializeField] private string str;
    IEnumerator TypeText(TMP_Text tmptext, string str, float interval)
    {
        int i = 0;
        while (i < str.Length)
        {
            tmptext.text += str[i];
            i++;
            yield return new WaitForSeconds(interval);
        }
        yield return new WaitForSeconds(interval);  
    }

    private void Start()
    {
        tmptext.text = "";

        StartCoroutine(TypeText(tmptext, str, 0.1f));
    }


}
