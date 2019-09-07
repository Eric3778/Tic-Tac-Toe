using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_table : MonoBehaviour
{
    private bool game_start = true;
    private int turn = 1; //标记当前该哪边下，1表示X，2表示O;
    private int[,] board = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; //记录当前棋盘信息，0为未下，1为X，2为O;
    private int choose = 1;
    public GUISkin MY_GUI;
    public GameObject ball1, ball2, ball3, ball4;
    //private const int button_width = 20;
    //private const int button_length = 30;

    private int game_situation() //返回当前棋盘状态，0为未结束，1为X胜利，2为O胜利，3为平局
    {
        for (int i = 0; i < 3; i++) //检查横纵方向
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0) return board[i, 0];
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != 0) return board[0, i];
        }
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0) return board[0, 0]; //检查两条对角线
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[1, 1] != 0) return board[1, 1];
        int full_count = 0;  //记录下子位的个数
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] != 0) full_count++;
        if (full_count == 9) return 3; //如果下满了就说明平局
        else return 0; //如果没下满说明没有结束
    }

    private void restart()  //重开游戏，刷新棋盘
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = 0;
        turn = 1;
    }

    private void draw_board() //根据当前棋盘记录绘制棋盘
    {
        int button_length = Screen.width / 8;
        int button_width = Screen.height / 8;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == 1)
                    GUI.Button(new Rect(Screen.width / 2 + button_length * (2 * j - 3) / 2, Screen.height / 8 + 50 + button_width * i, button_length, button_width), "X");
                else if (board[i, j] == 2)
                    GUI.Button(new Rect(Screen.width / 2 + button_length * (2 * j - 3) / 2, Screen.height / 8 + 50 + button_width * i, button_length, button_width), "O");
                else
                    if (GUI.Button(new Rect(Screen.width / 2 + button_length * (2 * j - 3) / 2, Screen.height / 8 + 50 + button_width * i, button_length, button_width), ""))
                {
                    board[i, j] = turn;
                    if (turn == 1) turn = 2;
                    else turn = 1;
                }
    }

    void OnGUI()
    {
        GUI.skin = MY_GUI;
        GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 16, 160, 50), "Tic-Tac-Toe"); //标题
        int situation = game_situation();
        if (situation != 0) game_start = false;
        if (game_start)
            draw_board();
        else
        {
            string words;
            if (situation == 1) words = "Winner is X!";
            else if (situation == 2) words = "Winner is O!";
            else words = "End in a draw";
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 4, 200, 50), words);
            if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2 - Screen.height / 16, 250, 80), "Start a new game"))
            {
                game_start = true;
                restart();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        choose = (choose + 1) % 8;
        if ((!game_start)&&(choose%2 == 0))
        {
            if(choose/2 == 0)
                Instantiate(this.ball1);
            else if(choose/2 == 1)
                Instantiate(this.ball2);
            else if(choose/2 == 2)
                Instantiate(this.ball3);
            else 
                Instantiate(this.ball4);
        }
    }
}
