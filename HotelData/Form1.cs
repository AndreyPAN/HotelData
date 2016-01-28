using HotelData.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelData
{
	public partial class Form1 : Form
	{
		MySQL sql;
		Model.Client mClient;

		public Form1()
		{
			InitializeComponent();
			sql = new MySQL("localhost", "root", "war1941", "hotel");
			timer1.Enabled = true;
			mClient = new Model.Client(sql);
			//данные для вставки, (проверки) через сетеры класса Client удалить из конструктора////
			//mClient.SetClient("Yf Gut");
			//mClient.SetInfo("malasia");
			//mClient.SetAddress("Guandgou");

			//mClient.InsertClient();
			//MessageBox.Show(mClient.id_client.ToString());


		}

		
		private void button1_Click(object sender, EventArgs e)
		{
			//DataTable client= sql.Select("select * from Client;");
			//MessageBox.Show(client.Rows[6][1].ToString());

			dataGridView1.DataSource =mClient.SelectClients() ;
			dataGridView1.Columns[0].HeaderText = "№";
			//MessageBox.Show(sql.Scalar("select client from Client where id_client = '1'; "));
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//do sql.Insert("INSERT INTO Client VALUES (7, 'Chok li You', 'chokuk.n@uran.com', '+388895477', 'opheen', 'lux');");
			//while (sql.SqlError());

			DataTable calendar = sql.Select("SELECT * FROM Calendar;");
			dataGridView1.DataSource = calendar;

		}

		private void timer1_Tick(object sender, EventArgs e)
		{

			button1.Text = sql.Scalar("SELECT COUNT(*) FROM CLIENT");
		}

		private void button3_Click(object sender, EventArgs e)
		{
			DataTable calendar = sql.Select("SELECT * FROM Calendar;");
			//dataGridView1.DataSource = calendar;

			DataTable room = sql.Select("SELECT * FROM ROOM;");
			//dataGridView1.DataSource = room;

			DataTable client = sql.Select("SELECT * FROM CLIENT;");
			//dataGridView1.DataSource = client;

			DataTable map = sql.Select("SELECT * FROM MAP;");
			//dataGridView1.DataSource = map;

			DataTable book = sql.Select("SELECT * FROM BOOK;");
			//dataGridView1.DataSource = book;

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			dataGridView1.DataSource = mClient.SelectClients(textBox1.Text);
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			Random rand = new Random();
			long id = long.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
			mClient.SelectClient(id);


			mClient.SetPhone(rand.Next(80000, 99999).ToString());


			mClient.UpdateClient();




			//MessageBox.Show(mClient.client+Environment.NewLine+mClient.address);
		}

		private void button4_Click(object sender, EventArgs e)
		{

			Book mbook = new Book(sql);
		dataGridView1.DataSource = mbook.SelectBook(textBox1.Text);
		}
	}
}
