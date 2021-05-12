using MySql.Data.MySqlClient;
using System;
using System.Text;
using Entidades;

namespace AccesoDatos
{
    public class ServicioCategoria
    {
        // MÉTODO PARA OBTENER TODAS LAS CATEGORIAS EN LA BASE DE DATOS (MYSQL)
        public ListaCategoria ObteberCategoria()
        {
            ListaCategoria lista = new ListaCategoria();
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" SELECT *FROM CATEGORIA ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());

                comando.CommandText = sentencia.ToString();
                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Categoria entidad = ConvertEntity(lector);
                    lista.Add(entidad);
                }
                lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
                MySqlConnection.ClearPool(conexion);
            }
            return lista;
        }
        private Categoria ConvertEntity(MySqlDataReader lector)
        {
            Categoria entidad = new Categoria();
            entidad.Id = !(lector["ID"] is DBNull) ? Convert.ToInt32(lector["ID"]) : 0;
            entidad.IdCategoria= !(lector["IDCATEGORIA"] is DBNull) ? lector["IDCATEGORIA"].ToString() : string.Empty;
            entidad.Descripcion = !(lector["DESCRIPCION"] is DBNull) ? lector["DESCRIPCION"].ToString() : string.Empty;
            return entidad;
        }

        //MÉTODO PARA AGREGAR Ó INSERTAR CATEGORIA EN LA BASE DE DATOS (MYSQL)
        public bool InsertarCategoria(Categoria entidad)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" INSERT INTO CATEGORIA ( ID_CATEGORIA, DESCRIPCION) ");
                sentencia.AppendLine(" VALUES ( @ID_CATEGORIA, @DESCRIPCION ) ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("ID_CATEGORIA", entidad.IdCategoria));
                comando.Parameters.Add(new MySqlParameter("DESCRIPCION", entidad.Descripcion));
                return comando.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
                MySqlConnection.ClearPool(conexion);
                conexion.Dispose();
            }
        }

        //MÉTODO PARA EDITAR Ó MODIFICAR CATEGORIA EN LA BASE DE DATOS (MYSQL)
        public bool EditarCategoria(Categoria categoria)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder();
                sentencia.AppendLine(" UPDATE CATEGORIA ");
                sentencia.AppendLine(" SET DESCRIPCION = @NOMBRE ");
                sentencia.AppendLine(" WHERE ID_CATEGORIA = @CLAVE ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("CLAVE", categoria.IdCategoria));
                comando.Parameters.Add(new MySqlParameter("NOMBRE", categoria.Descripcion));
                comando.CommandText = sentencia.ToString();

                return comando.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
                MySqlConnection.ClearPool(conexion);
            }
        }

        //ELIMINAR CATEGORIA MEDIANTE LA CLAVE DESDE LA BASE DE DATOS (MYSLQ)
        public bool EliminarCategoria(string clave)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder();
                sentencia.AppendLine(" DELETE FROM  CATEGORIA WHERE  ID_CATEGORIA = @CLAVE ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("CLAVE", clave));

                comando.CommandText = sentencia.ToString();
                
                return comando.ExecuteNonQuery() > 0 ;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexion.Close();
                MySqlConnection.ClearPool(conexion);
            }
        }
        
    }
    
}
