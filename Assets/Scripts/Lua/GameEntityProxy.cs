﻿using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LuaHost.Proxy
{
    class GameEntityProxy : ProxyBase<GameEntity>
    {

        [MoonSharpHidden]
        public GameEntityProxy(GameEntity entity) : base(entity)
        {
            //entity.OnUpdate += EntityUpdate;
        }
        private void EntityUpdate()
        {
            /*OnUpdate?.Invoke();
            if (target is LifeEntity && (target as LifeEntity).HP <= 0)
            {
                OnDead?.Invoke();
            }*/
        }
        public Vector2 Position
        {
            get
            {
                return target.transform.position.ToVector2();
            }
            set
            {
                target.transform.position = target.transform.position.Set(x: value.x, y: value.y);
            }
        }
        public bool Skill(int idx, float dir)
            => target.GetComponent<EntityController>()?.Skill(idx, dir) ?? false;
        public void Move(Vector2 movement)
            => target.GetComponent<EntityController>()?.Move(movement);
        public void Jump() 
            => target.GetComponent<EntityController>()?.Jump();
        public void Climb(float speed) 
            => target.GetComponent<EntityController>()?.Climb(speed);
        /*public IEnumerator Wait(int time)
        {
            foreach (var t in Utility.Timer(time))
                yield return null;
        }*/
        private IEnumerator WaitInternal(string target)
        {
            /*Debug.Log($"start wait {target}");
            foreach (var t in Utility.Timer(3))
                yield return null;*/

            switch (target)
            {
                case "skill":
                    yield return this.target.GetComponent<SkillController>().WaitSkill();
                    break;
                case "animation":
                case "action":
                    yield return this.target.GetComponent<AnimationController>().WaitAnimation();
                    break;
            }
            //Debug.Log($"end wait {target}");
        }
        /*public IEnumerator Wait(string target)
        {
            Debug.Log($"start wait {target}");
            foreach (var t in Utility.Timer(3))
                yield return null;

            switch (target)
            {
                case "skill":
                    yield return this.target.GetComponent<SkillController>().WaitSkill();
                    break;
                case "animation":
                case "action":
                    yield return this.target.GetComponent<AnimationController>().WaitAnimation();
                    break;
            }
            Debug.Log($"end wait {target}");
        }*/
        
        public UnityEngine.Coroutine Wait(string target, LuaScriptHost host)
        {
            return host.StartCoroutine(WaitInternal(target));   
        }
        /*-public UnityEngine.Coroutine StartCoroutine(Closure closure) 
            => target.StartCoroutine(closure.OwnerScript.CreateCoroutine(closure).Coroutine.AsUnityCoroutine());
        public UnityEngine.Coroutine StartCoroutine(MoonSharp.Interpreter.Coroutine coroutine) 
            => target.StartCoroutine(coroutine.AsUnityCoroutine());
        public void StopCoroutine(UnityEngine.Coroutine coroutine) 
            => target.StopCoroutine(coroutine);*/
        /*public event Action OnDead;*/
        /*public event Action OnUpdate;*/
    }
}