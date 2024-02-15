using System;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> where T : class
{
    private List<PooledItem> pooledItems = new List<PooledItem>();
    
    protected T GetItem()
    {
        if(pooledItems.Count > 0)
        {
            PooledItem itemInPool = pooledItems.Find(i => !i.isUsed);

            if(itemInPool != null)
            {
                itemInPool.isUsed = true;
                return itemInPool.itemController;
            }
        }

        return CreateNewPooledItem();
    }

    private T CreateNewPooledItem()
    {
        PooledItem pooledItem = new PooledItem();
        pooledItem.itemController = CreateNewItemController();
        pooledItem.isUsed = true;

        pooledItems.Add(pooledItem);

        return pooledItem.itemController;
    }

    protected virtual T CreateNewItemController()
    {
        //This is to be implemented in child class to create appropriate controller
        throw new NotImplementedException("Child class has not implemented Create New Item Controller");
    }

    public void ReturnItem(T item)
    {
        PooledItem pooledItem = pooledItems.Find(i => i.itemController.Equals(item));
        pooledItem.isUsed = false;
    }

    public class PooledItem
    {
        public bool isUsed;
        public T itemController;
    }
}
