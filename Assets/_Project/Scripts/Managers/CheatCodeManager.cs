using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodeManager : Singleton<CheatCodeManager>
{
    [SerializeField] private string cheatCode;
    private int index = 0;

    private void Update()
    {
        if (Input.anyKeyDown)   
        {
            if (Input.GetKeyDown(cheatCode[index].ToString()))
            {
                index++;

                if (index == cheatCode.Length)
                {
                    ScoreManager.ResetHighScore();

                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                    index = 0;
                }
            }

            else
            {
                index = 0;
            }
        }
    }
}
