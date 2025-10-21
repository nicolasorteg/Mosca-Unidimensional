using Math;

// constantes para controlar el menu, utiles para mejorar el mantenimiento y la legibilidad del código
const OPCION_MENU_MOSCA = 1;
const OPCION_MENU_INSTRUCCIONES = 2;
const OPCION_MENU_SALIR = 3;

const TAMAÑO_PANEL = 20;

Main {


    int[] panelJuego = int[20]; // array vacío de 20 huecos

    writeLine("Bienvenido al Juego: ¿Dónde está la mosca?");

    ejecutarSimulacion(panelJuego);

}


procedure ejecutarSimulacion(int[] panelJuego) {

    int opcionElegida = 0; // inicializamos a 0 para asegurar la primera entrada al bucle

    do {
        writeLine("-------------");
        writeLine(OPCION_MENU_MOSCA + ".- Jugar a la mosca.");
        writeLine(OPCION_MENU_INSTRUCCIONES + ".- Mostrar instrucciones.");
        writeLine(OPCION_MENU_SALIR + ".- Salir");    

        opcionElegida = leerEntero("Opción elegida: ");

        switch (opcionElegida) {

            case OPCION_MENU_MOSCA:

                simularJuegoMosca(panelJuego);
                break;

            case OPCION_MENU_INSTRUCCIONES:

                mostrarInstrucciones();
                break;

            case OPCION_MENU_SALIR:

                writeLine("Saliendo del programa... 😔");
                break;

            default:

                writeLine("❌ Opción introducida no válida. Por favor, introduce una opción de las " + OPCION_MENU_SALIR + " posibles."); 
                break; 
        }

    } while (opcionElegida != OPCION_MENU_SALIR);
}



procedure simularJuegoMosca(int[] panelJuego) {

    bool isMoscaMuerta = false;

    panelJuego = generarPosicionMosca(panelJuego);

    do {
        writeLine("-------------");
        writeLine("PANEL DE JUEGO");
        writeLine("-------------");
        writeLine(" 1    2    3   4    5    6    7   8    9   10   11   12   13  14   15   16   17  18   19   20 ");
        writeLine("[❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓] [❓]");

        int posicionElegida = leerEntero("Posición para golpear: "); 

        if (posicionElegida > 20 || (posicionElegida <= 0)) {

            writeLine("❌ Opción introducida no válida. Por favor, introduzca una posición de las " + TAMAÑO_PANEL + " disponibles.");

        } else {
            comprobarGolpeo(posicionElegida, panelJuego, ref isMoscaMuerta);
        }

    } while (!isMoscaMuerta);
   
}


procedure comprobarGolpeo(int posicionElegida, int[] panelJuego, ref bool isMoscaMuerta) {

    posicionElegida -= 1; // se pasa la posicion a indice

    if (panelJuego[posicionElegida] == 1) { // si para el indice facilitado

        writeLine("ENHORABUENA!! Has golpeado de lleno a la mosca sin que esta pudiese huir.");
        isMoscaMuerta = true;

    } else if (((panelJuego[posicionElegida - 1] == 1) && (posicionElegida > 0)) || ((panelJuego[posicionElegida + 1] == 1) && (posicionElegida < TAMAÑO_PANEL - 1))) {

        writeLine("CASI!! Has golpeado al lado de la mosca y esta ha revoloteado, cambiando así su posición.");
        panelJuego = generarPosicionMosca(panelJuego);

    } else {

        writeLine("FALLO!! Has golpeado en una casilla vacía. En la posición " + (posicionElegida + 1) + " no había nada");
    }
}

function generarPosicionMosca(int[] panelJuego) {

    bool flag = false;
    var mosca = 1;

    for (int i = 0; i < TAMAÑO_PANEL; i += 1) {

        if (panelJuego[i] == 1) {

            panelJuego[i] = 0;

            int posicionMosca = Math.random(0, 19); // el panel tiene 20 espacios
            panelJuego[posicionMosca] = mosca;

            return panelJuego;

        }
    }
    return panelJuego;
}


function int leerEntero(string mensaje) {

    int valorLeido = 0;
    bool isFormatoCorrecto = false; //flag

    do {
        writeLine(mensaje);
        try {
            valorLeido = (int)readLine(); 
            isFormatoCorrecto = true;
        } catch (Exception e) {
            writeLine("❌ Error de formato. Debe introducir un número entero. Inténtelo de nuevo.");
        }
    } while (!isFormatoCorrecto);

    return valorLeido; // devuelve el valor leido, no lo hace hasta que sea valido
}



procedure mostrarInstrucciones() {

    writeLine("-- ¿Cómo jugar a la mosca 🦟? --");
    writeLine("El tablero consta de una fila de 20 posiciones. La mosca se encuentra en una posición aleatoria de estas 20.");
    writeLine("- Si acierta la posicion de la mosca, gana!");
    writeLine("- Si acierta la posición contigua de la mosca, ya sea la anterior o la siguiente, esta revolotea y cambia su posición");
    writeLine("- Si no acierte y se queda lejos no pasa nada y puede volver a probr suerte sin que la mosca se haya movido");

}