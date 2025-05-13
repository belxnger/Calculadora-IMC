using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.InteropServices.Marshalling;
using System.IO;

class Program
{

    static List<ResultadoIMC> historial = new List<ResultadoIMC>();
    static void Main(string[] args)
    {
       bool salir = false;

       while (!salir)
       {
        Console.WriteLine("=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1. Calcular IMC");
        Console.WriteLine("2. Ver Historial");
        Console.WriteLine("3. Salir");
        Console.WriteLine("4. Guardar historial");
        Console.WriteLine("Selecciona una opción (1,2 o 3): ");

        string opcion = Console.ReadLine();
        Console.WriteLine();

        switch (opcion)
        {
            case "1":
                CalcularIMC();
                break;

            case "2":
                MostrarHistorial();
                break;

            case "3":
                salir = true;
                Console.WriteLine("Adiós!");
                break;

            case "4":
                GuardarHistorial();
                break;    

            default:
                Console.WriteLine("Opción no válida. Intenta nuevamente.\n");
                break;
        }
       }
    }

    static void CalcularIMC()
    {
        string nombre = PedirTexto("¿Cuál es su nombre? ");
        double peso = PedirDouble("¿Cuál es su peso en kg? ");
        double altura = PedirDouble("¿Cuál es su altura en metros? ");

        double imc = peso / (altura * altura);
        double imcredondeo = Math.Round(imc, 2);
        string clasificacion = ClasificarIMC(imc);

        Console.WriteLine($"\nHola {nombre}. Tu IMC es: {imcredondeo}");
        Console.WriteLine("Clasificación: " + clasificacion);
            Console.WriteLine();


             historial.Add(new ResultadoIMC
    {
        Nombre = nombre,
        Peso = peso,
        Altura = altura,
        IMC = imcredondeo,
        Clasificacion = clasificacion
    });

    }

    static void MostrarHistorial()
    {
        if (historial.Count == 0)
        {
            Console.WriteLine("No hay calculos registrados todavia.\n");
            return;
        }

        Console.WriteLine("=== HISTORIAL DE CALCULOS ===");

        foreach (var r in historial)
        {
            Console.WriteLine($"Nombre: {r.Nombre} | Peso: {r.Peso} kg | Altura: {r.Altura} m | IMC: {r.IMC} | Clasificación {r.Clasificacion}");
        }

        Console.WriteLine();
    }


    static string PedirTexto (string mensaje)
    {
        Console.Write(mensaje);
        return Console.ReadLine();
    }

    static double PedirDouble(string mensaje)
    {
        double valor;
        bool valido = false;

        do
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine();
            valido = double.TryParse(entrada, NumberStyles.Float, CultureInfo.InvariantCulture, out valor);

            if (!valido)
                Console.WriteLine("Entrada no valida, use punto para los decimales");
        }while (!valido);

        return valor;
    }

    static string ClasificarIMC(double imc)
    {
        if (imc <18.5)
            return "Bajo Peso";
        else if (imc <= 24.9)
            return "Normal";
        else if (imc <= 29.9)
            return "Sobrepeso";
        else
            return "Obesidad";    
    }


    static void GuardarHistorial()
    {
        if (historial.Count == 0)
        {
            Console.WriteLine("No hay datos para guardar.\n");
            return;
        }

        string nombreArchivo = "historial_imc.csv";

        try
        {
            using (var writer = new StreamWriter(nombreArchivo))
            {
                writer.WriteLine("Nombre,Peso,Altura,IMC,Clasificación");

                foreach (var r in historial)
                {
                    writer.WriteLine($"{r.Nombre},{r.Peso.ToString(CultureInfo.InvariantCulture)},{r.Altura.ToString(CultureInfo.InvariantCulture)}, {r.IMC.ToString(CultureInfo.InvariantCulture)}, {r.Clasificacion}");
                }
            }

                Console.WriteLine($"Historial guardado exitosamente {nombreArchivo}\n");

        }
        catch (Exception ex)
        {
        Console.WriteLine("Error al guardar el archivo " + ex.Message);
        }
    }
}

class ResultadoIMC
{
    public string Nombre { get; set; }
    public double Peso { get; set; }
    public double Altura { get; set; }
    public double IMC { get; set; }
    public string Clasificacion { get; set; }
}
