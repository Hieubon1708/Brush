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
            ResetChecked();

            if (!isFirstColor)
            {
                isFirstColor = true;

                Vector2Int max = Vector2Int.zero;

                int maxCount = 0;

                for (int i = 0; i < boxes.Length; i++)
                {
                    for (int j = 0; j < boxes[i].Length; j++)
                    {
                        if (boxes[i][j].boxColor == startColor)
                        {
                            int count = 0;

                            ToolCheckBoxAround(boxes[i][j], boxes[i][j].boxColor, ref count);
                        }
                    }
                }
            }
            else
            {

            }
        }
    }

    public void ToolCheckBoxAround(Box box, GameController.BoxColor boxColor, ref int count)
    {
        int row;
        int col;

        GetRowCol(box, out row, out col);

        //left
        if (col - 1 >= 0)
        {
            boxes[row][col - 1].ToolCheck(boxColor, ref count);
        }
        //right
        if (col + 1 < this.col)
        {
            boxes[row][col + 1].ToolCheck(boxColor, ref count);
        }
        //top
        if (row - 1 >= 0)
        {
            boxes[row - 1][col].ToolCheck(boxColor, ref count);
        }
        //bottom
        if (row + 1 < this.row)
        {
            boxes[row + 1][col].ToolCheck(boxColor, ref count);
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
