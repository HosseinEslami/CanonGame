using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class ZombieAIManager : MonoBehaviour
{
    public List<ZombieAI> zombieAIs;
    public List<Zombie> zombies;
    
    private NativeArray<ZombieAI.Data> _zombieAIDataArray;
    private ZombieAIUpdateJob _job;

    private void Update()
    {
        _zombieAIDataArray = new NativeArray<ZombieAI.Data>(zombieAIs.Count, Allocator.TempJob);
        for (var i = 0; i < zombieAIs.Count; i++)
        {
            _zombieAIDataArray[i] = new ZombieAI.Data(zombieAIs[i]);
        }
        
        _job = new ZombieAIUpdateJob
        {
            ZombieDataArray = _zombieAIDataArray
        };
        var jobHandle = _job.Schedule(zombieAIs.Count, 1);
        jobHandle.Complete();
        
        if (_job.ZombieDataArray.Length > 0)
        {
            for (int i = 0; i < _job.ZombieDataArray.Length; i++)
            {
                if(_job.ZombieDataArray[i].attacked && !zombies[i].zAttacked) zombies[i].Attack();
            }
        }
        
        _zombieAIDataArray.Dispose();
    }
}