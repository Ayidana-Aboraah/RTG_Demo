using UnityEngine;

[System.Serializable]
public sealed class Timer
{
	public bool isRunning, complete;
	public float maxTime;
	[HideInInspector] public float m_CurrentTime;

	public Timer(float maxTime, bool onStart)
	{
		this.maxTime = maxTime;

		if (onStart) Start();
	}

	public void Start()
	{
		complete = false;
		isRunning = true;
		m_CurrentTime = maxTime;
	}

	public void UpdateTimer()
	{
		if (!isRunning) return;
		
		m_CurrentTime -= Time.deltaTime;

		if (m_CurrentTime > 0) return;

		complete = true;
		isRunning = false;
	}

	public void ResetTimer()
	{
		isRunning = false;
		complete = false;
		m_CurrentTime = 0;
	}
}
