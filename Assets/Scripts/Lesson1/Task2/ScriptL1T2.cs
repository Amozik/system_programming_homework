using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Lesson1.Task2
{
    public class ScriptL1T2 : MonoBehaviour
    {
        private CancellationTokenSource _cts;
       
        private void Start()
        {
            _cts = new CancellationTokenSource();

            Task.Run(() => Task1(_cts.Token));
            Task.Run(() => Task2(60, _cts.Token));
        }

        public static async Task Task1(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            Debug.Log("Task1 completed");
        }

        public static async Task Task2(int framesNumber, CancellationToken cancellationToken)
        {
            var currentFrame = 0;

            while (currentFrame <= framesNumber)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                await Task.Yield();
                currentFrame++;
            }

            Debug.Log("Task2 completed");
        }

        private void OnDestroy()
        {
            _cts?.Dispose();
        }
    }
}
