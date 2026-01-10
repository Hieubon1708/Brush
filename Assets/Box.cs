using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameController;

public class Box : MonoBehaviour, IPointerClickHandler
{
    public BoxColor boxColor;

    Image image;

    public bool isChecked;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.color = GameController.instance.GetColor(boxColor);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Tool.instance.ResetChecked();

        Check(boxColor);
    }

    public void Check(BoxColor boxColor)
    {
        if (this.boxColor != boxColor || isChecked || this.boxColor == BoxColor.None) return;

        isChecked = true;

        image.color = GameController.instance.GetColor(Tool.instance.targetColor);

        Tool.instance.CheckBoxAround(this, boxColor);

        this.boxColor = Tool.instance.targetColor;
    }

    public void ToolCheck(BoxColor boxColor, ref int count)
    {
        if (this.boxColor != boxColor || isChecked || this.boxColor == BoxColor.None) return;

        count++;

        isChecked = true;

        image.color = GameController.instance.GetColor(Tool.instance.targetColor);

        Tool.instance.CheckBoxAround(this, boxColor);

        this.boxColor = Tool.instance.targetColor;
    }
}
