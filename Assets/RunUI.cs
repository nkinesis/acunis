using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class RunUI : MonoBehaviour
{
    public static int currentPlayer = 1;
    public static Player player1;
    public static Player player2;
    public static bool inputsBlocked = false;
    public static bool isPaused = false;
    public static GameObject pauseMenuUI;
   
    // métodos principais
    void Start()
    {
        // instanciar objetos player
        RunUI.player1 = new Player();
        RunUI.player2 = new Player();

        // definir listeners
        GameObject.Find("btn_mass").GetComponent<Button>().onClick.AddListener(btnMass);
        GameObject.Find("btn_gravity").GetComponent<Button>().onClick.AddListener(btnGravity);
        GameObject.Find("btn_elast").GetComponent<Button>().onClick.AddListener(btnElasticity);        GameObject.Find("btn_gravity").GetComponent<Button>().onClick.AddListener(btnGravity);

        // esconder menu pausa
        RunUI.pauseMenuUI = GameObject.Find("PauseMenu");
        RunUI.pauseMenuUI.SetActive(false);

   }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            switchPlayer();
        }
        if (Input.GetKeyDown("q"))
        {
            if (RunUI.currentPlayer == 1 && getCurrentForce() < 40)
            {
                RunUI.player1.Force += 5;
            } else if (RunUI.currentPlayer == 2 && getCurrentForce() < 40)
            {
                RunUI.player2.Force += 5;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            if (RunUI.currentPlayer == 1 && getCurrentForce() > 5)
            {
                RunUI.player1.Force -= 5;
            }
            else if (RunUI.currentPlayer == 2 && getCurrentForce() > 5)
            {
                RunUI.player2.Force -= 5;
            }
        }

        onPause();
    }

    void OnGUI()
    {

        GUI.Box(new Rect(10, 10, 120, 30), "Agora: Jogador " + RunUI.currentPlayer);
        GUI.Box(new Rect(200, 10, 120, 30), "Jogador 1: " + RunUI.player1.Score);
        GUI.Box(new Rect(300, 10, 120, 30), "Jogador 2:  " + RunUI.player2.Score);
        GUI.Box(new Rect(10, Screen.height - 30, 100, 30), "Força:  " + RunUI.getCurrentForce());

        GUI.Box(new Rect(Screen.width - 300, 10, 300, 30), "Pressione Z para passar a rodada.");


    }

    // lógica do jogo
    public void switchPlayer()
    {
        RunUI.currentPlayer = (RunUI.currentPlayer == 1) ? 2 : 1;
        RunUI.inputsBlocked = false;

        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.GetComponent<Rigidbody>() != null)
            {
                x.GetComponent<Rigidbody>().velocity = Vector3.zero;
                x.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

        }
    }

    // manipulação de física
    public static float getCurrentForce()
    {
        if (RunUI.currentPlayer == 1)
        {
            return RunUI.player1.Force;
        }
        else
        {
            return RunUI.player2.Force;
        }
    }

    public void changeMass(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.name == "CueBall")
            {
                continue;
            }
            if (x.GetComponent<Rigidbody>() != null)
            {
                if (newValue <= 0f)
                {
                    x.GetComponent<Rigidbody>().mass = 1;
                }
                else if (newValue > 999)
                {
                    x.GetComponent<Rigidbody>().mass = 999;
                }
                else
                {
                    x.GetComponent<Rigidbody>().mass = newValue;
                }

            }

        }
    }

    public void changeGravity(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.GetComponent<Rigidbody>() != null)
            {
                if (newValue <= 0f)
                {
                    x.GetComponent<Rigidbody>().useGravity = false;
                } else
                {
                    x.GetComponent<Rigidbody>().useGravity = true;
                }

            }

        }
    }

    public void changeBounciness(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.GetComponent<Rigidbody>() != null)

                if (newValue <= 0f)
                {
                    x.GetComponent<Collider>().material.bounciness = 0;
                }
                else if (newValue > 1)
                {
                    x.GetComponent<Collider>().material.bounciness = 1;
                }
                else
                {
                    x.GetComponent<Collider>().material.bounciness = newValue;
                }

        }
    }

    public void changeDynamicFriction(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.GetComponent<Rigidbody>() != null)

                if (newValue <= 0f)
                {
                    x.GetComponent<Collider>().material.dynamicFriction = 0;
                }
                else if (newValue > 1)
                {
                    x.GetComponent<Collider>().material.dynamicFriction = 1;
                }
                else
                {
                    x.GetComponent<Collider>().material.dynamicFriction = newValue;
                }

        }
    }


    public void changeStaticFriction(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.GetComponent<Rigidbody>() != null)

                if (newValue <= 0f)
                {
                    x.GetComponent<Collider>().material.staticFriction = 0;
                }
                else if (newValue > 1)
                {
                    x.GetComponent<Collider>().material.staticFriction = 1;
                }
                else
                {
                    x.GetComponent<Collider>().material.staticFriction = newValue;
                }

        }
    }

    public void changeDrag(float newValue)
    {
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject x = (GameObject)o;
            if (x.name == "CueBall")
            {
                continue;
            }
            if (x.GetComponent<Rigidbody>() != null)
            {
                if (newValue <= 0f)
                {
                    x.GetComponent<Rigidbody>().drag = 1;
                }
                else if (newValue > 999)
                {
                    x.GetComponent<Rigidbody>().drag = 999;
                }
                else
                {
                    x.GetComponent<Rigidbody>().drag = newValue;
                }

            }

        }
    }


    // menu pausa
    public void onPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                resume();
            } else
            {
                isPaused = true;
                pause();
            }
        }
    }

    public void resume()
    {
        print("resume");
        RunUI.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pause()
    {
        print("pause");
        RunUI.pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // ações de botões
    public void btnMass()
    {
        float mass = float.Parse(GameObject.Find("ipt_mass").GetComponent<InputField>().text);
        changeMass(mass);
    }

    public void btnGravity()
    {
        float gravity = float.Parse(GameObject.Find("ipt_gravity").GetComponent<InputField>().text);
        changeGravity(gravity);
    }
    public void btnElasticity()
    {
        float elast = float.Parse(GameObject.Find("ipt_elast").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        print(GameObject.Find("ipt_elast").GetComponent<InputField>().text);
        print(elast);
        changeBounciness(elast);
    }
}
