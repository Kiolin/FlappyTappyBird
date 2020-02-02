using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ParallaxerConfig", menuName = "Create Parallaxer config")]
public class ParallaxerConfig : ScriptableObject
{
	//Хранит переменные для настройки спавнера труб.
	[System.Serializable]
	public struct SpawnHeight
	{
		public float min;
		public float max;
	}

	public GameObject PipePrefab;
	public float shiftSpeed;
	public float spawnRate;
	public SpawnHeight spawnHeight;
	public Vector3 spawnPos;
	public Vector2 targetAspectRatio;
	public bool beginInScreenCenter;
}

