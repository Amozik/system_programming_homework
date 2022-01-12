using System;
using System.Threading;
using System.Threading.Tasks;
using Lesson1.Task2;
using UnityEngine;

namespace Lesson1.Task3
{
    public class ScriptL1T3 : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;

        private async void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            var task1 = ScriptL1T2.Task1(cancellationToken);
            var task2 = ScriptL1T2.Task2(60, cancellationToken);
            
            var isFirsTaskFaster = await WhatTaskFasterAsync(task1, task2, cancellationToken);
            
            
            Debug.Log($"Is First Task Faster {isFirsTaskFaster}");
        }

        private async Task<bool> WhatTaskFasterAsync(Task firstTask, Task secondTask, CancellationToken cancellationToken)
        {
            var finishedTask = await Task.WhenAny(firstTask, secondTask);
            if (cancellationToken.IsCancellationRequested)
                return false;
            
            return finishedTask == firstTask && finishedTask.IsCompleted;
        }
    }
}