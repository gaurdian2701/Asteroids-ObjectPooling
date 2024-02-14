using CosmicCuration.Bullets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private BulletView bulletView;
    private BulletScriptableObject bulletSO;

    private List<PooledBullet> bulletsInPool;
    public BulletPool(BulletScriptableObject _bulletSO, BulletView _bulletView) 
    {
        bulletSO = _bulletSO;
        bulletView = _bulletView;

        bulletsInPool = new List<PooledBullet>();
    }

    public BulletController GetBullet() 
    {
        if(bulletsInPool.Count > 0)
        {
            PooledBullet bullet = bulletsInPool.Find((bullet) => !bullet.isUsed);

            if(bullet != null)
            {
                bullet.isUsed = true;
                return bullet.bulletController;
            }
        }
        return CreateNewBulletInstance();
    }

    public void ReturnBullet(BulletController _bulletController)
    {
        PooledBullet bulletInPool = bulletsInPool.Find(bulletInstance => bulletInstance.bulletController == _bulletController);
        bulletInPool.isUsed = false;
    }

    private BulletController CreateNewBulletInstance()
    {
        BulletController bullet = new BulletController(bulletView, bulletSO);
        PooledBullet pooledBullet = new PooledBullet();

        pooledBullet.bulletController = bullet;
        pooledBullet.isUsed = true;
        bulletsInPool.Add(pooledBullet);

        return bullet;
    }

    public class PooledBullet
    {
        public bool isUsed;
        public BulletController bulletController;
    }
}
