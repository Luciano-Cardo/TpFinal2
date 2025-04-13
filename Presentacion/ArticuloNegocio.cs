using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Utilizamos una libreria para poder declarar determinados objetos

namespace Presentacion
{
    class ArticuloNegocio
    {
        //Esta clase establece una conexion a la base de datos y realza una lectura/trae los datos.
        //En esta clase creamos los metodos de acceso a datos para los articulos.

        public List<Articulo> listar() //Hacemos los metodos public para que la misma puede ser accedida desde el exterior
        {
            List<Articulo> lista = new List<Articulo>();

            SqlConnection conexion = new SqlConnection(); //Con esto nos conectamos a la base de datos

            SqlCommand comando = new SqlCommand(); //Con esto podemos realizar acciones

            SqlDataReader lector; //Guardamos el set de datos en el lector(variable)

            //Utilizamos un manejo excepciones para retorner una lista si todo esta ok o retorne error si hay algo mal.
            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";//Configuramos la cadena de conexion, le aclaramos a donde se va a conectar  y a que base de datos se va a conectar y como nos vamos a conectar entre las comillas dobles.
                
                comando.CommandType = System.Data.CommandType.Text;//El comando sirve para realizar la accion, en este caso la lectura mandando la sentencia sql que quiero ejecutar
                
                comando.CommandText = "select Codigo, Nombre, Descripcion from ARTICULOS "; //Esta consulta primero la hacemos en sql y luego la copiamos y pegamos para evitar erores, esta consulta la mandamos desde la apliacion a la base de datos

                comando.Connection = conexion; //Ejecuta el comando configurado en esta conexion

                conexion.Open(); //Abre la conexion

                lector = comando.ExecuteReader(); //Realiza la lectura

                while (lector.Read()) //se fija si hay una lectura, si da true se posiciona el puntero en la primer fila de la base de datos e ingresa en el while con el lector apuntado a la primer fila
                {
                    //En cada vuelta del while crea una nueva instancia del articulo y lo guarda en la lista
                    Articulo aux = new Articulo(); // Creamos un articulo auxiliar

                    //Cargamos los datos del registro
                    aux.codigo = (string)lector["Codigo"];
                    aux.nombre = (string)lector["Nombre"];
                    aux.descripcion = (string)lector["Descripcion"];

                    lista.Add(aux); //Agrega el articulo a la lista

                }
                conexion.Close(); //Cierra la conexion
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
}
}
