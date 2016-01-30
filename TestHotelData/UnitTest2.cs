using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelData;
using HotelData.Model;
using System.Data;
using System;

namespace TestHotelData
{
	[TestClass]
	public class UnitTest2
	{

		MySQL sql;

		public UnitTest2()
		{
			sql = new MySQL("localhost", "root", "war1941", "hotel");

		}





		[TestMethod]
		public void TestSQLconection()
		{

			string result = sql.Scalar("SELECT 5+12");
			Assert.AreEqual(result, "17");

		}


		[TestMethod]
		public void TestRoom()
		{

			Room room = new Room(sql);
			long roomID;
			string rominfo = "test543210";

			room.SetRoom("test");
			room.SetFloor("test");
			room.SetBeds(999);
			room.SetInfo("test123987");

			Assert.IsTrue(room.InsertRoom());
			roomID = room.id_room;

			DataTable table = room.SelectRooms();
			bool found = false;
			foreach (DataRow row in table.Rows)
			{
				if (row["info"].ToString() == "test123987")
					if (row["id_room"].ToString() == roomID.ToString())
					{
						found = true;
						break;
					}
			}

			Assert.IsTrue(found);

			room.SetInfo(rominfo);



			Assert.IsTrue(room.UpdateRoom(roomID));



			Assert.IsTrue(room.SelectRoom(roomID));

			Assert.AreEqual(room.info, rominfo);





			Assert.IsTrue(room.DeleteRoom(roomID));

			Assert.IsFalse(room.SelectRoom(roomID));

		}

		//Изменил обратно до состояния как было, убрал из SelectMap возвращаемое значение на void

		//[TestMethod]    
		//public void TestMap()
		//{

		//	Map map = new Map(sql);
		//	long id_room = 1;
		//	long id_book = 1;
		//	DateTime day_calendar = new DateTime(2016, 01, 01);


		//	map.setAdults(1);
		//	map.SetChildren(1);
		//	map.SetStatus("confirm");

		//	DataTable table = map.SelectMap(id_room, id_book, day_calendar);

		//	Assert.AreEqual(table.Rows.Count, 1);



		
	}
}
