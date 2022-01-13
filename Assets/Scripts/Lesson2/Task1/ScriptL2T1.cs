using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScriptL2T1 : MonoBehaviour
{
    private NativeArray<int> _inArray;

    private struct MyJob : IJob
    {
        public NativeArray<int> InArray;
        
        public void Execute()
        {
            for (var i = 0; i < InArray.Length; i++)
            {
                InArray[i] = InArray[i] > 10 ? 0 : InArray[i];
            }
        }
    }
    
    private void Start()
    {
        const int length = 10;
        _inArray = new NativeArray<int>(length, Allocator.Persistent);

        Debug.Log("--- Input ---");        
        for (var i = 0; i < length; i++)
        {
            _inArray[i] = Random.Range(1, 20);
            Debug.Log(_inArray[i]);
        }

        var myJob = new MyJob
        {
            InArray = _inArray,
        };

        var handle = myJob.Schedule();
        handle.Complete();
        
        Debug.Log("--- Output ---");
        foreach (var item in _inArray)
        {
            Debug.Log(item);
        }
    }

    private void OnDestroy()
    {
        _inArray.Dispose();
    }
}
