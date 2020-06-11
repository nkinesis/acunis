using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.SceneManagement;
using System;

public class RunUI : MonoBehaviour
{
    public static int currentPlayer = 1;
    public static bool isZeroGravity = false;
    public static Player player1;
    public static Player player2;
    public static bool inputsBlocked = false;
    public static bool isGameStarted = false;
    public static bool isPaused = false;
    public static bool isHelpOpen = false;
    public static GameObject pauseMenuUI, mainMenuUI, toggleMusic, audioMain;
    public static GameObject tm, te, td, tsf, tdf, tai, tdi, tg, inst, win1, win2;
    public static int currentHelpPanel = 0;
    private static bool zeroGravityMusic = false;

    // métodos principais
    void Start()
    {
        // instanciar objetos player
        RunUI.player1 = new Player();
        RunUI.player2 = new Player();
        RunUI.pauseMenuUI = GameObject.Find("PauseMenu");
        RunUI.mainMenuUI = GameObject.Find("MainMenu");
        RunUI.toggleMusic = GameObject.Find("ipt_music");
        RunUI.audioMain = GameObject.Find("Audio_Main");

        RunUI.tm = GameObject.Find("text_mass");
        RunUI.te = GameObject.Find("text_bounce");
        RunUI.td = GameObject.Find("text_drag");
        RunUI.tsf = GameObject.Find("text_stfri");
        RunUI.tdf = GameObject.Find("text_dinfri");
        RunUI.tai = GameObject.Find("text_angleinc");
        RunUI.tdi = GameObject.Find("text_dirinc");
        RunUI.tg = GameObject.Find("text_gravity");
        RunUI.inst = GameObject.Find("text_control_ref");
        RunUI.win1 = GameObject.Find("text_win_p1");
        RunUI.win2 = GameObject.Find("text_win_p2");

        // definir listeners (opções)
        GameObject.Find("btn_mass").GetComponent<Button>().onClick.AddListener(btnMass);
        GameObject.Find("btn_elast").GetComponent<Button>().onClick.AddListener(btnElasticity);
        GameObject.Find("btn_drag").GetComponent<Button>().onClick.AddListener(btnDrag); 
        GameObject.Find("btn_stfri").GetComponent<Button>().onClick.AddListener(btnStFri); 
        GameObject.Find("btn_dinfri").GetComponent<Button>().onClick.AddListener(btnDinFri); 
        GameObject.Find("btn_angleinc").GetComponent<Button>().onClick.AddListener(btnInc);
        GameObject.Find("ipt_gravity").GetComponent<Dropdown>().onValueChanged.AddListener(delegate {
            dropdownGravity(GameObject.Find("ipt_gravity").GetComponent<Dropdown>());
        });
        GameObject.Find("btn_restart").GetComponent<Button>().onClick.AddListener(restartMatch);
        GameObject.Find("btn_exit").GetComponent<Button>().onClick.AddListener(quitMatch);
        GameObject.Find("StartBtn").GetComponent<Button>().onClick.AddListener(btnStartGame);
        GameObject.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(btnExitGame);

        // definir listeners (ajuda)
        GameObject.Find("btn_help_mass").GetComponent<Button>().onClick.AddListener(btnHelpMass);
        GameObject.Find("btn_help_elast").GetComponent<Button>().onClick.AddListener(btnHelpElasticity);
        GameObject.Find("btn_help_drag").GetComponent<Button>().onClick.AddListener(btnHelpDrag);
        GameObject.Find("btn_help_stfri").GetComponent<Button>().onClick.AddListener(btnHelpStFri);
        GameObject.Find("btn_help_dinfri").GetComponent<Button>().onClick.AddListener(btnHelpDinFri);
        GameObject.Find("btn_help_angleinc").GetComponent<Button>().onClick.AddListener(btnHelpInc);
        GameObject.Find("btn_help_gravity").GetComponent<Button>().onClick.AddListener(btnHelpGravity);
        GameObject.Find("btn_help_dirinc").GetComponent<Button>().onClick.AddListener(btnHelpDirinc);
        GameObject.Find("btn_instructions").GetComponent<Button>().onClick.AddListener(btnInstructions);
        GameObject.Find("tm_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("te_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("td_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("tsf_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("tdf_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("tai_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("tdi_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("tg_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("inst_close").GetComponent<Button>().onClick.AddListener(closeAllHelp);
        GameObject.Find("p1_replay").GetComponent<Button>().onClick.AddListener(restartMatch);
        GameObject.Find("p1_quit").GetComponent<Button>().onClick.AddListener(quitMatch);
        GameObject.Find("p2_replay").GetComponent<Button>().onClick.AddListener(restartMatch);
        GameObject.Find("p2_quit").GetComponent<Button>().onClick.AddListener(quitMatch);
        GameObject.Find("tm_info").GetComponent<Button>().onClick.AddListener(btnTmInfo);
        GameObject.Find("te_info").GetComponent<Button>().onClick.AddListener(btnTeInfo);
        GameObject.Find("td_info").GetComponent<Button>().onClick.AddListener(btnTdInfo);
        GameObject.Find("tsf_info").GetComponent<Button>().onClick.AddListener(btnTsfInfo);
        GameObject.Find("tdf_info").GetComponent<Button>().onClick.AddListener(btnTdfInfo);
        GameObject.Find("tdi_info").GetComponent<Button>().onClick.AddListener(btnTdiInfo);
        GameObject.Find("tg_info").GetComponent<Button>().onClick.AddListener(btnTgInfo);

        // Definir valores padrão
        GameObject.Find("ipt_mass").GetComponent<InputField>().text = "1.0";
        GameObject.Find("ipt_elast").GetComponent<InputField>().text = "0.25";
        GameObject.Find("ipt_drag").GetComponent<InputField>().text = "0.0";
        GameObject.Find("ipt_stfri").GetComponent<InputField>().text = "0.3";
        GameObject.Find("ipt_dinfri").GetComponent<InputField>().text = "0.9";

        // esconder menu pausa
        if (!RunUI.isPaused)
        {
            RunUI.pauseMenuUI.SetActive(false);
        } else
        {
            RunUI.pauseMenuUI.SetActive(true);
        }

        if (!RunUI.isGameStarted)
        {
            RunUI.mainMenuUI.SetActive(true);
        } else {
            RunUI.mainMenuUI.SetActive(false);
        }
        RunUI.win1.SetActive(false);
        RunUI.win2.SetActive(false);

        closeAllHelp();

    }

    private void quitMatch()
    {
        SceneManager.LoadScene("SampleScene");
        RunUI.player1 = new Player();
        RunUI.player2 = new Player();
        RunUI.currentPlayer = 1;
        RunUI.inputsBlocked = false;
        RunUI.isGameStarted = false;
        RunUI.isHelpOpen = false;
        RunUI.isPaused = false;
        resume();
    }

    private void restartMatch()
    {
        SceneManager.LoadScene("SampleScene");
        RunUI.player1 = new Player();
        RunUI.player2 = new Player();
        RunUI.currentPlayer = 1;
        RunUI.inputsBlocked = false;
        RunUI.isGameStarted = true;
        RunUI.isHelpOpen = false;
        RunUI.isPaused = false;
        resume();
    }

    void Update()
    {
        // toggle menu pausa
        onPause();

        // checar demais comandos só se o jogo estiver em curso
        if (!RunUI.isPaused) { 
            if (Input.GetKeyDown("z")) // passa rodada
            {
                switchPlayer();
            }
            if (Input.GetKeyDown("q")) // aumenta força
            {
                if (RunUI.currentPlayer == 1 && getCurrentForce() < 40)
                {
                    RunUI.player1.Force += 5;
                } else if (RunUI.currentPlayer == 2 && getCurrentForce() < 40)
                {
                    RunUI.player2.Force += 5;
                }
            }
            if (Input.GetKeyDown("w")) // diminui força
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
            if (Input.GetKeyDown("r")) // reseta bola branca
            {
                var cue = GameObject.Find("CueBall");
                cue.transform.position = new Vector3(12.4f, 3.6f, 11.3f);
                cue.GetComponent<Rigidbody>().velocity = Vector3.zero;
                cue.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }

        if (RunUI.isZeroGravity && RunUI.audioMain.GetComponent<AudioSource>().clip.name == "acunis_theme")
        {
            RunUI.audioMain.GetComponent<AudioSource>().Stop();
            RunUI.audioMain.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("blauen");
            RunUI.audioMain.GetComponent<AudioSource>().Play();
            print(RunUI.audioMain.GetComponent<AudioSource>().clip);
        } else
        {
            if (!RunUI.isZeroGravity && RunUI.audioMain.GetComponent<AudioSource>().clip.name == "blauen")
            {
                RunUI.audioMain.GetComponent<AudioSource>().Stop();
                RunUI.audioMain.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("acunis_theme");
                RunUI.audioMain.GetComponent<AudioSource>().Play();
                RunUI.zeroGravityMusic = false;
            }

            if (!RunUI.toggleMusic.GetComponent<Toggle>().isOn)
            {
                RunUI.audioMain.GetComponent<AudioSource>().Stop();
                print("stop music");
            } else if (!RunUI.audioMain.GetComponent<AudioSource>().isPlaying
              && RunUI.toggleMusic.GetComponent<Toggle>().isOn)
            {
                RunUI.audioMain.GetComponent<AudioSource>().Play();
                print("play music");
            }
        }
        
    }

    void OnGUI()
    {
        if (RunUI.isGameStarted && !RunUI.isHelpOpen)
        {
            GUI.Box(new Rect(10, 10, 120, 30), "Agora: Jogador " + RunUI.currentPlayer);
            GUI.Box(new Rect(200, 10, 120, 30), "Jogador 1: " + RunUI.player1.Score);
            GUI.Box(new Rect(300, 10, 120, 30), "Jogador 2:  " + RunUI.player2.Score);

            if (!RunUI.isPaused)
            {
                GUI.Box(new Rect(10, Screen.height - 30, 100, 30), "Força:  " + RunUI.getCurrentForce());
                GUI.Box(new Rect(Screen.width - 300, 10, 300, 30), "Pressione Z para passar a rodada.");
            }

        }
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
        print("Mass changed");
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
        print("Gravity changed");
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
        print("Bounce changed");
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
                x.GetComponent<Collider>().enabled = false;
                x.GetComponent<Collider>().enabled = true;
        }
        print("D. Friction changed");
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
                x.GetComponent<Collider>().enabled = false;
                x.GetComponent<Collider>().enabled = true;
        }
        print("S. Friction changed");
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
        print("Drag changed");
    }

    public void changeTableInclination(float newValue)
    {
        if (newValue < -45)
        {
            GameObject.Find("BilliardsTable").transform.Rotate(-45f, 0.0f, 0.0f);
        } else if (newValue > 45) {
            GameObject.Find("BilliardsTable").transform.Rotate(45f, 0.0f, 0.0f);
        } else
        {
            GameObject.Find("BilliardsTable").transform.Rotate(newValue, 0.0f, 0.0f);
        }

    }

    // menu pausa
    public void onPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RunUI.isGameStarted)
            {
                if (RunUI.isHelpOpen)
                {
                    closeAllHelp();
                } else if (RunUI.isPaused)
                {
                    isPaused = false;
                    resume();
                }
                else
                {
                    isPaused = true;
                    pause();
                }
            }
        }
    }

