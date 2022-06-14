using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    // UI
    public Slider particle_slider;
    public Slider initial_vel_slider;
    public TextMeshProUGUI particle_slider_label;
    public TextMeshProUGUI initial_vel_slider_label;
    public Slider G_slider;
    public CanvasGroup group;
    public TextMeshProUGUI G_label;

    public GameObject circleprefab;
    public int particle_num;
    public float gravitational_constant;
    Rigidbody2D[] particles;
    float mass_center_x;
    float mass_center_y;
    float mass_sum = 0;
    float mass_center_x_numerator = 0;
    float mass_center_y_numerator = 0;
    public static int init_vel;
    public static bool collider;
    float Force;
    Vector2 force_vector;
    float distance;
    public Toggle ColliderToggle;
    int tick; 

    // Camera 
    public TMP_Dropdown zoom_levels;
    public Toggle Hide;
    public bool CM;
    public Toggle CM_toggle;
    float zoom;
    Vector2[] forces;
 
    void Start()
    {
        tick = 0;
        // Camera init 
        CM = bool.Parse(PlayerPrefs.GetString("CM"));
        CM_toggle.isOn = CM;
        zoom = PlayerPrefs.GetFloat("zoom");
        gameObject.GetComponent<Camera>().orthographicSize = 17 * zoom;
        zoom_levels.value = (int) ((zoom - 0.75f)/0.25f);
        collider = bool.Parse(PlayerPrefs.GetString("collider"));
        ColliderToggle.isOn = collider;
        
        // Settings init
        particle_slider.value = PlayerPrefs.GetInt("particle_num");
        initial_vel_slider.value = PlayerPrefs.GetInt("initial_vel");
        G_slider.value = PlayerPrefs.GetFloat("G_c");
        init_vel = (int) initial_vel_slider.value;
        particle_num = (int)particle_slider.value;
        particles = new Rigidbody2D[particle_num];
        forces = new Vector2[particle_num];
        gravitational_constant = 0;
        


        // Particles init
        for (int i = 0; i < particle_num; i++)
        {
            GameObject clone;
            clone = Instantiate(circleprefab);
            clone.transform.position = GenerateRandomVector(Random.Range(0, 20f));
            particles[i] = clone.GetComponent<Rigidbody2D>();
            mass_sum += particles[i].mass;
            clone.GetComponent<Collider2D>().enabled = collider;
            
        }

        
        particle_slider_label.text = "par_num = " + particle_num;
        initial_vel_slider_label.text = "init_vel = " + init_vel;
        circleprefab.GetComponent<Renderer>().enabled = false;

        gravitational_constant = G_slider.value;
        G_label.text = "G = " + gravitational_constant;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        tick = initDelay(tick);
       
        for (int i = 0; i < particle_num; i++)
        {
            for (int j = i+1; j < particle_num; j++)
            {                  
                forces[i] += GravitationalPull(particles[i], particles[j], j);          
            }

            particles[i].AddForce(forces[i], ForceMode2D.Force);
            forces[i] = new Vector2(0, 0);

            if (CM)
            {
                mass_center_x_numerator += particles[i].mass * particles[i].transform.position.x;
                mass_center_y_numerator += particles[i].mass * particles[i].transform.position.y;
            }

        }


        if (CM)
        {
            mass_center_x = mass_center_x_numerator / mass_sum;
            mass_center_y = mass_center_y_numerator / mass_sum;
            this.transform.position = new Vector3(mass_center_x, mass_center_y, -10);
            mass_center_x_numerator = 0;
            mass_center_y_numerator = 0;
        }
    }

    Vector2 GravitationalPull(Rigidbody2D primary_object, Rigidbody2D secondary_object,int j)
    {
        distance = Vector2.Distance(primary_object.transform.position, secondary_object.transform.position);

        if (distance > 1)
        {
            Force = (gravitational_constant*primary_object.mass*secondary_object.mass) / Mathf.Pow(distance, 3);
            force_vector = Force  * new Vector2(secondary_object.position.x - primary_object.position.x,
            secondary_object.position.y - primary_object.position.y);
            forces[j] += -1*force_vector;
        }

        else
        {
            force_vector = new Vector2(0, 0);
        }

        return force_vector;
    }

    public void AdjustG(float value)
    {
        gravitational_constant = value;
        G_label.text = "G = " + gravitational_constant;
    }

    public void Reset()
    {
        int temp_particle_slider_value = (int) particle_slider.value;
        int temp_vel_slider_value = (int)initial_vel_slider.value;

        PlayerPrefs.SetInt("particle_num", temp_particle_slider_value);
        PlayerPrefs.SetInt("initial_vel", temp_vel_slider_value);
        PlayerPrefs.SetFloat("G_c", gravitational_constant);
        PlayerPrefs.SetString("CM", CM.ToString());
        PlayerPrefs.SetFloat("zoom", zoom);
        PlayerPrefs.SetString("collider", collider.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    
    }

    public static Vector2 GenerateRandomVector(float modulus)
    {
        float arg = Random.Range(0.0f, Mathf.PI * 2);
        return new Vector2(modulus * Mathf.Cos(arg), modulus * Mathf.Sin(arg));
    }

    public void particle_num_change(float value)
    {
        particle_slider_label.text = "par_num = " + value;
    }

    public void init_vel_change(float value)
    {
        initial_vel_slider_label.text = "init_vel = " + value;
    }

    public void center_CM(bool logic)
    {
        CM = logic;
    }

    public void ZoomAdjust(int value)
    {
        zoom = 0.75f + 0.25f * value;
        gameObject.GetComponent<Camera>().orthographicSize = 17*zoom;
    }

    public void OnHideToggle(bool logic)
    {
        group.interactable = logic;

        if (logic)
        {
            group.alpha = 1;
        }
        else
        {
            group.alpha = 0;
        }
    }

    public void OnCollisionToggle(bool logic)
    {
        collider = logic;  
    }

    int initDelay(int tick)
    {
        if (tick < 5)
        {
            tick += 1;
            gravitational_constant = 0;
        }
        else
        {
            gravitational_constant = G_slider.value;
        }

        return tick;
    }
}
