# Cluster

$$F_G = G \cdot \frac{m_1\cdot m_2}{r^2}$$

#### The first algorithm (inefficient): 
$$\vec{F_i}=\sum_{j=1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}} \thinspace , \quad i\in \{1,2,3,\ldots,n \}$$
The gravitational force between two object will according to Newtons' law be the same. However the direction of the force will of cource be opposite. This algorithm will therefore do almost the double amount of the calulations that are actually needed.

In C# with Unity: 

#### The second algorithm (much more efficient):

$$
\begin{cases}
\sum_{1}^n\frac{GMm_i}{\lvert\vec{r}\rvert^3}\vec{r} , & i = 1 \\ 
\sum_{j=i+1}^n\frac{GMm_i}{\lvert\vec{r}\rvert^3}\vec{r} - \sum_{k=1}^{i-1}F_i \thinspace , & i\geq 2 \\
-\sum_{k=1}^{i-1}F_i \thinspace , & i=n
\end{cases}
$$

```C#
for (int i = 0; i < particle_num; i++)
        {
            for (int j = 0; j < particle_num; j++)
            {
                if (i != j)
                {
                    delta_force += GravitationalPull(particles[i], particles[j]);
                }
            }
            
            particles[i].AddForce(delta_force, ForceMode2D.Force);
            delta_force = new Vector2(0, 0);
        }

Vector2 GravitationalPull(Rigidbody2D primary_object, Rigidbody2D secondary_object)
    {
        distance = Vector2.Distance(primary_object.transform.position, secondary_object.transform.position);

        if (distance > 1)
        {
            Force = gravitational_constant / Mathf.Pow(distance, 3);
            force_vector = Force * new Vector2(secondary_object.position.x - primary_object.position.x,
            secondary_object.position.y - primary_object.position.y);
        }

        else
        {
            force_vector = new Vector2(0, 0);
        }
        
        return force_vector;
    }
```
