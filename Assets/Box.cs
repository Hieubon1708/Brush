using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static GameController;

public class Box : MonoBehaviour, IPointerClickHandler
{
    public GameController.BoxColor boxColor;

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
        Check(boxColor);
    }

    public void Check(GameController.BoxColor boxColor)
    {
        if (this.boxColor != boxColor || isChecked) return;
        Debug.Log(name);

        isChecked = true;

        image.color = GameController.instance.GetColor(Tool.instance.targetColor);

        Tool.instance.CheckBoxAround(this, boxColor);

        boxColor = Tool.instance.targetColor;
    }
}
