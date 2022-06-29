using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PanelSymbols panelSymbols;

    public TMP_InputField textWidth;
    public TMP_InputField textHeight;

    public Button buttonGenerate;
    public Button buttonShake;

    void Start()
    {
        panelSymbols.OnEndAnimShake += PanelSymbols_OnEndAnimShake;

        ButtonGenerate();
    }

    private void PanelSymbols_OnEndAnimShake()
    {
        buttonGenerate.interactable = true;
        buttonShake.interactable = true;
    }

    public void ButtonGenerate()
    {
        if (!int.TryParse(textWidth.text, out int countWidth))
            return;
        if (!int.TryParse(textHeight.text, out int countHeight))
            return;

        panelSymbols.Generate(countWidth, countHeight);
    }

    public void ButtonShake()
    {
        buttonGenerate.interactable = false;
        buttonShake.interactable = false;

        panelSymbols.Shake();
    }
}
