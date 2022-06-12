int[,] carton = new int[3, 9];  // Matriz en donde se guardan los números del cartón
int aux, auxFil, auxCol;
string auxString;
Random numeroRandom = new Random();

int cont = 0; // Contador para saber cuantas iteraciones se hicieron hasta conseguir un cartón valido
bool respuesta = true;
while (respuesta)
{
    cont++; // Contador de iteraciones para generar un cartón
    
    // Generamos 27 números para el carton.    
    int a = -9;// Inicializamos las variables 'a' y 'b' para poder tener un rango en lo números aleatorios de 'numeroRandom.Next(a,b)'. 
    int b = 0;
    for (int col = 0; col < 9; col++)
    {
        a += 10; // A medida que pasamos a otra columna en el cartón, subimos de 10 en 10 los valores de 'a' y 'b'.
        b += 10;
        for (int fil = 0; fil < 3; fil++)
        {
            if (a == 11) a = 10; // En la segunda vuelta del bucle, cuando 'a' pasa a valer 11, tenemos que inicializarlo en 10 para poder sumar de decena en decena los siguientes números
            if (b == 90) b = 91; // Cuando estamos llenando la ultima columna, a la variable 'b' la igualamos a 91, para poder tener disponible el número 90
            aux = numeroRandom.Next(a, b);
            // Bucle para verificar números repetidos
            for (int i = 0; i < 3; i++)
            {
                if (aux == carton[i, col])// Si el número esta repetido, se resta una posición de la fila y se sale del bucle para comenzar de nuevo.
                {
                    fil--;
                    break;
                }
                if (i == 2) carton[fil, col] = aux; // Si llegamos al final del bucle, es porque no se encontró un numero repetido, y lo asignamos a la matrix.
            }
        }
    }

    // Ordenamos la matriz 'carton'
    for (int col = 0; col < 9; col++)
    {
        for (int fil = 0; fil < 2; fil++)
        {
            for (int k = fil + 1; k < 3; k++)
            {
                if (carton[fil, col] > carton[k, col])
                {
                    aux = carton[fil, col];
                    carton[fil, col] = carton[k, col];
                    carton[k, col] = aux;
                }
            }
        }
    }

    // Eliminamos 15 números al azar
    for (int i = 0; i < 12; i++)
    {
        auxFil = numeroRandom.Next(0, 3);
        auxCol = numeroRandom.Next(0, 9);
        if (carton[auxFil, auxCol] != 0) carton[auxFil, auxCol] = 0;
        else i--;
    }

    respuesta = false;
    // Verificamos que las columnas tengan uno a dos esapcios UNICAMENTE.
    for (int col = 0; col < 9; col++)
    {
        int sumaCol = 0;
        for (int fil = 0; fil < 3; fil++)
        {
            if (carton[fil, col] == 0) sumaCol++;
        }
        if (sumaCol < 1 || sumaCol > 2) // Si la columna no cumple con la condición, se sale del bucle. 
        {
            respuesta = true;
            break;
        }
    }

    // Verificamos que las filas tengas 4 espacios(ceros). En la verificacion de filas entramos unicamente si la verificacion de columnas fue correcta
    if (respuesta == false)
    {
        for (int fil = 0; fil < 3; fil++)
        {
            int sumaFil = 0;

            for (int col = 0; col < 9; col++)
            {
                if (carton[fil, col] == 0) sumaFil++;
            }
            if (sumaFil != 4) // Si la fila no cumple con la condición, se sale del bucle y se comienza a generar un cartón nuevo.
            {
                respuesta = true;
                break;
            }
        }
    }

    //¡Impresion por consola
    if (respuesta == false)
    {
        Console.WriteLine("================================================");
        for (int fil1 = 0; fil1 < 3; fil1++)
        {
            for (int col1 = 0; col1 < 9; col1++) // Imprimimos la fila del cartón
            {
                auxString = carton[fil1, col1].ToString();
                if (col1 == 0) Console.Write("||");
                if (auxString == "0") Console.Write($" {Convert.ToChar(6)}{Convert.ToChar(6)} |");
                if (auxString.Length == 1 & auxString != "0") Console.Write($" 0{auxString} |");
                if (auxString.Length == 2) Console.Write($" {auxString} |");
                if (col1 == 8) Console.Write("|");
            }
            Console.WriteLine();
        }
        Console.WriteLine("================================================");
        Console.WriteLine($"Iteraciones para conseguir el cartón: {cont}");
        Console.WriteLine("Quiere imprimir otro cartón? (s/n)");
        auxString = Console.ReadLine();
        while (auxString.ToUpper() != "S" & auxString.ToUpper() != "N")
        {
            Console.WriteLine("¡Ha ingresado una opción inválida! Quiere imprimir otro cartón? (s/n)");
            auxString = Console.ReadLine();
        }
        if (auxString.ToUpper() == "S") respuesta = true;
    }
}