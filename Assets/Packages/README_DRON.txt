===========================
DRON
===========================

EL Dron és el prefab del modelo, más el prefab del Quad que muestra la vida "Player_life"
Player_life es hijo del Dron.

Player_life lleva el script "look at" que como target tendrá la MainCamera, i también el script"DRON"
En el script "DRON" hay un vector que debe ser de 16 posiciones i se tiene que llenar con las texturas de la vida del player, empezando por la textura 0_life hasta la 15_...


Por otro lado el prefab del dron lleva el script "DronFollow" que como target tendrá al player
Finalmente hay que poner la layer de "Ignoreraycast" en el dron para evitar futuros problemas