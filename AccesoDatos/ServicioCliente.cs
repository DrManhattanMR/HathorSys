using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ServicioCliente
    {
        public bool AgregarCliente(Cliente entidad)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" INSERT INTO CLIENTES ( ID_CLIENTE, NOMBRE, APEPAT, APEMAT, FECHANACIMIENTO, DIRECCION, CORREO, TELEFONO, FECHAALTA, FECHAMODIFICACION, PASSWORD, FOTO ) ");
                sentencia.AppendLine(" VALUES ( @ID_CLIENTE, @NOMBRE, @APEPAT, @APEMAT, @FECHANACIMIENTO, @DIRECCION, @CORREO, @TELEFONO, @FECHAALTA, @FECHAMODIFICACION, @PASSWORD, @FOTO ) ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("ID_CLIENTE", entidad.IdCliente));
                comando.Parameters.Add(new MySqlParameter("NOMBRE", entidad.Nombre));
                comando.Parameters.Add(new MySqlParameter("APEPAT", entidad.ApellidoPaterno));
                comando.Parameters.Add(new MySqlParameter("APEMAT", entidad.ApellidoMaterno));
                comando.Parameters.Add(new MySqlParameter("FECHANACIMIENTO", entidad.FechaNacimiento));
                comando.Parameters.Add(new MySqlParameter("DIRECCION", entidad.Direccion));
                comando.Parameters.Add(new MySqlParameter("CORREO", entidad.Correo));
                comando.Parameters.Add(new MySqlParameter("TELEFONO", entidad.Telefono));
                comando.Parameters.Add(new MySqlParameter("FECHAALTA", entidad.FechaAlta));
                comando.Parameters.Add(new MySqlParameter("FECHAMODIFICACION", entidad.FechaModificacion));
                comando.Parameters.Add(new MySqlParameter("PASSWORD", entidad.Password));
                comando.Parameters.Add(new MySqlParameter("FOTO", entidad.Foto));
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
    }
}
