# PEC 2 - First Person Shooter

## Cómo jugar
Al iniciar el juego, después de seleccionar "Play" en el menú, el jugador se encuentra en un espacio montañoso rodeado de lava. En este primer nivel, deberá moverse por el espacio para llegar a las instalaciones del segundo nivel, protegidas por drones. Para protegerse de los drones, lleva dos tipos de arma, una de más largo alcance y más daño y otra de corto alcance y más lenta. Además, por el espacio encontrará tanto balas para estas armas como kits para recuperar vida y reparar su escudo.

Cuando llegue a las instalaciones y atraviese la puerta, sin necesidad de llave, pasará al segundo nivel. Este se desarrolla dentro de las instalaciones y en él, además de encontrarse con más drones, el jugador deberá encontrar las llaves necesarias para pasar de una zona del espacio a otra. Cada puerta tiene una indicación de color que define qué llave tiene que haber encontrado el jugador para poder pasar por ella.

Para controlar al personaje, se tendrán que utilizar las teclas WASD para el movimiento y el espacio para el salto. Por otro lado, con la tecla E se podrá cambiar de arma y haciendo clic izquierdo con el ratón, se disparará.

## Estructura e Implementación
El juego se divide en tres escenas, el menú y los dos niveles.

Desde el menú, el jugador solo tiene la opción de jugar o de salir. Para estas funcionalidades, tenemos el script MenuManager, que se encargará de cualquier cambio de escena que haya que realizar en el juego, y que se mantendrá entre escenas al ser hijo del GameManager. Este, por su parte, sirve de conector entre varios elementos del juego y se encarga de activar o desactivar menús de UI cuando sea necesario.

En los niveles, podemos encontrar varios elementos a comentar: los enemigos, los items, las puertas y las funcionalidades del personaje.

Los enemigos funcionan mediante una máquina de estados definida por una interfaz que determina los métodos de cada estado, tres estados distintos - patrulla, alerta y ataque - y un controlador. Cada estado, se encarga de determinar qué debe hacer el enemigo, siendo relevantes los métodos UpdateState y OnTriggerEnter/Stay/Exit, que definirán cambios de estado y acciones a realizar. El enemigo patrullará por su ruta hasta que el jugador entre en su zona marcada por un trigger. En ese momento, pasará al estado de alerta, en el que dará una vuelta sobre si mismo. Si ve al jugador, pasará al modo de ataque, en el que, como su nombre indica, atacará al jugador hasta que salga de su línea de visión. Si no ve al jugador, volverá al estado de patrulla. Además, si el jugador lo ataca, pasará también al estado de ataque. El EnemyAIController será el encargado de hacer los cálculos y las acciones utilizadas por cada uno de los estados, además de almacenar varias características editables desde el editor que nos permitirán darles un poco de variedad a los enemigos, controlando, por ejemplo, el tiempo entre disparos, la velocidad de giro o la cantidad de daño que hace añ jugador. En la muerte del enemigo, controlada por el EnemyAIController, hacemos un Fade algo dificil de apreciar por las texturas del asset wue estamos usando. Es por esto, que además del fade vamos cambiando el color del material en función de la cantidad de vida que tiene el enemigo. Además, cuando la vida llega a 0, hacemos que el enemigo pueda dejar caer objetos del tipo HealthPack o del tipo ShieldRepairKit con una probabilidad del 70%.

Por su parte, los items heredarán todos de la clase Item. Esta contiene un método común para todos, OnTriggerEnter, y otro abstracto que cada hijo implementa como necesita, PickUp. El primero detecta cuando el jugador toca el objeto y se encarga de activar el sonido asociado al item, destruir el objeto y llamar al método PickUp. En el juego tenemos cuatro tipos de item que realizarán sus propias acciones en el método PickUp: las llaves, que guardarán el tipo de llave en la clase KeyHolder; los kits de vida, HealthPack, que aumentarán la vida del jugador; los kits de reparación, ShieldRepairKit, que aumentarán el escudo del jugador; y la munición, Munition, que aumentará el número de balas del tipo concreto asociado al item que tiene el jugador.

