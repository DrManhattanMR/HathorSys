using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace AccesoDatos
{
    public class ServicioCliente
    {
        public bool AgregarCliente(Cliente entidad)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                string Nombre = new System.Globalization.CultureInfo("es-ES", false).TextInfo.ToTitleCase(entidad.Nombre.ToLower());
                string ApellidoPaterno = new System.Globalization.CultureInfo("es-ES", false).TextInfo.ToTitleCase(entidad.ApellidoPaterno.ToLower());
                string ApellidoMaterno = new System.Globalization.CultureInfo("es-ES", false).TextInfo.ToTitleCase(entidad.ApellidoMaterno.ToLower());
                string idcliente = GetIdCliente(entidad);
                StringBuilder sentencia = new StringBuilder(" INSERT INTO CLIENTES ( ID_CLIENTE, NOMBRE, APEPAT, APEMAT, FECHANACIMIENTO, DIRECCION, CORREO, TELEFONO, FECHAALTA, FECHAMODIFICACION, PASSWORD, FOTO, SEXO ) ");
                sentencia.AppendLine(" VALUES ( @ID_CLIENTE, @NOMBRE, @APEPAT, @APEMAT, @FECHANACIMIENTO, @DIRECCION, @CORREO, @TELEFONO, CURDATE(), CURDATE(), @PASSWORD, @FOTO, @SEXO ) ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("ID_CLIENTE", idcliente));
                comando.Parameters.Add(new MySqlParameter("NOMBRE", Nombre.Trim()));
                comando.Parameters.Add(new MySqlParameter("APEPAT", ApellidoPaterno.Trim()));
                comando.Parameters.Add(new MySqlParameter("APEMAT", ApellidoMaterno.Trim()));
                comando.Parameters.Add(new MySqlParameter("FECHANACIMIENTO", entidad.FechaNacimiento));
                comando.Parameters.Add(new MySqlParameter("DIRECCION", entidad.Direccion.Trim()));
                comando.Parameters.Add(new MySqlParameter("CORREO", entidad.Correo.Trim().ToLower()));
                comando.Parameters.Add(new MySqlParameter("TELEFONO", entidad.Telefono.Trim()));
                //comando.Parameters.Add(new MySqlParameter("FECHAALTA", entidad.FechaAlta));
                //comando.Parameters.Add(new MySqlParameter("FECHAMODIFICACION", entidad.FechaModificacion));
                comando.Parameters.Add(new MySqlParameter("PASSWORD", GetSHA256(entidad.Password.Trim())));
                comando.Parameters.Add(new MySqlParameter("FOTO", entidad.Foto));
                comando.Parameters.Add(new MySqlParameter("SEXO", entidad.Sexo));
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
        public ListaCliente ObtenerClientes(string idCliente)
        {
            ListaCliente lista = new ListaCliente();

            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" SELECT ID, ID_CLIENTE, NOMBRE, APEPAT, APEMAT, FECHANACIMIENTO, DIRECCION, " +
                    "CORREO, TELEFONO, FECHAALTA, FECHAMODIFICACION, PASSWORD, FOTO, SEXO FROM CLIENTES ");
                sentencia.AppendLine(" WHERE 1 = 1 ");
                if (idCliente!="All")
                    sentencia.AppendLine(" AND ID_CLIENTE = @IDCLIENTE ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                if(idCliente!="All")
                    comando.Parameters.Add(new MySqlParameter("IDCLIENTE", idCliente));
                comando.CommandText = sentencia.ToString();
                MySqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Cliente entidad = ConvertirEntidad(lector);
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
        private Cliente ConvertirEntidad(MySqlDataReader lector)
        {
            Cliente entidad = new Cliente();
            entidad.Id = !(lector["ID"] is DBNull) ? Convert.ToInt32(lector["ID"]) : 0;
            entidad.IdCliente = !(lector["ID_CLIENTE"] is DBNull) ? lector["ID_CLIENTE"].ToString() : string.Empty;
            entidad.Nombre = !(lector["NOMBRE"] is DBNull) ? lector["NOMBRE"].ToString() : string.Empty;
            entidad.ApellidoPaterno= !(lector["APEPAT"] is DBNull) ? lector["APEPAT"].ToString() : string.Empty;
            entidad.ApellidoMaterno= !(lector["APEMAT"] is DBNull) ? lector["APEMAT"].ToString() : string.Empty;
            entidad.FechaNacimiento= !(lector["FECHANACIMIENTO"] is DBNull) ? Convert.ToDateTime(lector["FECHANACIMIENTO"].ToString()) : DateTime.MinValue;
            entidad.Direccion = !(lector["DIRECCION"] is DBNull) ? lector["DIRECCION"].ToString() : string.Empty;
            entidad.Correo = !(lector["CORREO"] is DBNull) ? lector["CORREO"].ToString() : string.Empty;
            entidad.Telefono = !(lector["TELEFONO"] is DBNull) ? lector["TELEFONO"].ToString() : string.Empty;
            entidad.FechaAlta = !(lector["FECHAALTA"] is DBNull) ? Convert.ToDateTime(lector["FECHAALTA"].ToString()) : DateTime.MinValue;
            entidad.FechaModificacion = !(lector["FECHAMODIFICACION"] is DBNull) ? Convert.ToDateTime(lector["FECHAMODIFICACION"].ToString()) : DateTime.MinValue;
            entidad.Password = !(lector["PASSWORD"] is DBNull) ? lector["PASSWORD"].ToString() : string.Empty;
            entidad.Foto = !(lector["FOTO"] is DBNull) ? (byte[])lector["FOTO"] : null;
            entidad.Sexo = !(lector["SEXO"] is DBNull) ? lector["SEXO"].ToString() : string.Empty;
            return entidad;
        }
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public static string GetIdCliente(Cliente entidad)
        {
            string fln = entidad.Nombre.Substring(0, 2);
            string flap = entidad.ApellidoPaterno.Substring(0, 1);
            string flam = entidad.ApellidoMaterno.Substring(0, 1);
            int db = entidad.FechaNacimiento.Day;
            int mb = entidad.FechaNacimiento.Month;
            int yb = entidad.FechaNacimiento.Year;
            int l = yb.ToString().Length;
            return (db.ToString() + flap + "-" + fln + mb + "-" + yb.ToString().Substring(2,2) + flam).ToUpper();
        }
        public bool EliminarCliente(string idCliente)
        {
            MySqlConnection conexion = ServiciosBD.ObtenerConexion();
            try
            {
                StringBuilder sentencia = new StringBuilder(" DELETE FROM CLIENTES ");
                sentencia.AppendLine(" WHERE ID_CLIENTE = @IDCLIENTE ");
                MySqlCommand comando = ServiciosBD.ObtenerComando(conexion, sentencia.ToString());
                comando.Parameters.Add(new MySqlParameter("IDCLIENTE", idCliente));
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
