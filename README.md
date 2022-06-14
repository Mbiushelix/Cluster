# Cluster
The most central formula in this project is:
<p align="center">
<img src="https://latex.codecogs.com/svg.image?{\color{Emerald}F_G&space;=&space;G&space;\cdot&space;\frac{m_1\cdot&space;m_2}{r^2}}" title="https://latex.codecogs.com/svg.image?{\color{Emerald}F_G = G \cdot \frac{m_1\cdot m_2}{r^2}}" />
</p>
... which states that the gravitational force between two object depend on the masses of the object and is inverse proportional with the distance between the mass centers (CM).

### The first algorithm (inefficient): 
<p align="center">
<img src="https://latex.codecogs.com/svg.image?{\color{Emerald}\vec{F_i}=\sum_{j=1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}}&space;\thinspace&space;,&space;\quad&space;i\in&space;\{1,2,3,\ldots,n&space;\}&space;}" title="https://latex.codecogs.com/svg.image?{\color{Emerald}\vec{F_i}=\sum_{j=1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}} \thinspace , \quad i\in \{1,2,3,\ldots,n \} }" />
</p>
According to Newton’s law of gravitation, the gravitational force between two objects will be the same. However, the direction of the forces will of course be opposite. As a result, this algorithm would therefore do almost doubled the amount of calculations that are strictly necessary.


In C# with Unity: 


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

### The second algorithm (much more efficient):

<p align="center">
<img src="https://latex.codecogs.com/svg.image?{\color{Emerald}F_i&space;=&space;\begin{cases}\sum_{1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}}&space;,&&space;i&space;=&space;1&space;\\&space;\sum_{j=i&plus;1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}}&space;-&space;\sum_{k=1}^{i-1}F_i&space;\thinspace&space;,&space;&&space;i\geq&space;2&space;\\-\sum_{k=1}^{i-1}F_i&space;\thinspace&space;,&space;&&space;i=n\end{cases}}" title="https://latex.codecogs.com/svg.image?{\color{Emerald}F_i = \begin{cases}\sum_{1}^n\frac{Gm_im_j}{\lvert\vec{r}\rvert^3}\vec{r_{ij}} ,& i = 1 \\ \sum_{j=i+1}^n\frac{Gm_im_j}{\lvert\vec{r}\rvert^3}\vec{r_{ij}} - \sum_{k=1}^{i-1}F_i \thinspace , & i\geq 2 \\-\sum_{k=1}^{i-1}F_i \thinspace , & i=n\end{cases}}" />
</p>

This algorithm takes into account that Newton’s formula for the gravitational force infers to the force-vector of both objects. This reduces the number of calculations that are performed. See the formula over or the code below for more information.

```C#
for (int i = 0; i < particle_num; i++)
        {
            for (int j = i+1; j < particle_num; j++)
            {                  
                forces[i] += GravitationalPull(particles[i], particles[j], j);          
            }

            particles[i].AddForce(forces[i], ForceMode2D.Force);
            forces[i] = new Vector2(0, 0);
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
```

### How much better you may ask.

Well, let ![equation](https://latex.codecogs.com/svg.image?{\color{Emerald}E_f(n)}) be an estimate of calculation reduction. 
<p align="center">
<img src="https://latex.codecogs.com/svg.image?{\color{Emerald}E_f(n)&space;=&space;\frac{\binom{n}{2}}{n^2}&space;=&space;\frac{n-1}{n^2}}" title="https://latex.codecogs.com/svg.image?{\color{Emerald}E_f(n) = \frac{\binom{n}{2}}{n^2} = \frac{n-1}{n^2}}" />
</p>
That means that ...

![Cluster algorithms (1)](https://user-images.githubusercontent.com/81691774/173643502-d04ecb5e-cec5-419d-9bf7-0b3536edc1c4.png)
