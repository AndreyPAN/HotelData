using System.Data;

namespace HotelData.Model
{
	public class Client
	{
		public long id_client { get; private set; }
		public string client { get; private set; }
		public string e_mail { get; private set; }
		public string phone { get; private set; }
		public string address { get; private set; }
		public string info { get; private set; }
		MySQL sql;

		public Client(MySQL sql)
		{
			id_client = 0;
			client = "";
			e_mail = "";
			phone = "";
			address = "";
			info = "";
			this.sql = sql;
		}
		/// <summary>
		/// set data client name
		/// </summary>
		/// <param name="client">  add comment</param>
		public void SetClient(string client)
		{
			this.client = client;
		}

		public void SetEmail(string e_mail)
		{
			this.e_mail = e_mail;
		}

		public void SetPhone(string phone)
		{
			this.phone = phone;
		}

		public void SetAddress(string address)
		{
			this.address = address;
		}

		public void SetInfo(string info)
		{
			this.info = info;
		}

		/// <summary>
		///    < Client > Регистрация нового клиента в базе;
		/// </summary>

		public bool InsertClient()
		{
			string query = "INSERT INTO Client(client, e_mail, phone, address, info) " +
			"VALUES ('" + sql.addslashes(client) + "', '" + sql.addslashes(e_mail) + "', '" + sql.addslashes(phone) + "', '" + sql.addslashes(address) + "', '" + sql.addslashes(info) + "');";
			do id_client = this.sql.Insert(query);
			while (this.sql.SqlError());
			return (this.id_client > 0);
		}

		/// <summary>
		/// <Client> Получение списка клиентов ;
		/// </summary>
		/// <returns></returns>
		public DataTable SelectClients()

		{
			DataTable client;
			do client = sql.Select("select id_client, client, e_mail, phone, address, info from Client;");

			while (sql.SqlError());
			return client;

		}
		//ModelClient.SelectClients (string param)
		//<Client> Получение списка клиентов по фильтру;

		/// <summary>
		/// <Client> Получение списка клиентов по фильтру ;
		/// </summary>
		/// <returns></returns>
		public DataTable SelectClients(string find)

		{
			DataTable client;
			find = sql.addslashes(find);
			do client = sql.Select("select id_client, client, e_mail, phone, address, info"+
			" from Client where client like '%"+find+"%'"+ 
		"or e_mail like '%"+find+"%'"+
		"or phone like '%"+find+"%'"+
		"or address like '%"+find+"%'"+
		"or info like '%"+find+"%'"+
		"or id_client = '"+find+"'");

			while (sql.SqlError());
			return client;

		}



		/// <summary>
		/// <Client> Получение инфы по заданному клиенту;
		/// </summary>
		/// <param name="id_client">N of client</param>
		/// <returns> True успешно, False -ошибка </returns>
		public bool SelectClient(long id_client)
		{
			DataTable client;
			do client = sql.Select("select id_client, client, e_mail, phone, address, info" +
			" from Client where " +
		"id_client = '" + id_client.ToString() + "';");

			while (sql.SqlError());
			if (client.Rows.Count == 0)
				return false;

			this.id_client = long.Parse( client.Rows[0]["id_client"].ToString());
			this.client =    client.Rows[0]["client"].ToString();
			this.e_mail =    client.Rows[0]["e_mail"].ToString();
			this.phone =     client.Rows[0]["phone"].ToString();
			this.address =   client.Rows[0]["address"].ToString();
			this.info =      client.Rows[0]["info"].ToString();

			return true;


		}


		/// <summary>
		/// >Исправление\дополнение данных по существующему клиенту;
		/// </summary>
		/// <param name="id_client"></param>
		/// <returns>True успешно, False -ошибка</returns>
		public bool  UpdateClient () 
		{
			int result;
			do result = sql.Update("UPDATE CLIENT " +
			"set client =  '" +sql.addslashes( this.client )+ "'," +
			" e_mail = '" + sql.addslashes( this.e_mail )+ "'," +
			" phone = '" + sql.addslashes( this.phone )+ "'," +
			" address = '" + sql.addslashes( this.address )+ "'," +
			" info = '" + sql.addslashes( this.info )+ "'" +
			" where id_client =" + sql.addslashes(id_client.ToString()) + ";");

			while (sql.SqlError());


			if (result == 0)
				return false;
			return true;
		}
		//<Client>Исправление\дополнение данных по существующему клиенту;

		//UPDATE Client
		//set client = 'Gikita Derun',
		//	e_mail = 'ta@jec.ww.com',
		//	phone = '380 555 9898989',
		//	address = 'world',
		//	info = 'afterlife'
		//	where id_client = 3
		//	LIMIT 1;


	}
}
