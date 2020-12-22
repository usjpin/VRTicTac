using UnityEngine;
using System.Collections.Generic;

public class OpponentAI
{
    public bool CheckWin(string[] virtboard, string turn)
    {
        if ((virtboard[0] == turn && virtboard[1] == turn && virtboard[2] == turn) ||
            (virtboard[3] == turn && virtboard[4] == turn && virtboard[5] == turn) ||
            (virtboard[6] == turn && virtboard[7] == turn && virtboard[8] == turn) ||
            (virtboard[0] == turn && virtboard[3] == turn && virtboard[6] == turn) ||
            (virtboard[1] == turn && virtboard[4] == turn && virtboard[7] == turn) ||
            (virtboard[2] == turn && virtboard[5] == turn && virtboard[8] == turn) ||
            (virtboard[0] == turn && virtboard[4] == turn && virtboard[8] == turn) ||
            (virtboard[2] == turn && virtboard[4] == turn && virtboard[6] == turn))
        {
            return true;
        }
        return false;
    }

    public float Minimax(string[] virtboard, bool maximize, int depth)
    {
        if (CheckWin(virtboard, "O")) return 1;
        if (CheckWin(virtboard, "X")) return -1;
        if (depth == 9) return 0;

        float best = maximize ? -Mathf.Infinity : Mathf.Infinity;
        for (int i = 0; i < virtboard.Length; i++)
        {
            if (virtboard[i] == "")
            {
                virtboard[i] = maximize ? "O" : "X";
                float score = Minimax(virtboard, !maximize, depth + 1);
                virtboard[i] = "";
                best = maximize ? Mathf.Max(score, best) : Mathf.Min(score, best);
            }
        }
        return best;
    }

    public int GetMinimaxMove(GameObject[] board)
    {
        string[] virtboard = new string[9];
        int filled = 0;
        for (int i = 0; i < board.Length; i++)
        {
            virtboard[i] = board[i].GetComponent<TextMesh>().text;
            if (virtboard[i] != "") filled++;
        }

        float best = -Mathf.Infinity;
        int move = -1;

        for (int i = 0; i < virtboard.Length; i++) {
            if (virtboard[i] == "")
            {
                virtboard[i] = "O";
                float score = Minimax(virtboard, false, filled+1);
                if (best < score)
                {
                    best = score;
                    move = i;
                }
                virtboard[i] = "";
            }   
        }
        return move;
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
