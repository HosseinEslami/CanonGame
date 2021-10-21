using Unity.Collections;
using Unity.Jobs;

public struct ZombieAIUpdateJob : IJobParallelFor
{
    public NativeArray<ZombieAI.Data> ZombieDataArray;
    
    public void Execute(int index)
    {
        var data = ZombieDataArray[index];
        data.Update();
        ZombieDataArray[index] = data;
    }
}