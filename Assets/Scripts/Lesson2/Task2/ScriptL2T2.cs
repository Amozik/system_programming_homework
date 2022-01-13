using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lesson2.Task2
{
    public class ScriptL2T2 : MonoBehaviour
    {
        private NativeArray<Vector3> _positions;
        private NativeArray<Vector3> _velocities;
        private NativeArray<Vector3> _finalPositions;

        private struct MyJob : IJobParallelFor
        {
            [ReadOnly]
            public NativeArray<Vector3> Positions;
            [ReadOnly]
            public NativeArray<Vector3> Velocities;
            [WriteOnly]
            public NativeArray<Vector3> FinalPositions;

            public void Execute(int index)
            {
                FinalPositions[index] = Positions[index] + Velocities[index];
            }
        }

        private void Start()
        {
            const int vectorsCount = 5;

            _positions = new NativeArray<Vector3>(vectorsCount, Allocator.Persistent);
            _velocities = new NativeArray<Vector3>(vectorsCount, Allocator.Persistent);
            _finalPositions = new NativeArray<Vector3>(vectorsCount, Allocator.Persistent);

            for (var i = 0; i < vectorsCount; i++)
            {
                _positions[i] = Random.insideUnitSphere;
                _velocities[i] = Random.insideUnitSphere;
            }
            
            var myJob = new MyJob
            {
                Positions = _positions,
                Velocities = _velocities,
                FinalPositions = _finalPositions,
            };

            var handle = myJob.Schedule(vectorsCount, 0);
            handle.Complete();

            foreach (var finalPosition in _finalPositions)
            {
                Debug.Log(finalPosition);
            }
        }

        private void OnDestroy()
        {
            _positions.Dispose();
            _velocities.Dispose();
            _finalPositions.Dispose();
        }
    }
}