using System;
using System.Collections.Generic;


namespace lr3
{
	internal class Program
	{
		/// <summary>
		/// перечисление уровней доступности
		/// </summary>
		enum LevelAvailability
		{
			NONCONFIDENTIAL = 0, 
			CONFIDENTIAL = 1,
			SECRET = 2,
			TOP_SECRET = 3,

		}
		
		public static List<string> arrayOfLevelAtributes = new List<string>(){ "NONCONFIDENTIAL", "CONFIDENTIAL", "SECRET", "TOP SECRET" };
		public static List<string> arrayOfSubjects = new List<string>() { "Admin", "User1", "User2", "User3" };
		public static List<string> arrayOfObjects = new List<string>() { "File1.txt", "File2.txt", "File3.txt", "File4.txt" };

		public static Dictionary<string, string> Start_Privileges = new Dictionary<string, string>();
		public static Dictionary<string, string> Current_Privileges = new Dictionary<string, string>();
		public static Dictionary<string, string> AvailabilityLevel = new Dictionary<string, string>();

		public static string login;
		public static string currentLvl;
		public static string currentLvlObj;
		public static int indBase;

		static void Main(string[] args)
		{
			
			

			
			bool correctLogin = false;
			string action;

			Random random = new Random();


			foreach (var item in arrayOfSubjects)
			{
				if (item.Equals("Admin"))
				{
					Start_Privileges[item] = arrayOfLevelAtributes[3];
					Current_Privileges[item] = arrayOfLevelAtributes[3];
				}
				else { 
					Start_Privileges[item] = arrayOfLevelAtributes[random.Next(0, arrayOfLevelAtributes.Count - 1)];
					Current_Privileges[item] = Start_Privileges[item];
					
				}
			}

			foreach (var item in arrayOfObjects)
			{
				AvailabilityLevel[item] = arrayOfLevelAtributes[random.Next(0, arrayOfLevelAtributes.Count)];
			}


            Console.WriteLine("ACCESS: ");
            Print(Start_Privileges);
            Console.WriteLine("ACCESS OBJECTS: ");
			Print(AvailabilityLevel);

			


			while (true)
			{
				while (!correctLogin)
				{
					Console.WriteLine("Enter login: ");
					login = Console.ReadLine();
					if (arrayOfSubjects.Contains(login)) {
						correctLogin = true;
						indBase = arrayOfLevelAtributes.IndexOf(Start_Privileges[login]);
					} 
					else Console.WriteLine("Repet enter login");
				}

				if (correctLogin)
				{
					
					Console.Write("Command> ");
					action = Console.ReadLine();

					switch (action)
					{
						case "read":
							Read();
							break;

						case "write":
							Write();
							break;

						case "change":
							ChangeLevel();
                            break;

						case "exit":
							correctLogin = false;
							break;
					}
				}
                
            }
        }


		public static void Write()
		{
			currentLvl = Current_Privileges[login];
			bool writeSuccess = false;

			while (!writeSuccess)
			{
				Console.WriteLine("Choose object: ");
				foreach (var item in arrayOfObjects)
				{
					Console.WriteLine(item);
				}
				string Object = Console.ReadLine();

				if (AvailabilityLevel.TryGetValue(Object, out currentLvlObj))
				{
					int indexObject = arrayOfLevelAtributes.IndexOf(currentLvlObj);
					int indexSubject = arrayOfLevelAtributes.IndexOf(currentLvl);

					if (indexSubject <= indexObject)
					{
						Console.WriteLine("Success write");
						writeSuccess = true;

					}
					else Console.WriteLine("Unsuccess write");
				}
			}
		}


		public static void Read()
		{
			currentLvl = Current_Privileges[login];
			bool readSuccess = false;

			while (!readSuccess)
			{
				Console.WriteLine("Choose object: ");
				foreach (var item in arrayOfObjects)
				{
                    Console.WriteLine(item);
                }
				string Object = Console.ReadLine();

				if (AvailabilityLevel.TryGetValue(Object, out currentLvlObj))
				{
					int indexObject = arrayOfLevelAtributes.IndexOf(currentLvlObj);
					int indexSubject = arrayOfLevelAtributes.IndexOf(currentLvl);

					if (indexObject <= indexSubject)
					{
                        Console.WriteLine("Success read");
						readSuccess = true;

					}
					else Console.WriteLine("Unsuccess read");
                }
			}
		}


		public static void ChangeLevel()
		{
			bool changeSuccess = false;
			while (!changeSuccess)
			{
				Console.WriteLine("Enter new level access: 1. NONCONFIDENTIAL \n" +
													  "2. CONFIDENTIAL\n" +
													  "3. SECRET\n" +
													  "4. TOP SECRET");
				string newLevel = Console.ReadLine();

				if (Current_Privileges.TryGetValue(login, out currentLvl))
				{
					int indNew = arrayOfLevelAtributes.IndexOf(newLevel);


					if (indNew <= indBase)
					{
						Current_Privileges[login] = arrayOfLevelAtributes[indNew];
						changeSuccess = true;
						Print(Current_Privileges);
					}
				}
            }
		}


		public static void Print(Dictionary<string,string> dict)
		{
			foreach (var item in dict)
			{
                Console.WriteLine(item.Key + " : " + item.Value);
            }
		}
	}
}
