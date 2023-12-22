# GDD Return to the Moon

Revisión: 0.0.2

## Resumen

### Características del juego

**Tema**: La Perseverancia.

**Premisa**: La Perseverancia te guiará hacia la paz.

**Género**: Runner, Sidescroller, Musical.

**Entorno**: Cerro con zonas aisladas de explotación ambiental.
Deberás ser parte del entorno.

**Gameplay Núcleo (Breve)**:
- Presionar (Input) genera comportamientos e interacciones según el contexto.

### Construcción del protagonista

¿Cómo generamos empatía por nuestro protagonista?
- **Mala suerte.** Nuestro protagronista tiene la mala suerte de ser vulnerable a ataques por otros animales hostiles de la zona en la que se encuentra.
- **Malos tratos.** Nuestro protagonista es rechazado por su propia especie al perder su sello identificador.
- **Vulnerable.** Nuestro protagonista pierde sus dotes únicos, lo que lo deja indefenso en un ambiente hostil.
- **Abandonado.** Nuestro protagonista es abandonado por su especie y se encuentra solo en un lugar desconocido.
- **Traición.** Si bien sus compañeros intentan ayudarle, lo rechazan solo por verse diferente, lo que se siente como una traición.
- **Posee una verdad, pero no es oído.** Nuestro protagonista sabe lo que es y cree en sus capacidades, sin embargo lo dan por muerto y rechazan ayudarlo.
  

¿Cómo generamos interés por nuestro protagonista?
- **Objetivo claro.** Volver a la luna.
- **Urgencia dramática.** Si no vuelve a tiempo, morirá.
- **No puede conseguir su objetivo fácilmente.** Debido a su discapicidad física

### Contrucción del videojuego

***Mechanics --> Dynamics --> Aesthetics***

**(M) Reglas establecidas**: 
- Caminas siempre hacia delante, pero esto NO es suficiente, deberás presionar (Input) para moverte un poco más rápido durante breves segundos, así podrás conseguir tu objetivo.
- Puedes presionar (Input) en diferentes situaciones, dependiendo de cada contexto recibiras recompensas o penalizaciones relacionadas a tu tiempo de vida.

**(D) Comportamiento esperado**: 
- Al tener un recurso vital pero seguro tras una muralla de esfuerzo seguro, deberás ser paciente y consistente.
- Gestión de recursos vitales; Tu vida está en peligro, tomarás decisiones ambigüas para no morir.

**(A) Experiencia esperada**: 
- Perseverancia.
- Deseo de supervivencia.

**Plataformas:** Android / PC (Por verse)

**Modelo de monetización:** Anuncios a cambio de vidas, 3 vidas por día por defecto.

**Alcance del proyecto:** Fecha ideal de término: 26/01/2024 

**Referentes:** "Kiwis can't fly.", "Planet of Lana", "Geometry 
Dash", "Alto's odyssey/adventure"

**Pitch:** "Este juego representa lo que se siente seguir un camino."

**Elevator Pitch:** 
- Problema: Hay una gran cantidad de videojuegos "Endless Runner" para móviles, donde solamente se recompensa al jugador con elementos prácticos.
- Analgésico: Y aunque juegos que intentan poner un enfoque en estético, siempre reina lo práctico y arcade.
- Solución: Crearemos un videojuego donde el viaje sea lo importante y simule de manera fiel la experiencia de perseguir un objetivo.
- Equipo: Somos un equipo de dos personas que ya tienen experiencia desarrollando videjuegos cortos.
- Recursos solicitados: Feedback, Activos y donaciones.

Project Description (Brief)
Project Description (Detailed)
... (wip)

### Mecánicas de Gameplay Núcleo (Detalle)
- Presiona (Input) para avanzar más rápido (sin posibilidad de mantener).
	- Variaciones:
      - Mantener presionado (Input) mientras estés frente a un arbusto, relentizará a tu personaje y lo esconderá del peligro (sigilo).
      - Quick time events con (Input),
      - Decidir no presionar (Input) gracias a recompensas auxiliares
      - En niveles ESPECÍFICOS Presionar (Input) al ritmo de la música hará que avances más rápido, el no hacerlo te relentizará, esta regla actuará desde principio a fin del nivel sin ninguna otra variación presente.

