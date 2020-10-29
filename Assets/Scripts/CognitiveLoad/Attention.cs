using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    float initialTime;
    float endTime;

    public Attention(float initialTime, float endTime)
    {
        this.initialTime = initialTime;
        this.endTime = endTime;
    }

    public float getInitialTime()
    {
        return initialTime;
    }
    public float getEndTime()
    {
        return endTime;
    }

    public void setInitialTime(float updatedInitialTime)
    {
        this.initialTime = updatedInitialTime;
    }
    public void setEndTime(float updatedEndTime)
    {
        this.endTime = updatedEndTime;
    }

    public string toString()
    {
        return "[" + initialTime + "," + endTime + "]";
    }

    public float attentionTime()
    {
        float attentionTime = 0;
        if ((endTime > initialTime) && (initialTime>0))
            attentionTime = endTime - initialTime;
        return attentionTime;

    }

    public void resetTime()
    {
        this.initialTime = 0;
        this.endTime = 0;
    }
}
