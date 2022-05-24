using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utils
{
    [Serializable]
    public class SpawnProperties
    {
        public GameState state;
        public float backgroundScrollSpeed;
        
        public float aSpawnBeginningOffset;
        public float aSpawnTimeout;
        public int aMaxObjectsInScene;
        public bool aLimitObjects;
        public GameObject[] asteroidPrefabs;
        
        public float eSpawnBeginningOffset;
        public float eSpawnTimeout;
        public int eMaxEnemiesInScene;
        public bool eLimitEnemies;
        public GameObject[] enemyPrefabs;
    }
}