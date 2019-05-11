﻿using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        EntityManager.Instance.Entities.Add(this);
    }
    protected virtual void OnDestroy()
    {
        EntityManager.Instance.Entities.Remove(this);
    }
    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public static Entity Find(string name)
        => EntityManager.FindEntity(name);

    public static EntityT Find<EntityT>(string name) where EntityT : Entity
        => EntityManager.FindEntity<EntityT>(name);

    public static EntityT[] FindAll<EntityT>() where EntityT : Entity
        => EntityManager.FindEntities<EntityT>();
}
