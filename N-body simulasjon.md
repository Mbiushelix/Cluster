# Hvordan simulere galakser?
En computational essay av Yudhishtiran Vajeeston for Elvebakken videregående skole. 

### Introduksjon: Hva er galakser?

![[Pasted image 20221118184426.png]] **Bilde 1:** Bildet av en stavspiralgalaksen NGC2336 tatt av NASA/ESA Hubble Space Telescope ([NASA](https://www.nasa.gov/image-feature/goddard/2021/hubble-beholds-a-big-beautiful-blue-galaxy))



### Utregninger for gravitasjonelle krefter 
Newtons gravitasjonslov $F_g = \gamma \frac{m_{1}m_2}{r^2}$ gjelder for en tredimensjonal verden. Ettersom denne essayen skal simulere galakser i en todimensjonal verden, må denne formen av gravitasjonsloven endres for å tilpasse behovet. 

Følgelig blir den aktuelle gravitasjonsloven følgende: $F_g=\gamma \frac{m_{1}m_2}{r^2} = \gamma \frac{1}{r^2}$ (husk at alle partiklene har 1 som masse)

Vi lar massen forbli det samme for alle partiklene i systemet og lar $\lbrace{ \vec{p_1}, \vec{p_2}, ... , \vec{p_n}\rbrace}$ inneholde alle possisjonsvektorene og $\lbrace{ \vec{v_1}, \vec{v_2}, ... , \vec{v_n}\rbrace}$ alle fartsvektorene til partiklene. Da får vi følgende når vi anvender Newtons todimensjonal gravitasjonslov for alle partiklene. 
$$
\Sigma\vec{F_k} = \sum_{i=1}^{n}\gamma \frac{m^2 (\vec{p_i}-\vec{p_k})}{\left | \vec{p_i}-\vec{p_k} \right |^2}=\gamma m^2  \sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}=\gamma\sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}$$

Hvor $\vec{F_k}$ er kraftsumvektoren og $p_k$ er posisjonsvektoren til enhver partikkel, mens  $\vec{p_e} =\frac{\vec{p_i}-\vec{p_k}}{\left | \vec{p_i}-\vec{p_k} \right |}$ er enhetsvektoren til ... . Ved å kombinere formlene ovenfor med newtons 2.lov får vi uttrykk for  $\vec{a}$.  
$$\vec{a_k}=\gamma\sum_{i=1}^{n}\frac{ \vec{p_e}}{\left | \vec{p_i}-\vec{p_k} \right |}$$
Ved å implementere dette får vi resultatet nedenfor. Fargen er en indikator på farten. Rødt betyr lav fart mens lyseblått betyr høy fart.

<p align="center">
<img src="https://media.giphy.com/media/bzAM8XaelRrofNLlES/giphy.gif" width="368.5" height="200.5" />
</p>



### Kollisjoner 




### Mørk energi 


### Mørk materie 


### Optimalisering av programmet 









### Konklusjon

