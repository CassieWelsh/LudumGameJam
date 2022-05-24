using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class SpawnProperties
    {
        public GameState state; 
        
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