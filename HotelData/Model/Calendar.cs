using System;


namespace HotelData.Model
{
	public  class Calendar
	{
		MySQL sql;

		public Calendar(MySQL sql)
		{
			this.sql = sql;
		}


/// <summary>
/// Установка  или отмена праздничных дней календаря
/// </summary>
/// <param name="day"></param>
/// <param name="holiday"></param>
		private void SetHoliday (DateTime day, bool holiday )

		{
			string query =
				  "UPDATE Calendar " +
				  "set holiday ="+(holiday ? "1 ":"0 ")  +
				  "where day='" + sql.DateToString(day) + "' " +
				  "LIMIT 1 ;";
			do this.sql.Update(query);
			while (sql.SqlError());
		}

		/// <summary>
		/// <Calendar> Генерация календаря на заданный год;
		/// </summary>
		/// <param name="year">На какой год создаём календарь</param>
		public void InsertDays(int year)
		{
			
			DateTime day = new DateTime(year,1,1);

			
				while (day.Year==year)
			{
				int weekend = 0;
				if (day.DayOfWeek == DayOfWeek.Friday ||
				day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
					weekend = 1;
				string query = "INSERT IGNORE INTO Calendar "+
			"set day='"+ sql.DateToString(day)+ "', "+ 
			" weekend='"+weekend+"', "+	
			"holiday=0;";
				do this.sql.Insert(query);

				while (sql.SqlError());
				day= day.AddDays(1);
			}
		}


		/// <summary>
		/// <Calendar> Удаление календаря на заданный год;
		/// </summary>
		/// <param name="year">На какой год удаляем календарь</param>
		public void DeleteDays(int year)
		{			
				string query = "Delete from Calendar " +
			"where year(day)='" + year.ToString() + "';";
				
			do this.sql.Update(query);
				while (sql.SqlError());
			
		}


		/// <summary>
		/// Установка праздничного дня
		/// </summary>
		/// <param name="day"></param>
		public void  AddHoliday (DateTime day)
		{
			SetHoliday(day, true);	

		}

		/// <summary>
		/// Удалениее  пометки праздничного дня
		/// </summary>
		/// <param name="day"></param>
		public void DelHoliday(DateTime day)
		{
			SetHoliday(day, false);	

		}

		
	}
}
