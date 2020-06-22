using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform gooPrefab;

    private Transform goop;

    private void Start()
    {
        StartCoroutine(GenerateGoop());
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    private IEnumerator GenerateGoop()
    {
        while(true)
        {
            if(goop == null)
            {
                goop = Instantiate(gooPrefab);
                goop.position = new Vector2(Random.Range(-5f,5f),-4.5f);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
