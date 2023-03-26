using UnityEngine;

public static class RandomFromDistribution
{
    public static float random_normal_distribution(float mean, float standard_deviation)
    {
        float u1 = Random.Range(0.0f, 1.0f);
        float u2 = Random.Range(0.0f, 1.0f);
        
        float rand_std_normal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); // random normal(0,1)
        
        return mean + standard_deviation * rand_std_normal; // random normal(mean,stdDev^2)
    }
}
