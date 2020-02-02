using UnityEngine;

public class TimeController3
{
    public void PauseOn()
    {
        Time.timeScale = 0;
    }
    public void PauseOff()
    {
        Time.timeScale = 1;
    }
}