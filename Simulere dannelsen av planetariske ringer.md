En computational essay av Yudhishtiran Vajeeston for Elvebakken videregående skole. 

### Introduksjon: Planetariske ringer?

![[Saturn.jpg]] **Bilde 1:** Nyeste bilde tatt av Saturn fra NASAs Hubble-romteleskop ([NASA](https://solarsystem.nasa.gov/resources/2490/saturns-rings-shine-in-hubble-portrait/?category=planets_saturn))



Antagelser for simuleringen: 
- Alle partiklene har masse 1
- Fysikken er tilpasset til to dimensjoner



### Roche grensen

### Gravitasjonskrefter 
Newtons gravitasjonslov $F_g = \gamma \frac{m_{1}m_2}{r^2}$ gjelder for en tredimensjonal verden. Ettersom denne essayen skal simulere galakser i en todimensjonal verden, må denne formen av gravitasjonsloven endres for å tilpasse behovet. 

Følgelig blir den aktuelle gravitasjonsloven følgende: $F_g=\gamma \frac{m_{1}m_2}{r} = \gamma \frac{1}{r}$ .

Vi lar massen forbli det samme for alle partiklene i systemet og lar $\lbrace{ \vec{p_1}, \vec{p_2}, ... , \vec{p_n}\rbrace}$ inneholde alle possisjonsvektorene og $\lbrace{ \vec{v_1}, \vec{v_2}, ... , \vec{v_n}\rbrace}$ alle fartsvektorene til partiklene. Da får vi følgende når vi anvender Newtons todimensjonal gravitasjonslov for alle partiklene. 
$$
\Sigma\vec{F_k} = \sum_{i=1}^{n}\gamma \frac{m^2 (\vec{p_i}-\vec{p_k})}{\left | \vec{p_i}-\vec{p_k} \right |^2}=\gamma m^2  \sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}=\gamma\sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}$$

Hvor $\vec{F_k}$ er kraftsumvektoren og $p_k$ er posisjonsvektoren til enhver partikkel, mens  $\vec{p_e} =\frac{\vec{p_i}-\vec{p_k}}{\left | \vec{p_i}-\vec{p_k} \right |}$ er en enhetsvektor. Ved å kombinere formlene ovenfor med newtons 2.lov, får vi uttrykk for  $\vec{a}$.  
$$\vec{a_k}=\gamma\sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}$$
Ved å videre implementere dette får vi resultatet nedenfor. Fargen er en indikator på farten. Rødt betyr lav fart mens lyseblått betyr høy fart.

<p align="center">
<img src="https://media.giphy.com/media/bzAM8XaelRrofNLlES/giphy.gif" width="368.5" height="200.5" />
</p>


``` HLSL
float2 forceVector = {0,0};
    for (int i = 0; i < particleAmount; i++)
    {
        float r = distance(positions[id.x], positions[i]);
        if (r > 1)
        {
           forceVector += (positions[i] - positions[id.x])/ (r*r);
        }
    }

    velocity[id.x] += (forceVector*G)* dt;
```


### Kollisjoner 
Vi fokuserer på to vilkårlige partikler A og B. Når avstanden mellom disse er mindre eller lik summen av radiiene til partiklene vil de kollidere. Da setter vi opp et koordinatsystem som tar utgangspunkt i kollisjonspunktet. Se bildet nedenfor: 
![[kollisjon.png]]
**Figur 1:** Her er $\vec{n}$ og $\vec{t}$ vinkelrett på hverandre. Vektorene representerer henholdsvis normalvektoren og tangentvektoren. Fartsvektorene til partikkel A og B kan dekomponeres ut ifra disse vektorene. 

Vi ser at $\vec{n} = \frac{\vec{r}}{r}$ (siden normalvektoren er parallell med $\vec{r}$). Videre kan vi regne oss fram til at $\vec{t}=(-r_y, r_x)$ med fundemental vektorregning. Med disse enhetsvektorene finner vi utrykk for :


$$\begin{align*}
v_{0,A,n} = \vec{n} \cdot \vec{v}_{0,A}&, \quad v_{0,B,n} = \vec{n} \cdot \vec{v}_{0,B}

\\

v_{0,A,t} = \vec{t} \cdot \vec{v}_{0,A}&, \quad v_{0,B,t} = \vec{t} \cdot \vec{v}_{0,B}
\end{align*}$$

Viktige formler:
$$\begin{align*}
v_{A,0,t}&= v_{A,1,t}
\\
v_{B,0,t}&= v_{B,1,t}
\end{align*}$$

$$v_{A,1,n}= \frac{C_r(v_{B,0,n}-v_{A,0,n})+v_{A,0,n}+v_{B,0,n}}{2}$$

$$
\vec{v}_{1,A} = \vec{n} \cdot \vec{v}_{0,A,n} + \vec{t} \cdot \vec{v}_{0,A,t}, \quad \vec{v}_{1,A} = \vec{n} \cdot \vec{v}_{0,A,n} + \vec{t} \cdot \vec{v}_{0,A,t}
$$


```HLSL
for (int j = 0; j < particleAmount; j++)
    {
        float r = distance(positions[id.x], positions[j]);

        if (r < 1 && r != 0)
        {
            positions[id.x] = positions[j] + normalize(positions[id.x]-positions[j])*r;
            velocity[id.x] = velocity[id.x] - (positions[id.x]-positions[j])*(dot(velocity[id.x]-velocity[j], positions[id.x]-positions[j]))/(r*r);
        }
    }
```








### Konklusjon

