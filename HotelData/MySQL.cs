using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace HotelData
{
	public class MySQL
	{
		string host;
		string user;
		string pass;
		string baze;
		string connectionString;
		MySqlConnection myconnection;
		MySqlCommand cmd;
		string error="";
		string query;

		//Конструктор класса
		public MySQL(string host, string user, string pass, string baze)
		{
			this.host = host;
			this.user = user;
			this.pass = pass;
			this.baze = baze;
			this.connectionString =
			"SERVER=" + host +
			"; DATABASE=" + baze +
			"; UID=" + user +
			"; PASSWORD=" + pass +
			"; CHARSET=utf8";
		}

		/// <summary>
		/// Открытие соединения с БД
		/// </summary>
		/// <returns></returns>
		protected bool Open()
		{
			error = "";
			try
			{
				myconnection = new MySqlConnection(connectionString);
				myconnection.Open();
				return true;
			}
			catch (Exception ex)
			{
				error = ex.Message;
				query = "Connection to MySQL failed" + user + "\n@" + host;
				return false;
			}
		}

		/// <summary>
		/// Закрытие соединения с БД
		/// </summary>
		/// <returns></returns>
		protected bool Close()
		{
			try
			{
				myconnection.Close();
				return true;
			}
			catch (Exception ex)
			{
				error = ex.Message;
				query = "Disconnection to MySQL failed" + user + "\n@" + host;
				return false;

			}

		}

		/// <summary>
		/// функция для проверки работоспособности  после завершения отладки можно убрать
		/// </summary>
		/// <param name="query">Ввести запрос SQL</param>
		/// <returns></returns>
		public string Scalar(string query) 
		{
			this.query = query;
			string result = "";
			if (!Open())
				return null;

			try
			{
				MySqlCommand cmd = new MySqlCommand(query, myconnection);
				result = cmd.ExecuteScalar().ToString();
			}
			catch (Exception ex)
			{
				error = ex.Message;
				return null;
			}
			Close();
			return result;

		}

		internal string DateToString(DateTime date)
		{
			return date.ToString("yyyy-MM-dd");
		}

		/// <summary>
		/// Функция (безопасная) отправки запроса в БД
		/// </summary>
		/// <param name="query"></param>
		/// <returns>Возвращает строки таблицы</returns>
		public DataTable Select (string query)
		{
			DataTable table = null;
			this.query = query;
			if (!Open()) return table;

			try
			{
				MySqlCommand cmd = new MySqlCommand(query, myconnection);
				MySqlDataReader reader = cmd.ExecuteReader();
				table = new DataTable("table");
				table.Load(reader);
			}
			catch (Exception ex)
			{
				error = ex.Message;
				return null;
			}
			Close();
			return table;
		}

		/// <summary>
		/// Функция добавления записи в таблицу БД
		/// </summary>
		/// <param name="query"></param>
		/// <returns>В случае успеха вставленная строка, в случае неудачи 0</returns>
		public long Insert (string query) //return last inserted id
		{
			int rows = Update(query);
			if (rows > 0)		
				return  cmd.LastInsertedId;
			
				return 0;
			
		}

		/// <summary>
		/// Функция изменения значений в строке
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public int Update (string query) //update or delete  return count of rowed lines
		{
			int  rows = 0;
			this.query = query;
			if (!Open()) 
			return -1;

			try
			{
			    cmd = new MySqlCommand(query, myconnection);
				rows = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				error = ex.Message;
				return -1;
			}
			Close();
			return rows;
		}
	

		/// <summary>
		/// Функция обработки вводимых запросов для удаления // слешей случайных
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string addslashes (string text)
		{
			return text.Replace("'", "\\");
		}

		public bool SqlError ()
		{
			if (error == "")
				return false;

			DialogResult dr = MessageBox.Show(error + "\n" +
			"Query:\n" + query +
			"\nAbort - close program" +
			"\nRetry - repeate query" +
			"\nIgnore  - skip" ,
			 "Error database " + user + "  @" + host, 
			MessageBoxButtons.AbortRetryIgnore);
			if (dr== DialogResult.Abort)
			{
				Application.Exit();
				return false;
			}
			if (dr == DialogResult.Retry)
				return true;
			return false;
		}
	}


}