| Ejemplos de eventos interactivos (Riesgo/Recompensa) | Perder mucho | Perder    | Neutral | Ganar                         | Ganar Mucho  |
|----------------------|--------------|-----------|---------|-------------------------------|--------------|
| Boost QTE            |              |           |         | Vel+1                         | Vel+2 * xSeg |
| Sigilo               | Vel-2        | Vel-1     |         |                               |              |
| Boost default        |              | Stamina-X |         | Vel+1 * xSeg Death*0.7 * xSeg |              |
| Skill QTE            | Vel-2        |           | Vel =   |                               |              |
| Boost Musical        |              | Vel-1     |         | Vel+1                         |              |

**Storyline**

Eres un Ave Lunar mística, apasionada a la cartografía. Un dia caes a la tierra, esto te rompe un ala, lo cual te deja vulnerable a la hostilidad del entorno, tu misión es regresar a tu hogar; La luna, sin embargo ¿Realmente ese es tu objetivo en la vida?.

**Conflicto Matriz**

Durante diciembre del año 20XX, Ave Lunar(?) orbita la Tierra, entra en el campo de gravedad de esta. Al caer se rompe un ala, no puede volver a La Luna. Con la ayuda de su mapa, intenta sobrevivir a los depredadores y clima de la zona. Finalmente salta de un risco y sueña que logra volar, sin embargo la realidad es que muere en el océano.

Gameplay (Brief)
Gameplay (Detailed)
... (wip)

### Alcance del proyecto (Detalle)

**Tamaño del equipo**: 2

Julián Abaroa (Programación, Diseño de videojuegos).

Sara San Martín (Gráficos, Diseño de videojuegos, Audio, Programación)
  
### Referentes (Detalle)
**Kiwis can't fly** *(Cocoa Moss, DDRKirby(ISQ), Kat Jia)*

Nos basamos en su trabajo del *"Mood"*, queremos crear una experiencia enfocada en la parte *Estética*. Dejando de lado mecánicas complicadas.

**Planet of Lana** *(Wishfully Studios)*

Queremos capturar la forma en que Planet of Lana te hace parte de su entorno. El ambiente no está diseñado PARA TI, al contrario, tu debes adaptarte a como funcional las reglas del mundo.

**Geometry Dash** *(Robtop Games)* 

En este frenético juego se encuentran piezas de arte visual muy interesantes, ya que actúan como excelentes recompensas auxiliares. Tu objetivo no es necesariamente completar los niveles, si no simplemente admirar la creación de otra persona, con eso es suficiente para sentarse a jugar.

**Alto's odyssey/adventure** *(Noodlecake Studios)*

La jugabilidad de *"Return to the moon"* es muy similar a la de este videojuego, sin embargo, la principal inspiración viene de sus fondos, queremos capturar la sensación de escala y extensión de su mundo, lugares en los que pierdas la mirada en el horizonte.

### Mecánicas de Gameplay Núcleo (Detalle)

... (wip)

### Activos necesarios.
- 2D
- Datos necesarios

- Lista de personajes
  - Character #1
  - Character #2
  - Character #3
  
- Lista de entornos (Arte)
	- Example #1
    - Example #2 
    - Example #3

- Sonido
  - Lista de sonidos (Ambiente)
    - Level 1
    - Level 2 
    - Level 3
  - Lista de sonidos (Jugador)
  
- Código
  - Controlador de personaje
  - Calculadoras
  - QTES
  - etc..

- Animación
  - Animaciones de entorno 
    - ejemplo 
  - Animaciones de personaje
    - ejemplo

# License 
"Return to the Moon GDD" Originally written by Julián Abaroa and Sara San Martín *(21/12/2023)*

Created with Google Docs GDD Template, Credits to: Alec Markarian, Benjamin Stanley and Brandon Fedie.