El control de los items va ligado a la funcionalidad adicional del jugador. A este objeto, le hemos añadido varias clases nuevas: KeyHolder y Health. KeyHolder se encargará tanto de guardar las llaves recogidas, mediante una lista de DoorTypes, un enum creado para definir los distintos tipos de llaves y puertas; como para saber si la puerta se tiene que abrir o no, comprobando si el tipo de puerta a la que se quiere acceder coincide con alguna de las llaves recogidas. Health, por su parte, controlará tanto la vida como el valor de escudo del jugador, teniendo una variable que define qué porcentaje de daño recibirá el escudo cuando siga activo. En la muerte del jugador, además, bloqueamos el juego.

Siguiendo con el jugador, este objeto tendrá como hijo los dos tipos de armas. Estas serán las que se encargarán de los disparos, gracias a la clase Gun. Esta se encarga de detectar cuando el jugador puede disparar y a qué dispara. Instanciará un GameObject en forma de agujero de bala en el collider contra el que choque, siempre que este no sea un enemigo. En caso de disparar al enemigo, se le aplicará una cantidad de daño definida en este mismo objeto. Además, del mismo modo que en los enemigos, se podrá definir el tiempo entre disparos y el daño que hacen para añadir variedad en funciónd el tipo de arma. Por otro lado, para controlar los cambios de arma hemos añadido un script GunHolder en el jugador, de modo que cuando se pulse la tecla E se active la siguiente arma contenida en un array y se desactive la que estaba en uso.

Por otro lado, tenemos varios objetos simples que se encargan de detectar al personaje, con un trigger, para realizar distintas acciones. Estos son la Lava, que herirá al jugador suficientemente para matarlo; el LevelChangeDetector, que activará un evento al que el MenuManager está escuchando y que definirá a qué nivel hay que pasar; Door, que comprobará si el jugador tiene permiso para abrir la puerta asociada a él, ya sea porque no necesita ninguna llave o porque ya se encuentra en el KeyHolder, y activará la animación de apertura o cierre de puerta; y GameWinner, que detectará cuando el jugador ha llegado al final del juego y mediante un evento avisará al GameManager de ello.

Tenemos, también, una clase MovingPlatform que seguirá un patrón similar al de la patrulla de los enemigos. Es decir, tiene un conjunto de puntos y se mueve en cada iteración hacia el siguiente en la lista, mediante una interpolación. Si llegamos al punto de destino, pasamos al siguiente.

Finalmente, tenemos varios métodos para controlar los elementos de UI y mostrar la munición, la vida, el escudo y el tipo de arma en uso. Estas clases, UIMunition, UIEnemyHealth y UIBarIndicator, mediante el uso de eventos, se encargan de actualizar los datos. Además, en el centro de la pantalla mostramos un indicador de disparo cuando el arma detecta un collider contra el que chocar, dando feedback al jugador sobre cuando puede disparar y a qué punto está apuntando.

## Problemas conocidos
El escenario montañoso es muy amplio para la cantidad de acción que se encuentra en él.

En el segundo nivel, el terreno deja de tener sentido por no haber podido hacer que se vea como si el jugador estuviera bajo la lava. Esta lava, además, da problemas al ser un plano que atraviesa los pasillos del nivel.

Al cambiar de nivel, no se mantienen el número balas y la vida y el escudo del jugador.

Las plataformas móviles no tienen control de si el jugador está sobre ellos o no, por lo que es casi imposible mantenerse sobre ellas todo el trayecto.

Los items que pueden caer del enemigo a veces fallan en la detección del jugador al intentar cogerlos.

## Vídeo
Este es el [enlace](https://youtu.be/NsWpYmAoHqc) al vídeo de la PEC.
