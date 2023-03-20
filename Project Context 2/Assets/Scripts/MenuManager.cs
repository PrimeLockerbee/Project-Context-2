using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _miniMap;
    [SerializeField] GameObject _endCam;
    [SerializeField] GameObject _menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_menu.activeSelf.Equals(false))
            {
                _menu.SetActive(true);
                _miniMap.SetActive(false);
            }
            else
            {
                _menu.SetActive(false);
                _miniMap.SetActive(true);
                _endCam.SetActive(false);
            }
        }
    }

    public void SceneLoader(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
