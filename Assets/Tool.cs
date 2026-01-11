using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public static Tool instance;

    public GameController.BoxColor targetColor;
    public GameController.BoxColor startColor;

    public int row;
    public int col;

    Box[][] boxes;

    private void Awake()
    {
        instance = this;

        Box[] bx = GetComponentsInChildren<Box>();

        boxes = new Box[row][];

        int count = 0;

        for (int i = 0; i < boxes.Length; i++)
        {
            Box[] b = new Box[col];

            for (int j = 0; j < b.Length; j++)
            {
                b[j] = bx[count];
                count++;
            }

            boxes[i] = b;
        }

        /*for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = 0; j < boxes[i].Length; j++)
            {
                Debug.Log(boxes[i][j].boxColor);
            }
        }*/
    }

    bool isFirstColor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isFirstColor)
            {
                isFirstColor = true;

                GameController.BoxColor maxColor = GameController.BoxColor.None;

                string name = string.Empty;

                Box box = null;

                for (int i = 0; i < boxes.Length; i++)
                {
                    for (int j = 0; j < boxes[i].Length; j++)
                    {
                        if (boxes[i][j].boxColor == startColor)
                        {
                            int maxCount = 0;

                            GameController.BoxColor[] bc = ((GameController.BoxColor[])Enum.GetValues(typeof(GameController.BoxColor)));

                            for (int k = 0; k < bc.Length; k++)
                            {
                                int count = 0;

                                ResetChecked();

                                if (bc[k] != GameController.BoxColor.None && bc[k] != boxes[i][j].boxColor)
                                {
                                    boxes[i][j].ToolCheck(boxes[i][j].boxColor, bc[k], ref count);
                                    //Debug.LogError("Color = " + bc[k] + " Count = " + count);
                                }

                                if (count > maxCount)
                                {
                                    maxCount = count;
                                    maxColor = bc[k];
                                    name = boxes[i][j].name;
                                    box = boxes[i][j];
                                }
                            }

                            Debug.Log("Max = " + maxColor.ToString() + " " + name + " Count = " + maxCount);
                        }
                    }
                }

                ResetChecked();

                targetColor = maxColor;

                box.Check(box.boxColor);
            }
            else
            {
                GameController.BoxColor maxColor = GameController.BoxColor.None;
                GameController.BoxColor maxFinalColor = GameController.BoxColor.None;

                int maxFinalCount = 0;

                Box box = null;
                Box finalbox = null;

                for (int i = 0; i < boxes.Length; i++)
                {
                    for (int j = 0; j < boxes[i].Length; j++)
                    {
                        int maxCount = 0;

                        GameController.BoxColor[] bc = ((GameController.BoxColor[])Enum.GetValues(typeof(GameController.BoxColor)));

                        for (int k = 0; k < bc.Length; k++)
                        {
                            int count = 0;

                            ResetChecked();

                            if (bc[k] != GameController.BoxColor.None && bc[k] != boxes[i][j].boxColor)
                            {
                                boxes[i][j].ToolCheck(boxes[i][j].boxColor, bc[k], ref count);
                                //Debug.LogError("Color = " + bc[k] + " Count = " + count);
                            }

                            if (count > maxCount)
                            {
                                maxCount = count;
                                maxColor = bc[k];
                                box = boxes[i][j];
                            }
                        }

                        if (maxCount > maxFinalCount)
                        {
                            maxFinalCount = maxCount;
                            maxFinalColor = maxColor;
                            finalbox = box;
                        }
                    }
                }

                Debug.Log("Max = " + maxFinalColor.ToString() + " " + finalbox.name + " Count = " + maxFinalCount);

                ResetChecked();

                targetColor = maxFinalColor;

                finalbox.Check(finalbox.boxColor);
            }
        }
    }

    public void ToolCheckBoxAround(Box box, GameController.BoxColor originColor, GameController.BoxColor boxColor, ref int count)
    {
        int row;
        int col;

        GetRowCol(box, out row, out col);

        //left
        if (col - 1 >= 0)
        {
            boxes[row][col - 1].ToolCheck(originColor, boxColor, ref count);
        }
        //right
        if (col + 1 < this.col)
        {
            boxes[row][col + 1].ToolCheck(originColor, boxColor, ref count);
        }
        //top
        if (row - 1 >= 0)
        {
            boxes[row - 1][col].ToolCheck(originColor, boxColor, ref count);
        }
        //bottom
        if (row + 1 < this.row)
        {
            boxes[row + 1][col].ToolCheck(originColor, boxColor, ref count);
        }
    }

    public void ResetChecked()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = 0; j < boxes[i].Length; j++)
            {
                boxes[i][j].isChecked = false;
            }
        }
    }

    void GetRowCol(Box box, out int row, out int col)
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            for (int j = 0; j < boxes[i].Length; j++)
            {
                if (boxes[i][j] == box)
                {
                    row = i;
                    col = j;

                    return;
                }
            }
        }

        row = -1; col = -1;
    }

    public void CheckBoxAround(Box box, GameController.BoxColor boxColor)
    {
        int row;
        int col;

        GetRowCol(box, out row, out col);
        /*Debug.Log(row);
        Debug.Log(col);*/

        if (row == -1 || col == -1)
        {
            Debug.LogError("-1 " + row + " - " + col);

            return;
        }

        //left
        if (col - 1 >= 0)
        {
            boxes[row][col - 1].Check(boxColor);
        }
        //right
        if (col + 1 < this.col)
        {
            boxes[row][col + 1].Check(boxColor);
        }
        //top
        if (row - 1 >= 0)
        {
            boxes[row - 1][col].Check(boxColor);
        }
        //bottom
        if (row + 1 < this.row)
        {
            boxes[row + 1][col].Check(boxColor);
        }
    }
}
