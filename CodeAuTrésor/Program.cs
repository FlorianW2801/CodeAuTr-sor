using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeAuTrésor
{
	class Program
	{
		const int testQuantity = 6;
		const string KeyPart1 = "TJQPO";
		const string KeyPart2 = "DZQSF";
		const string KeyPart3 = "BBMNO";
		const string KeyPart4 = "MBYQS";
		const string KeyPart5 = "AOTXL";

		static void Main(string[] args)
		{
			Console.WriteLine($"Hello cutesy! Welcome to Code Au Trésor! Will you find the coveted treasure?");
			Console.WriteLine($"Before running this, make sure your code is compiled by hitting Rebuild :)");
			Console.WriteLine($"If it's your first time, you should start with test n°{testQuantity} ;^)\n");
			AskForTest();
		}

		static void AskForTest()
		{
			Console.WriteLine();
			Console.WriteLine($"Please choose the test you want to run (type from 1 to {testQuantity})");

			var parsed = uint.TryParse(Console.ReadLine(), out var testNumber);
			if (!parsed)
			{
				Console.WriteLine($"You need to choose an integer between 1 and {testQuantity}!");
				AskForTest();
				return;
			}

			Console.WriteLine();
			try
			{
				switch (testNumber)
				{
					case 1:
						Test1();
						break;
					case 2:
						Test2();
						break;
					case 3:
						Test3();
						break;
					case 4:
						Test4();
						break;
					case 5:
						Test5();
						break;
					case 6:
						Test6();
						break;
					default:
						Console.WriteLine($"You need to choose an integer between 1 and {testQuantity}!");
						AskForTest();
						break;
				}
			}
			catch
			{
				AskForTest();
			}
		}

		static void Test1()
		{
			Console.WriteLine("Hi! Please give me the name of a class:");

			var className = Console.ReadLine();

			var classType = GetTypeFromClassName(className);

			SuccessForTest(1);
		}

		static void Test2()
		{
			Console.WriteLine("Hi! Please give me the name of a class, who has a function with the following behavior:");
			Console.WriteLine();
			Console.WriteLine("When I give it 2 and 8, it answers true");
			Console.WriteLine("When I give it 3 and 4, it answers false");
			Console.WriteLine("When I give it 8 and 2, it answers false");
			Console.WriteLine("When I give it 3 and 15, it answers true");
			Console.WriteLine("When I give it 7 and 28, it answers true");
			Console.WriteLine();
			Console.WriteLine("Your chosen class name:");

			var className = Console.ReadLine();

			var classType = GetTypeFromClassName(className);
			var function = GetFunctionFromTypeAndParameterTypes(classType, new List<Type> { typeof(int), typeof(int), typeof(bool) });
			var instance = GetInstanceFromType(classType);

			var TestData = new List<(int a, int b, bool result)>()
			{
				(2, 8, true),
				(3, 4, false),
				(8, 2, false),
				(3, 15, true),
				(7, 28, true),
				(7, 28, true),
				(11, 1221, true),
				(17, 2022, false)
			};

			foreach (var test in TestData)
			{
				var result = function.Invoke(instance, new object[] { test.a, test.b });
				if ((bool)result != test.result)
				{
					Console.WriteLine($"Sorry, but for the case {test.a} and {test.b} as input, I was expecting {test.result} as an answer");
					throw new Exception($"function does not match expected behavior");
				}
			}

			SuccessForTest(2);
		}

		static void Test3()
		{
			Console.WriteLine("Hi! Please give me the name of a class, who has a function with the following behavior:");
			Console.WriteLine();
			Console.WriteLine("When I give it 8, it returns a list containing 1, 2, 4 and 8");
			Console.WriteLine("When I give it 17, it returns a list containing 1 and 17");
			Console.WriteLine("When I give it 15, it returns a list containing 1, 3, 5 and 15");
			Console.WriteLine();
			Console.WriteLine("Your chosen class name:");

			var className = Console.ReadLine();

			var classType = GetTypeFromClassName(className);
			var function = GetFunctionFromTypeAndParameterTypes(classType, new List<Type> { typeof(int), typeof(List<int>) });
			var instance = GetInstanceFromType(classType);

			var TestData = new List<(int input, List<int> result)>()
			{
				(8, new List<int>{ 1, 2, 4, 8 }),
				(17, new List<int>{ 1, 17 }),
				(15, new List<int>{ 1, 3, 5, 15 }),
				(120, new List<int>{ 1, 2, 3, 4, 5, 6, 8, 10, 12, 15, 20, 24, 30, 40, 60, 120 })
			};

			foreach (var test in TestData)
			{
				var result = (List<int>)function.Invoke(instance, new object[] { test.input });
				if (!Enumerable.SequenceEqual(result, test.result))
				{
					var expectedString = "";
					test.result.ForEach(x => expectedString += x + ", ");
					Console.WriteLine($"Sorry, but for the case {test.input} as input, I was expecting {expectedString}as an answer");
					throw new Exception($"function does not match expected behavior");
				}
			}

			SuccessForTest(3);
		}

		static void Test4()
		{
			Console.WriteLine("Hi! Please give me the name of a class, who has a function with the following behavior:");
			Console.WriteLine();
			Console.WriteLine("When I give it a first list {1, 13, 6, 7} and a second list {12, 4, 8, 6}, it returns 6");
			Console.WriteLine("When I give it a first list {2, 4, 6, 8} and a second list {5, 8, 19, 4}, it returns 8");
			Console.WriteLine("When I give it a first list {1, 2, 3} and a second list {3, 4, 5}, it returns 3");
			Console.WriteLine("When I give it a first list {1, 2, 3} and a second list {6, 7, 8}, it returns -1");
			Console.WriteLine();
			Console.WriteLine("Your chosen class name:");

			var className = Console.ReadLine();

			var classType = GetTypeFromClassName(className);
			var function = GetFunctionFromTypeAndParameterTypes(classType, new List<Type> { typeof(List<int>), typeof(List<int>), typeof(int) });
			var instance = GetInstanceFromType(classType);

			var TestData = new List<(List<int> a, List<int> b, int result)>()
			{
				(new List<int>{1, 13, 6, 7}, new List<int>{12, 4, 8, 6}, 6),
				(new List<int>{2, 4, 6, 8}, new List<int>{5, 8, 19, 4}, 8),
				(new List<int>{1, 2, 3}, new List<int>{3, 4, 5}, 3),
				(new List<int>{1, 2, 3}, new List<int>{6, 7, 8}, -1),
			};

			foreach (var test in TestData)
			{
				var result = (int)function.Invoke(instance, new object[] { test.a, test.b });
				if (result != test.result)
				{
					var firstList = "";
					test.a.ForEach(x => firstList += x + ", ");
					var secondList = "";
					test.b.ForEach(x => secondList += x + ", ");
					Console.WriteLine($"Sorry, but for the case {firstList}and {secondList}as input, I was expecting {test.result} as an answer");
					throw new Exception($"function does not match expected behavior");
				}
			}

			SuccessForTest(4);
		}

		static void Test5()
		{
			Console.WriteLine("Hi! Please give me the name of a class, who has a function with the following behavior:");
			Console.WriteLine();
			Console.WriteLine("When I give it 42 and 36, it returns 6");
			Console.WriteLine("When I give it 10 and 15, it returns 5");
			Console.WriteLine("When I give it 8 and 9, it returns 1");
			Console.WriteLine("When I give it 4 and 8, it returns 4");
			Console.WriteLine();
			Console.WriteLine("Your chosen class name:");

			var className = Console.ReadLine();

			var classType = GetTypeFromClassName(className);
			var function = GetFunctionFromTypeAndParameterTypes(classType, new List<Type> { typeof(int), typeof(int), typeof(int) });
			var instance = GetInstanceFromType(classType);

			var TestData = new List<(int a, int b, int result)>()
			{
				(42, 36, 6),
				(10, 15, 5),
				(8, 9, 1),
				(4, 8, 4),
				(8978, 15142, 134),
			};

			foreach (var test in TestData)
			{
				var result = (int)function.Invoke(instance, new object[] { test.a, test.b });
				if (result != test.result)
				{
					Console.WriteLine($"Sorry, but for the case {test.a} and {test.b} as input, I was expecting {test.result} as an answer");
					throw new Exception($"function does not match expected behavior");
				}
			}

			SuccessForTest(5);
		}

		static void Test6()
		{
			Console.WriteLine("So you have come to find my treasure?");
			Console.WriteLine("Fine! I'll help you find it, but only if you succeed in my tests!");

			for (int i = 1; i <= 5; i++)
			{
				Console.WriteLine();
				Console.WriteLine("What was the secret key of test n°" + i + "?");
				var key = Console.ReadLine();
				if (key != Key(i))
				{
					Console.WriteLine("Sorry, this is not the key I was expecting ;^(");
					AskForTest();
					return;
				}
				if (i < 3)
				{
					Console.WriteLine("Good job! Keep going ;)");
				}
				if (i == 3)
				{
					Console.WriteLine("Clue n°1: Your treasure is as small as it is cute");
				}
				if (i == 4)
				{
					Console.WriteLine("Clue n°2: In some languages, \"Other\" would be her name");
				}
				if (i == 5)
				{
					Console.WriteLine("Congratulations! For you have vanquished all of my tests!");
					Console.WriteLine("Last clue: Your treasure lies close to where the giant sleeps, you can hear a soft \"Oink\" in the distance");
					Console.WriteLine("Thank you for playing along <3, press any key to exit :)");
					Console.ReadKey();
					Environment.Exit(0);
				}
			}

		}

		static string Key(int number)
		{
			string key = null;
			switch (number)
			{
				case 1:
					key = KeyPart1;
					break;
				case 2:
					key = KeyPart2;
					break;
				case 3:
					key = KeyPart3;
					break;
				case 4:
					key = KeyPart4;
					break;
				case 5:
					key = KeyPart5;
					break;
			}
			return key;
		}

		static void SuccessForTest(int number)
		{
			var key = Key(number);
			Console.WriteLine();
			Console.WriteLine($"Congratulations, you win test n°{number}!");
			Console.WriteLine("Please write down the following secret key:");
			Console.WriteLine(key);
			AskForTest();
		}

		static Type GetTypeFromClassName(string className)
		{
			var classType = Type.GetType($"SolutionCodeAuTresor.{className}, SolutionCodeAuTresor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

			if (classType is null)
			{
				Console.WriteLine("Sorry, I don't know that class ;^(");
				throw new Exception("Class not found");
			}
			else
			{
				return classType;
			}
		}

		static MethodInfo GetFunctionFromTypeAndParameterTypes(Type typeToExplore, List<Type> parameterTypes)
		{
			try
			{
				var functionCandidates = typeToExplore.GetMethods().Where(x =>
					x.GetParameters().Length == parameterTypes.Count - 1
					&& x.ReturnType == parameterTypes.Last());
				foreach (var candidate in functionCandidates)
				{
					var paramTypes = candidate.GetParameters();
					var rightFunction = true;
					for (int j = 0; j < paramTypes.Length; j++)
					{
						var paramType = paramTypes[j];
						if (paramType.ParameterType != parameterTypes[j])
						{
							rightFunction = false;
							break;
						}
					}
					if (!rightFunction)
					{
						continue;
					}
					return candidate;
				}
				throw new Exception("Could not find expected function");
			}
			catch
			{
				var inputTypesString = "";
				for (int i = 0; i < parameterTypes.Count - 1; i++)
				{
					inputTypesString += parameterTypes[i].Name + ",";
				}
				Console.WriteLine($"Your class needs to have a function with the following caracteristics:\n" +
					$"-public\n" +
					$"-{parameterTypes.Count - 1} input parameters having types {inputTypesString}\n" +
					$"-{parameterTypes.Last().Name} output!\n");
				throw;
			}
		}

		static object GetInstanceFromType(Type typeToInstanciate)
		{
			var constructor = typeToInstanciate.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
			if (constructor is null)
			{
				Console.WriteLine("Your class needs to have at least one public parameterless constructor");
				throw new Exception("Could not find an accessible parameterless constructor");
			}
			return constructor.Invoke(new object[0]);
		}
	}
}
