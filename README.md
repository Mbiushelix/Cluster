# Cluster

$$F_G = G \cdot \frac{m_1\cdot m_2}{r^2}$$

#### The first algorithm (inefficient): 
$$\vec{F_i}=\sum_{j=1}^n\frac{Gm_im_j}{\lvert\vec{r_{ij}}\rvert^3}\vec{r_{ij}} \thinspace , \quad i\in \{1,2,3,\ldots,n \}$$
The gravitational force between two object will according to Newtons' law be the same. However the direction of the force will of cource be opposite. This algorithm will therefore do almost the double amount of the calulations that are actually needed.

#### The second algorithm (much more efficient):

$$
\begin{cases}
\sum_{1}^n\frac{GMm_i}{\lvert\vec{r}\rvert^3}\vec{r} , & i = 1 \\ 
\sum_{j=i+1}^n\frac{GMm_i}{\lvert\vec{r}\rvert^3}\vec{r} - \sum_{k=1}^{i-1}F_i \thinspace , & i\geq 2 \\
-\sum_{k=1}^{i-1}F_i \thinspace , & i=n
\end{cases}
$$

