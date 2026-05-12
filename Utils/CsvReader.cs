
namespace SauceDemoLogin.Utils;

public static class CsvReader
{
    // Metodo para leer los datos de un archivo CSV y devolverlos como una lista de arreglos de strings
    public static IEnumerable<string[]> LeerCsv(string nombreArchivo, bool encabezado)
    {
        // Construir la ruta completa al archivo csv
        string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Data", nombreArchivo);

        // Verificar si el archivo existe
        if (!File.Exists(rutaArchivo))
        {
            throw new FileNotFoundException($"El archivo '{nombreArchivo}' no se encontró en la ruta '{rutaArchivo}'.");
        }

        using var reader = new StreamReader(rutaArchivo);

        // Si el archivo tiene encabezado, leer la primera línea y descartarla
        if (encabezado && !reader.EndOfStream)
        {
            reader.ReadLine();
        }

        // Continuar leyendo el archivo línea por línea
        while (!reader.EndOfStream)
        {
            // Leer una linea del archivo
            var linea = reader.ReadLine();

            // Si la linea no es nula, dividirla por comas y devolver el arreglo de valores
            if (string.IsNullOrWhiteSpace(linea))
            {
                continue; // Saltar líneas vacías
            }

            // Devolver el arreglo de valores separados por comas
            yield return linea.Split(',');
        }
    
    }
}

   //MODO DE USO:
    //Método de prueba para validar el xxxTest utilizando datos del archivo CSV con 2 columnas
    //private static IEnumerable<TestCaseData> xxxData()
    //{
    //    // Leer los datos del archivo CSV utilizando la clase CsvReader
    //    foreach (var valores in CsvReader.LeerCsv("xxx.csv", encabezado: false))
    //    {
    //        yield return new TestCaseData(valores[0], valores[1]);
    //    }
    //}
    //
    //[Test]
    //[TestCaseSource(nameof(xxxData))]
    //public async Task xxxTest(string par1, string par2)
    //{
    //***Pasos del test utilizando par1 y par2 como datos de prueba***
    //}
    //
    //Debe existir /TestData/Data/xxx.csv:
    //Para --> encabezado: false
    //         registros en formato sin encabezado: valor1,valor2
    //Para --> encabezado: true
    //         primera línea con encabezado: columna1,columna2
    //         registros en formato con encabezado: valor1,valor2