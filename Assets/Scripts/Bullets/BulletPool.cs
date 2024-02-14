using CosmicCuration.Bullets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private BulletView bulletView;
    private BulletScriptableObject bulletSO;

    private List<PooledBullet> bulletList;
    public BulletPool(BulletScriptableObject _bulletSO, BulletView _bulletView) 
    {
        bulletSO = _bulletSO;
        bulletView = _bulletView;

        bulletList = new List<PooledBullet>();
    }
    public class PooledBullet
    {
        public bool isUsed;
        public BulletController bullet;
    }
}
