using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ServiciosBD
    {
        public static MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection();
            string cadena = "datasource=localhost;port=3306;username=root;password=4682;database=hathor;";
            conexion.ConnectionString = cadena;
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MySqlCommand ObtenerComando(MySqlConnection conn, string sql)
        {
            return new MySqlCommand(sql, conn);
        }
        public static MySqlCommand ObtenerComando(MySqlConnection conn, string sql, MySqlTransaction tran)
        {
            return new MySqlCommand(sql, conn, tran);
        }
        public bool EjecutarProcedimiento(string pNombre)
        {
            MySqlConnection conexion = ObtenerConexion();
            try
            {
                MySqlCommand cmdProc = new MySqlCommand(pNombre, conexion);
                cmdProc.CommandType = CommandType.StoredProcedure;
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                cmdProc.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                MySqlConnection.ClearPool(conexion);
            }
        }
    }
}
