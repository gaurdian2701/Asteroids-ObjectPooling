using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyScriptableObject enemySO;

        private List<PooledEnemy> enemiesInPool;
        public EnemyPool(EnemyScriptableObject _enemySO, EnemyView _enemyView)
        {
            enemySO = _enemySO;
            enemyView = _enemyView;

            enemiesInPool = new List<PooledEnemy>();
        }

        public EnemyController GetEnemy()
        {
            if (enemiesInPool.Count > 0)
            {
                PooledEnemy enemyInstance = enemiesInPool.Find((enemy) => !enemy.isUsed);

                if (enemyInstance != null)
                {
                    enemyInstance.isUsed = true;
                    return enemyInstance.enemyController;
                }
            }
            return CreateNewEnemyInstance();
        }

        public void ReturnEnemy(EnemyController _enemyController)
        {
            PooledEnemy enemyInPool = enemiesInPool.Find(enemy => enemy.enemyController == _enemyController);
            enemyInPool.isUsed = false;
        }

        private EnemyController CreateNewEnemyInstance()
        {
            EnemyController enemy = new EnemyController(enemyView, enemySO.enemyData);
            PooledEnemy pooledEnemy = new PooledEnemy();

            pooledEnemy.enemyController = enemy;
            pooledEnemy.isUsed = true;
            enemiesInPool.Add(pooledEnemy);

            return enemy;
        }

        public class PooledEnemy
        {
            public bool isUsed;
            public EnemyController enemyController;
        }
    }
}
