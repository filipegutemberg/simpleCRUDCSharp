﻿using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace CRUDMysqlApp
{
    public class CrudDemo
    {
        private string connString;

        public CrudDemo(string server,string database,string uid,string password)
        {
            connString = string.Format("server={0};database={1};user={2};password={3}", 
                server, database, uid, password);
        }

        public void TestConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");
                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
        public void CreateData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "insert into users(nome,endereco, garantia) values(@nome,@endereco,@garantia)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar, 55);
                cmd.Parameters.Add("@endereco", MySqlDbType.VarChar, 55);
                cmd.Parameters.Add("@garantia", MySqlDbType.VarChar, 24);

                //cmd.Parameters.Add("@price", MySqlDbType.Float);
                //cmd.Parameters.Add("@created", MySqlDbType.DateTime);

                Console.Write("Inserting 10 data....");
                //DateTime now = DateTime.Now;
                for (int i = 1; i <= 10; i++)
                {
                    cmd.Parameters[0].Value = "pessoa-" + i;
                    cmd.Parameters[1].Value = "casa-" + i;
                    cmd.Parameters[2].Value = "garantia-" + i;

                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Done");

                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public void BulkData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                MySqlBulkLoader bulk = new MySqlBulkLoader(conn);
                bulk.TableName = "users";
                bulk.FieldTerminator = "\t";
                bulk.LineTerminator = "\n";
                bulk.FileName = "C:/Users/filipegutemberg/Documents/IntegracaoDeSistemas/IS_Aula_05_ETLs_ExternalTransferLoad/crud-mysql-cs/src/CRUDMysqlApp/CRUDMysqlApp/product.txt"; // change with your file
                bulk.NumberOfLinesToSkip = 0;
                bulk.Columns.Add("nome");
                bulk.Columns.Add("endereco");
                bulk.Columns.Add("garantia");
                
                Console.Write("Inserting bulk data....");
                int count = bulk.Load();
                Console.WriteLine("Done-" + count.ToString());

                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void ReadData()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "select numero_aluno,nome,endereco,garantia from users";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    Console.WriteLine("Id: " + rd["numero_aluno"].ToString());
                    Console.WriteLine("nome: " + rd["nome"].ToString());
                    Console.WriteLine("Endereco: " + rd["endereco"].ToString());
                    Console.WriteLine("Garantia: " + rd["garantia"].ToString());
                    Console.WriteLine("---------------------------");
                }
                rd.Close();

                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void UpdateData(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "update users set nome=@nome,endereco=@endereco,garantia=@garantia where numero_aluno=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar, 55);
                cmd.Parameters.Add("@endereco", MySqlDbType.VarChar, 55);
                cmd.Parameters.Add("@garantia", MySqlDbType.VarChar, 24);
                cmd.Parameters.Add("@id", MySqlDbType.Int32);

                Console.Write("Updating data....");
                cmd.Parameters[0].Value = "user-update";
                cmd.Parameters[1].Value = "endereco-update";
                cmd.Parameters[2].Value = "garantia-update";
                cmd.Parameters[3].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }catch(MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void DeleteData(int id)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "delete from users where numero_aluno=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32);

                Console.Write("Deleting data....");
                cmd.Parameters[0].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
        public void DeleteUserSP(int idUsuario)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "CALL `SP_GATILHO`(@p0, @p1, @p2, @p3, @p4)"; 
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@p0", MySqlDbType.Int32);
                cmd.Parameters[0].Value = 3;
                cmd.Parameters.Add("@p1", MySqlDbType.Int32);
                cmd.Parameters[1].Value = idUsuario;
                cmd.Parameters.Add("@p2", MySqlDbType.VarChar, 55);
                cmd.Parameters[2].Value = "";
                cmd.Parameters.Add("@p3", MySqlDbType.VarChar, 55);
                cmd.Parameters[3].Value = "";
                cmd.Parameters.Add("@p4", MySqlDbType.VarChar, 55);
                cmd.Parameters[4].Value = "";

                Console.Write("Deleting data....");
                //cmd.Parameters[0].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        public void CreateUserSP(string nome, string endereco, string garantia)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "CALL `SP_GATILHO`(@p0, @p1, @p2, @p3, @p4)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@p0", MySqlDbType.Int32);
                cmd.Parameters[0].Value = 1;
                cmd.Parameters.Add("@p1", MySqlDbType.Int32);
                cmd.Parameters[1].Value = 0;
                cmd.Parameters.Add("@p2", MySqlDbType.VarChar, 55);
                cmd.Parameters[2].Value = nome;
                cmd.Parameters.Add("@p3", MySqlDbType.VarChar, 55);
                cmd.Parameters[3].Value = endereco;
                cmd.Parameters.Add("@p4", MySqlDbType.VarChar, 55);
                cmd.Parameters[4].Value = garantia;

                Console.Write("Creating user....");
                //cmd.Parameters[0].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        public void UpdateUserSP(int idUsuario, string novoNome, string novoEndereco, string novaGarantia)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "CALL `SP_GATILHO`(@p0, @p1, @p2, @p3, @p4)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@p0", MySqlDbType.Int32);
                cmd.Parameters[0].Value = 2;
                cmd.Parameters.Add("@p1", MySqlDbType.Int32);
                cmd.Parameters[1].Value = idUsuario;
                cmd.Parameters.Add("@p2", MySqlDbType.VarChar, 55);
                cmd.Parameters[2].Value = novoNome;
                cmd.Parameters.Add("@p3", MySqlDbType.VarChar, 55);
                cmd.Parameters[3].Value = novoEndereco;
                cmd.Parameters.Add("@p4", MySqlDbType.VarChar, 55);
                cmd.Parameters[4].Value = novaGarantia;

                Console.Write("Updating user....");
                //cmd.Parameters[0].Value = id;

                cmd.ExecuteNonQuery();
                Console.WriteLine("Done");


                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        public void ListarUsuariosSP()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                Console.WriteLine("Connected");

                string query = "CALL `SP_GATILHO`(@p0, @p1, @p2, @p3, @p4)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add("@p0", MySqlDbType.Int32);
                cmd.Parameters[0].Value = 4;
                cmd.Parameters.Add("@p1", MySqlDbType.Int32);
                cmd.Parameters[1].Value = 0;
                cmd.Parameters.Add("@p2", MySqlDbType.VarChar, 55);
                cmd.Parameters[2].Value = "";
                cmd.Parameters.Add("@p3", MySqlDbType.VarChar, 55);
                cmd.Parameters[3].Value = "";
                cmd.Parameters.Add("@p4", MySqlDbType.VarChar, 55);
                cmd.Parameters[4].Value = "";
                
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Console.WriteLine("Id: " + rd["numero_aluno"].ToString());
                    Console.WriteLine("nome: " + rd["nome"].ToString());
                    Console.WriteLine("Endereco: " + rd["endereco"].ToString());
                    Console.WriteLine("Garantia: " + rd["garantia"].ToString());
                    Console.WriteLine("---------------------------");
                }

                rd.Close();

                conn.Close();
                Console.WriteLine("Closed");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

    }
}
