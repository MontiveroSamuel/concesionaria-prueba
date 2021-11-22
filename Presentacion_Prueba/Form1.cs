﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion_Prueba
{
    public partial class PruebaConexion : Form
    {
        public PruebaConexion()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //Creacion de String y SqlConnection necesarios para realizar conexion
            string connetionString;
            SqlConnection cnn;

            //Dar el punto de entrada al servidor/db al String e instanciar SqlConnection con ese punto de entrada
            connetionString = @"Server=PC-SAMUEL\SMONTIVERO;Database=DiariosPrueba;Trusted_Connection=True;";
            cnn = new SqlConnection(connetionString);

            //Abrir conexion
            cnn.Open();


            //SqlCommand recibe un string con la consulta y una conexion donde ir a buscar los datos
            //Aca creamos el command y el dataReader que va a buscar la infom Output  va a ser un String que
            //acumule y muestre los datos
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            //sql es el string que contiene la consulta
            sql = "SELECT TOP 5 [NroCliente], [ApellidoNombre],[Credito] FROM[DiariosPrueba].[dbo].[Clientes]";

            //Creamos la instancia nueva del SqlCommand para conectarse a la base de datos y mandar la consulta
            command = new SqlCommand(sql, cnn);

            //El dataReader recibe la info, al ejecutar .ExecuteReader devuelve todas las filas de la tabla
            dataReader = command.ExecuteReader();

            //Este while sirve para acumular linea por linea los datos de la columna definida en .GetValue(#) en un String
            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + "\n";
            }

            //Crea una ventana con la info almacenada en Output
            MessageBox.Show(Output);

            //Proocolo de cierre de las acciones -> dataReader, command y cnn
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }
    }
}