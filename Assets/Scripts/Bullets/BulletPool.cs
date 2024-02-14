using CosmicCuration.Bullets;
using System;
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

    public BulletController GetBullet() 
    {
        if(bulletList.Count > 0)
        {
            PooledBullet bullet = bulletList.Find((bullet) => !bullet.isUsed);

            if(bullet != null)
            {
                bullet.isUsed = true;
                return bullet.bulletController;
            }
        }
        return CreateNewBulletInstance();
    }

    private BulletController CreateNewBulletInstance()
    {
        BulletController bullet = new BulletController(bulletView, bulletSO);
        PooledBullet pooledBullet = new PooledBullet();

        pooledBullet.bulletController = bullet;
        pooledBullet.isUsed = true;
        bulletList.Add(pooledBullet);

        return bullet;
    }

    public class PooledBullet
    {
        public bool isUsed;
        public BulletController bulletController;
    }
}
