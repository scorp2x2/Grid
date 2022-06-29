using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSymbols : MonoBehaviour
{
    public GameObject prefubSymbol;

    GridLayoutGroup gridLayoutGroup;
    RectTransform rectTransform;

    public delegate void EndAnimShake();
    public event EndAnimShake OnEndAnimShake;

    void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Generate(int countWidth, int countHeight)
    {
        var widthChar = (rectTransform.rect.size.x - gridLayoutGroup.padding.left - gridLayoutGroup.padding.right - gridLayoutGroup.spacing.x * (countWidth - 1)) / countWidth;
        var heightChar = (rectTransform.rect.size.y - gridLayoutGroup.padding.top - gridLayoutGroup.padding.bottom - gridLayoutGroup.spacing.y * (countHeight - 1)) / countHeight;
        gridLayoutGroup.cellSize = new Vector2(widthChar, heightChar);

        int count = countHeight * countWidth;
        for (int i = 0; i < count || i < transform.childCount; i++)
        {
            if (i < count)
            {
                Symbol symbol = null;

                if (i < transform.childCount)
                    symbol = transform.GetChild(i).GetComponent<Symbol>();
                else
                {
                    symbol = Instantiate(prefubSymbol, transform).GetComponent<Symbol>();
                    symbol.OnEndAnimShake += Symbol_OnEndAnimShake;
                }

                symbol.SetRandomSymbol();
            }
            else
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    private void Symbol_OnEndAnimShake()
    {
        countShake--;
        if (countShake == 0)
            OnEndAnimShake?.Invoke();
    }

    public int countShake;

    public void Shake()
    {
        var targets = new List<int>();
        var symbols = new List<Symbol>();

        for (int i = 0; i < transform.childCount; i++)
        {
            symbols.Add(transform.GetChild(i).GetComponent<Symbol>());
            targets.Add(i);
        }

        countShake = symbols.Count;

        foreach (var item in symbols)
        {
            var index = Random.Range(0, targets.Count);
            item.Move(symbols[targets[index]]);
            targets.RemoveAt(index);
        }
    }
}
