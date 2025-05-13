using System;

class programa
{
    static void Main(string [] args)
    {
        Saludar();

        int resultado = restar(5, 2);
        Console.WriteLine("Resultado de la resta: " + restar);
    }

    static void Saludar()
    {
        Console.WriteLine("¡Hola Valentina!");
    }

    static int restar(int a, int b)
    {
        return a - b;
    }
}