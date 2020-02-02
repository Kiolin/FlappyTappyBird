using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Create game config")]
public class GameConfig : ScriptableObject
{
    //Хранит переменные для настройки игры.
    public int tapForce;
    public int tiltSmooth;
    public Vector3 birbStartPos;
}