    public void resume()
    {
        if (RunUI.pauseMenuUI == null)
        {
            RunUI.pauseMenuUI = GameObject.Find("PauseMenu");
        }
        RunUI.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pause()
    {
        if (RunUI.pauseMenuUI == null)
        {
            RunUI.pauseMenuUI = GameObject.Find("PauseMenu");
        }
        RunUI.pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // ações de botões
    private void closeAllHelp()
    {
        RunUI.tm.SetActive(false);
        RunUI.te.SetActive(false);
        RunUI.td.SetActive(false);
        RunUI.tsf.SetActive(false);
        RunUI.tdf.SetActive(false);
        RunUI.tdi.SetActive(false);
        RunUI.tai.SetActive(false);
        RunUI.tg.SetActive(false);
        RunUI.inst.SetActive(false);
        RunUI.win1.SetActive(false);
        RunUI.win2.SetActive(false);
        RunUI.isHelpOpen = false;
    }

    public void btnMass()
    {
        float mass = float.Parse(GameObject.Find("ipt_mass").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeMass(mass);
    }

    public void btnGravity()
    {
        float gravity = float.Parse(GameObject.Find("ipt_gravity").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeGravity(gravity);
    }

    public void btnElasticity()
    {
        float elast = float.Parse(GameObject.Find("ipt_elast").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeBounciness(elast);
    }

    public void btnDrag()
    {
        float value = float.Parse(GameObject.Find("ipt_drag").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeDrag(value);
    }

    public void btnStFri()
    {
        float value = float.Parse(GameObject.Find("ipt_dinfri").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeStaticFriction(value);
    }

    public void btnDinFri()
    {
        float value = float.Parse(GameObject.Find("ipt_stfri").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);
        changeDynamicFriction(value);
    }

    public void btnInc()
    {
        GameObject toggle = GameObject.Find("ipt_dirinc");
        float value = float.Parse(GameObject.Find("ipt_angleinc").GetComponent<InputField>().text, CultureInfo.InvariantCulture.NumberFormat);

        if (toggle.GetComponent<Toggle>().isOn)
        {
            value = value * -1;
        }
        changeTableInclination(value);
    }

    public void dropdownGravity(Dropdown change)
    {
        switch (change.value)
        {
            case 0: //terra
                Physics.gravity = new Vector3(0, -9.8f, 0);
                break;
            case 1: //sol
                Physics.gravity = new Vector3(0, -274f, 0);
                break;
            case 2: //jupiter
                Physics.gravity = new Vector3(0, -24.9f, 0);
                break;
            case 3: //netuno
                Physics.gravity = new Vector3(0, -11.1f, 0);
                break;
            case 4: //saturno
                Physics.gravity = new Vector3(0, -10.4f, 0);
                break;
            case 5: //urano/venus
                Physics.gravity = new Vector3(0, -8.8f, 0);
                break;
            case 6: //marte/mercurio
                Physics.gravity = new Vector3(0, -3.7f, 0);
                break;
            case 7: //lua
                Physics.gravity = new Vector3(0, -1.6f, 0);
                break;
            case 8: //espaço
                Physics.gravity = new Vector3(0, 0.0f, 0);
                break;
            case 9: //raio trator
                Physics.gravity = new Vector3(0, 0.05f, 0);
                break;
            default: //terra
                Physics.gravity = new Vector3(0, -9.8f, 0);
                break;
        }

        if (change.value == 8 || change.value == 9)
        {
            RunUI.isZeroGravity = true;
        } else
        {
            RunUI.isZeroGravity = false;
        }
        
    }

    public void btnStartGame()
    {
        RunUI.isGameStarted = true;
        GameObject.Find("MainMenu").SetActive(false);
    }

    public void btnExitGame()
    {
        RunUI.isGameStarted = false;
        Application.Quit();
    }

    private void btnHelpMass()
    {
        RunUI.tm.SetActive(RunUI.tm.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpInc()
    {
        RunUI.tai.SetActive(RunUI.tai.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpDinFri()
    {
        RunUI.tdf.SetActive(RunUI.tdf.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpStFri()
    {
        RunUI.tsf.SetActive(RunUI.tsf.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpDrag()
    {
        RunUI.td.SetActive(RunUI.td.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpElasticity()
    {
        RunUI.te.SetActive(RunUI.te.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpGravity()
    {
        RunUI.tg.SetActive(RunUI.tg.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnHelpDirinc()
    {
        RunUI.tdi.SetActive(RunUI.tdi.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }

    private void btnInstructions()
    {
        RunUI.inst.SetActive(RunUI.inst.activeSelf ? false : true);
        RunUI.isHelpOpen = true;
    }


    private void btnTgInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/o-que-e/fisica/o-que-e-gravidade.htm");
    }

    private void btnTdiInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/fisica/nocoes-importantes.htm");
    }

    private void btnTdfInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/fisica/forca-atrito.htm");
    }

    private void btnTsfInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/fisica/forca-atrito.htm");
    }

    private void btnTdInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/fisica/forca-resistencia-ar.htm");
        Application.OpenURL("https://www.portalsaofrancisco.com.br/fisica/resistencia-do-ar");
    }

    private void btnTeInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/quimica/propriedades-materia.htm");
    }

    private void btnTmInfo()
    {
        Application.OpenURL("https://brasilescola.uol.com.br/o-que-e/fisica/o-que-e-massa.htm");
    }



}
