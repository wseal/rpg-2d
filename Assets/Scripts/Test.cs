using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
  private IEnumerator SyaMessage()
  {
    Debug.Log("Hello from coroutine!");
    yield return new WaitForSeconds(2f);


    Debug.Log("Goodbye from coroutine!");
    yield return new WaitForSeconds(1f);

    Debug.Log("Coroutine finished!");
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.T))
    {
      StartCoroutine(SyaMessage());
    }
  }
}