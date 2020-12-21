using UnityEngine;
using System.Collections.Generic;

public class OpponentAI
{
    public int GetMinimaxMove(GameObject[] board)
    {
        return 0;
    }

    public int GetRandomMove(GameObject[] board)
    {
        List<int> options = new List<int>();
        Debug.Log("GetRand");
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i].GetComponentInParent<ButtonComponent>().IsActive())
            {
                options.Add(i);
            }
        }
        Debug.Log("Completed Seting");
        int choice = Random.Range(0, options.Count);
        Debug.Log("Returning Rand" + options[choice].ToString());
        return options[choice];
    }
}
