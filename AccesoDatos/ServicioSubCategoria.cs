using MySql.Data.MySqlClient;
using System;
using System.Text;
using Entidades;

namespace AccesoDatos
{
     public class ServicioSubCategoria
    {
        public ListaSubCategoria ObtenerSubCategoria()
        {
            ListaSubCategoria lista = new ListaSubCategoria();
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" SELECT *FROM SUBCATEGORIA ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());

                comando.CommandText = sentencia.ToString();
                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    SubCategoria entidad = ConvertEntity(lector);
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
        private SubCategoria ConvertEntity(MySqlDataReader lector)
        {
            SubCategoria entidad = new SubCategoria();
            entidad.Id = !(lector["ID"] is DBNull) ? Convert.ToInt32(lector["ID"]) : 0;
            entidad.IdSubCategoria = !(lector["IDSUBCATEGORIA"] is DBNull) ? lector["IDSUBCATEGORIA"].ToString() : string.Empty;
            entidad.Descripcion = !(lector["DESCRIPCION"] is DBNull) ? lector["DESCRIPCION"].ToString() : string.Empty;
            return entidad;
        }
        public bool InsertarSubCategoria(SubCategoria entidad)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" INSERT INTO SUBCATEGORIA ( ID_CATEGORIA, ID_SUBCATEGORIA, DESCRIPCION) ");
                sentencia.AppendLine(" VALUES ( @ID_CATEGORIA, @ID_SUBCATEGORIA, @DESCRIPCION ) ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("ID_CATEGORIA", entidad.IdCategoria));
                comando.Parameters.Add(new MySqlParameter("ID_SUBCATEGORIA", entidad.IdSubCategoria));
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
        public bool EditarSubCategoria(SubCategoria subcategoria)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder();
                sentencia.AppendLine(" UPDATE SUBCATEGORIA ");
                sentencia.AppendLine(" SET DESCRIPCION = @NOMBRE ");
                sentencia.AppendLine(" WHERE ID_SUBCATEGORIA = @CLAVE ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("CLAVE", subcategoria.IdSubCategoria));
                comando.Parameters.Add(new MySqlParameter("NOMBRE", subcategoria.Descripcion));
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
        public bool EliminarSubCategoria(string clave)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder();
                sentencia.AppendLine(" DELETE FROM  SUBCATEGORIA WHERE ID_SUBCATEGORIA = @CLAVE ");

                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("CLAVE", clave));

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
    }
}
