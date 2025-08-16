using System.Reflection;
using System.Runtime.Loader;

string pathToLibrary = @"F:\Projects\Acly.Assembler\Acly.Demos.NoStd\bin\Debug\Acly.Demos.NoStd.dll";
string pathToSystemLibrary = @"F:\Projects\Acly.Assembler\Acly.System\bin\Debug\net9.0\Acly.System.dll";

//AssemblyLoadContext assemblyContext = AssemblyLoadContext.Default;
AssemblyLoadContext assemblyContext = new("LowSystem");
var systemAssembly = assemblyContext.LoadFromAssemblyPath(pathToSystemLibrary);
var assembly = assemblyContext.LoadFromAssemblyPath(pathToLibrary);

foreach (var type in systemAssembly.GetTypes())
{
    Console.WriteLine(type.FullName + " - " + type.BaseType?.FullName + " - " + (type.BaseType == typeof(object)));
}

Console.WriteLine();

if (assembly.EntryPoint != null)
{
    var entryClass = assembly.EntryPoint.DeclaringType;
    var properties = entryClass.GetProperties();
    
    //assembly.EntryPoint.Invoke(null, null);

    foreach (var property in properties)
    {
        if (property.Name == "Result")
        {
            var value = property.GetValue(null);
            
            if (value != null)
            {
                Console.WriteLine(value);
                Console.WriteLine($"{value.GetType().FullName} - {value.GetType().Assembly.FullName}");
            }
        }
    }
}

Console.WriteLine("No entry points");
Console.ReadLine();