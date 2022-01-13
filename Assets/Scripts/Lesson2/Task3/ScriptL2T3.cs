using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Lesson2.Task3
{
    public class ScriptL2T3 : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField] 
        private float _speed = 45f;

        private TransformAccessArray _transformAccessArray;

        private struct RotateJob : IJobParallelForTransform
        {
            [ReadOnly]
            public float Speed;
            [ReadOnly] 
            public float DeltaTime;

            public void Execute(int index, TransformAccess transform)
            {
                transform.rotation *= Quaternion.Euler(new Vector3(0, Speed * DeltaTime,0));
            }
        }

        private void Start()
        {
            var objTransform = Instantiate(_prefab).transform;

            _transformAccessArray = new TransformAccessArray(new[] {objTransform});
        }

        private void Update()
        {
            var rotateJob = new RotateJob
            {
                Speed = _speed,
                DeltaTime = Time.deltaTime,
            };

            var handle = rotateJob.Schedule(_transformAccessArray);
            handle.Complete();
        }

        private void OnDestroy()
        {
            _transformAccessArray.Dispose();
        }
    }
}