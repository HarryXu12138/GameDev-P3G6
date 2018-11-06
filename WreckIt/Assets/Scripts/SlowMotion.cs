using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    public float slowDownScale = 0.02f;
    public float slowDownTime = 5f;

    private void Update()
    {
        Time.timeScale += (1f / slowDownTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void slowDown()
    {
        Time.timeScale = slowDownScale;
        Time.fixedDeltaTime = Time.timeScale * 0.08f;
        print("slow Down");
    }
}
