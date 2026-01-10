using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public static Tool instance;

    public GameController.BoxColor targetColor;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
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
