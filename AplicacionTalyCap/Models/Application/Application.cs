using System.Data.SqlClient;
using System.Data;
using AplicacionTalyCap.Models.DataBase;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace AplicacionTalyCap.Models.Application
{
    public class Application
    {
        private ConexionDb conexion = new ConexionDb();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        public List<VentasTotalesModel> VentasTotales()
        {
            var oLista = new List<VentasTotalesModel>();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "VENTAS_TOTALES";
            comando.CommandType = CommandType.StoredProcedure;
            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {     
                    oLista.Add(new VentasTotalesModel()
                    {
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Tipo_Venta = dr["Tipo_Venta"].ToString(),
                        Nombre = dr["Nombre"].ToString(),
                        Cantidad = Convert.ToInt32(dr["Cantidad"]),
                        Fecha_venta = Convert.ToDateTime(dr["Fecha_Venta"]),
                        Valor_Venta = Convert.ToDecimal(dr["Valor_Venta"]),

                    });

                }
            }

            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public List<ProductoModel> Productos()
        {
            var oLista = new List<ProductoModel>();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "PRODUCTOS";
            comando.CommandType = CommandType.StoredProcedure;

            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {
                    oLista.Add(new ProductoModel()
                    {
                        Id_Producto = Convert.ToInt32(dr["Id_Producto"]),
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),

                    });

                }
            }
            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public List<ClienteModel> Clientes()
        {
            var oLista = new List<ClienteModel>();


            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "CLIENTES";
            comando.CommandType = CommandType.StoredProcedure;

            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {
                    oLista.Add(new ClienteModel()
                    {
                        Id_cliente = Convert.ToInt32(dr["Id_Cliente"]),
                        Nombre = dr["Nombre"].ToString(),

                    });

                }
            }
            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public List<TipoVentaModel> TipoVentas()
        {
            var oLista = new List<TipoVentaModel>();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "TIPO_VENTAS";
            comando.CommandType = CommandType.StoredProcedure;

            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {
                    oLista.Add(new TipoVentaModel()
                    {
                        Id_TipoVenta = Convert.ToInt32(dr["Id_TipoVenta"]),
                        Tipo_Venta = dr["Tipo_Venta"].ToString(),

                    });

                }
            }
            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public List<ProductoModel> ProductoMenosVendido()
        {
            var oLista = new List<ProductoModel>();


            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "PRODUCTO_MENOS_VENDIDO";
            comando.CommandType = CommandType.StoredProcedure;

            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {
                    oLista.Add(new ProductoModel()
                    {
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Stock = Convert.ToInt32(dr["Cantidad"])

                    });

                }
            }
            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public List<ProductoModel> ProductoMasVendido()
        {
            var oLista = new List<ProductoModel>();

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "PRODUCTO_MAS_VENDIDO";
            comando.CommandType = CommandType.StoredProcedure;

            using (var dr = comando.ExecuteReader())
            {

                while (dr.Read())
                {
                    oLista.Add(new ProductoModel()
                    {
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Stock = Convert.ToInt32(dr["Cantidad"])

                    });

                }
            }
            comando.Dispose();

            conexion.CerrarConexion();
            return oLista;

        }
        public bool VentaProducto(VentaModel obj)
        {
            bool rpta;

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "VENTA_PRODUCTO";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ID_CLIENTE", obj.Id_cliente);
                comando.Parameters.AddWithValue("@ID_PRODUCTO", obj.Id_Producto);
                comando.Parameters.AddWithValue("@VALOR_VENTA", obj.Valor_Venta);
                comando.Parameters.AddWithValue("@FECHA_VENTA", obj.Fecha_venta);
                comando.Parameters.AddWithValue("@TIPO_VENTA", obj.Id_TipoVenta);

                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
                rpta = true;
            }
            catch (Exception ex) 
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }
    }
}
