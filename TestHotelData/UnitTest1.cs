using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelData;
using HotelData.Model;

namespace TestHotelData
{
	[TestClass]
	public class UnitTest1
	{

		//MySQL sql;
		//const int testyear=1970;

		//public UnitTest1()
		//{
		//	sql = new MySQL("localhost", "root", "war1941", "hotel");
		//}
		//[TestMethod]
		//public void TestSQLconection()
		//{
			
		//	string result = sql.Scalar("SELECT 5+12");
		//	Assert.AreEqual(result, "17");

		//}
		//[TestMethod]
		//public void TestSQLconection1()
		//{
			
		//	string result = sql.Scalar("SELECT 15+12");
		//	Assert.AreEqual(result, "27");

		//}

		//[TestMethod]
		//public void TestCalendarAddDays()
		//{

		//	Calendar calendar = new Calendar(sql);
		//	calendar.InsertDays(testyear);
		//	string days = sql.Scalar("SELECT COUNT(*) FROM CALENDAR WHERE YEAR(DAY)="+testyear.ToString());

		//	Assert.AreEqual(days, DateTime.IsLeapYear(testyear)? "366":"365");
		//	//sql.Update("DELETE * from CALENDAR where YEAR(day)="+testyear.ToString());

		//}

		//[TestMethod]
		//public void TestCalendarAddHolidays()
		//{

		//	Calendar calendar = new Calendar(sql);
		//	//calendar.InsertDays(testyear);


		//	calendar.AddHoliday(new DateTime(testyear, 5, 25));
		//	string days = sql.Scalar("SELECT count(*) FROM CALENDAR WHERE DAY='"+testyear+"-05-25' AND holiday=1;");

		//	sql.Update("DELETE * from CALENDAR where YEAR(day)="+testyear);



		//	Assert.AreEqual(days, "1");

		//}

		//[TestMethod]
		//public void TestCalendarDelHolidays()
		//{

		//	Calendar calendar = new Calendar(sql);
		//	//calendar.InsertDays(testyear);


		//	calendar.DelHoliday(new DateTime(testyear, 5, 25));
		//	string days = sql.Scalar("SELECT count(*) FROM CALENDAR WHERE DAY='" + testyear + "-05-25' AND holiday=0;");

		//	sql.Update("DELETE * from CALENDAR where YEAR(day)=" + testyear);



		//	Assert.AreEqual(days, "1");

		//}

		//[TestMethod]
		//public void TestCalendarDelDays()
		//{

		//	Calendar calendar = new Calendar(sql);
		//	calendar.DeleteDays(testyear);
		//	string days = sql.Scalar("SELECT COUNT(*) FROM CALENDAR WHERE YEAR(DAY)=" + testyear.ToString());

		//	Assert.AreEqual(days, "0");

		//}


	}
}
