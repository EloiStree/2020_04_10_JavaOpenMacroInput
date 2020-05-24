using System;
using System.Collections;
using UnityEngine;

public  class JavaOmiCoroutine: MonoBehaviour
{
	private static JavaOmiCoroutine m_instanceInScene;

	public static JavaOmiCoroutine InstanceInScene
	{
		get {
			if (m_instanceInScene == null)
			{
				m_instanceInScene=  GameObject.FindObjectOfType<JavaOmiCoroutine>();
			}
				
			if (m_instanceInScene == null) { 
				GameObject obj = new GameObject("JavaOMI Created Coroutine Manager");
				m_instanceInScene = obj.AddComponent<JavaOmiCoroutine>();
			}
			return m_instanceInScene; 
		}
		set { m_instanceInScene = value; }
	}


	public static void Start( IEnumerator toExecture)
    {
		InstanceInScene.ExecutreNewCoroutine(toExecture);
    }

	private void ExecutreNewCoroutine(IEnumerator toExecture)
	{
		StartCoroutine(toExecture);
	}
}