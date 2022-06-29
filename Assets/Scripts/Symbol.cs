using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public TextMeshProUGUI symbolTMP;
    public float timeAnimation = 2;
    public float delta = .1f;

    public event PanelSymbols.EndAnimShake OnEndAnimShake;

    public void SetSymbol(TextMeshProUGUI newText)
    {
        newText.transform.SetParent(transform);
        newText.transform.localPosition = new Vector3();
        symbolTMP = newText;
    }

    public void SetRandomSymbol()
    {
        symbolTMP.text = ((char)(Random.Range(0, 26) + 65)).ToString();
    }

    public void Move(Symbol target)
    {
        StartCoroutine(Moving(target));
    }

    IEnumerator Moving(Symbol targetSymbol)
    {
        var currentSymbol = symbolTMP;
        var startPosition = symbolTMP.transform.position;
        var targetPosition = targetSymbol.transform.position;
        float time = 0;
        while (true)
        {
            currentSymbol.transform.position = Vector3.Lerp(startPosition, targetPosition, time / timeAnimation);
            time += Time.deltaTime;

            if (Vector3.Distance(currentSymbol.transform.position, targetPosition) < delta)
            {
                targetSymbol.SetSymbol(currentSymbol);
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        OnEndAnimShake?.Invoke();
    }
}
