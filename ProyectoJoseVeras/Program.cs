using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace ProyectoJoseVeras
{
    internal class Program
    {
        
           static List<Estudiante> estudiantes = new List<Estudiante>();
           
            static void Main(string[] args)
            {
             
              


                bool Salir = false;

                while (!Salir)
                {
                    Console.WriteLine("Bienvenido al sistema de Registro Estudiantil JV");
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Seleccione una opción:");
                    Console.WriteLine("1. Agregar nuevo estudiante");
                    Console.WriteLine("2. Visualizar estudiantes existentes");
                    Console.WriteLine("3. Eliminar estudiante");
                    Console.WriteLine("4. Registrar calificaciones");
                    Console.WriteLine("5. Salir");
                    Console.WriteLine("------------------------------------------------");

                string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            Agregar();
                            break;
                        case "2":
                            Mostrar();
                            break;
                        case "3":
                            Eliminar();
                            break;
                        case "4":
                            Calificaciones();
                            break;
                        case "5":
                            Salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                            break;
                    }

                    Console.WriteLine();
                }
            }

            static void Agregar()
            {
                Console.WriteLine("Ingrese el nombre completo del estudiante:");
                string nombre = Console.ReadLine();

                Console.WriteLine("Ingrese el número de identificación del estudiante:");
                string id = Console.ReadLine();

                estudiantes.Add(new Estudiante(nombre, id));

                
              //Aqui establezco la conexion a la BBDD,especialemtente tabla estudiante
                SqlConnection sqlConnection;
                string cadenaConexion = "Data Source=DESKTOP-0RAL8F0\\SQLEXPRESS; Initial Catalog = RegistroEstudiantil; Integrated Security=True";

                 sqlConnection = new SqlConnection(cadenaConexion);
                 sqlConnection.Open();
                 Console.WriteLine("Conectado");
                 string insertQuery = " insert into Estudiante(nombre,id) values ('" + nombre + "', '" + id + "')"; 
                 SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection );
                 insertCommand.ExecuteNonQuery();
                 Console.WriteLine("El estudiante ha sido agregado exitosamente.");
                 sqlConnection.Close();

            }
            
            

            static void Mostrar()
            {
                if (estudiantes.Count == 0)
                {
                    Console.WriteLine("No hay estudiantes registrados.");
                }
                else
                {
                    foreach (var estudiante in estudiantes)
                    {
                        Console.WriteLine($"Nombre: {estudiante.Nombre}, ID: {estudiante.Id}");
                    }
                }
               SqlConnection sqlConnection;
               string cadenaCon = "Data Source=DESKTOP-0RAL8F0\\SQLEXPRESS; Initial Catalog = RegistroEstudiantil; Integrated Security=True";

               sqlConnection = new SqlConnection(cadenaCon);
               sqlConnection.Open();

               SqlCommand cmd = new SqlCommand("select * from ESTUDIANTE;", sqlConnection);
               
               SqlDataReader reader = cmd.ExecuteReader();

               while (reader.Read())
               {
                 string Nombre = reader["Nombre"].ToString();
                 string Id = reader["Id"].ToString();
                 Console.WriteLine(Nombre + "  " + Id);

               }
               reader.Close();
               sqlConnection.Close();
               Console.Read();
            

            

            }
        
            static void Eliminar()
            {
                if (estudiantes.Count == 0)
                {
                    Console.WriteLine("No hay estudiantes registrados.");
                }
                else
                {
                    Console.WriteLine("Ingrese el número de identificación del estudiante que desea eliminar:");
                    string id = Console.ReadLine();

                    bool identificado = false;

                    foreach (var estudiante in estudiantes)
                    {
                        if (estudiante.Id == id)
                        {
                            estudiantes.Remove(estudiante);
                            identificado = true;
                            break;
                        }
                    }

                    if (identificado)
                    {
                        Console.WriteLine("El estudiante ha sido eliminado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún estudiante con ese número de identificación.");
                    }
                }
            }

            static void Calificaciones()
            {
                if (estudiantes.Count == 0)
                {
                    Console.WriteLine("No hay estudiantes registrados.");
                }
                else
                {
                    Console.WriteLine("Ingrese el número de identificación del estudiante:");
                    string id = Console.ReadLine();

                    bool found = false;

                    foreach (var student in estudiantes)
                    {
                        if (student.Id == id)
                        {
                            Console.WriteLine($"Ingrese las calificaciones del estudiante {student.Nombre}:");

                            // Aquí voy agregar la lógica para registrar las calificaciones del estudiante

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("No se encontró ningún estudiante con ese número de identificación.");
                    }
                }
            }
        }

        class Estudiante
        {
            public string Nombre { get; set; }
            public string Id { get; set; }

            public Estudiante(string nombre, string id)
            {
                Nombre = nombre;
                Id = id;
            }
        }
    
    
}