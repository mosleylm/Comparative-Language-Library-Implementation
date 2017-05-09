using System;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;

/*
Liam Mosley
CSE 565

Serializer function for an object, can handle data types of
boolean, int, double, and string

*/

namespace Serializer {
	public class MySerializer {
		public static string Serialize(Object obj) {
			Type type = obj.GetType();
				
			string serialized = "";

			foreach(FieldInfo info in type.GetFields()) {
				if(info.MemberType == MemberTypes.Field) {
					if(info.FieldType != typeof(String)) {
						serialized += info.Name + " = " + info.GetValue(obj) + "; ";
					} else {
						serialized += info.Name + " = \"" + info.GetValue(obj) +"\"; ";
					}
				}
			}

			return serialized;
		}
		public static T Deserialize<T>(string str) {
			Type type = typeof(T);
			ConstructorInfo ctor = type.GetConstructor(new Type[] { });
			T obj = (T)ctor.Invoke(new Object[] { });		
			
			str = str.Trim();
			
			// split string on ';' except for those in quotations
			string[] toks = Regex.Split(str, ";(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");	
			Dictionary<string, string> variables = new Dictionary<string, string>();			

			// split each variable expr on '=' except those in quotes
			// if assignment starts with a quote, trim and save to value of pair
			// dont go into last index, it's an extra line from Split above.
			for(int i=0; i<toks.Length - 1; i++) {
				string[] expr = Regex.Split(toks[i], "=(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
				
				variables.Add(expr[0].Trim(), expr[1].Replace("\"", "").Trim());
			}	
			
			// now set values			
			foreach(string name in variables.Keys) {
				Console.WriteLine("Name: " +name+" value: " + variables[name]);
				Set(obj, name, variables[name]);
			}

			return obj;
		}
		// SET VALUES (COPIED FROM ZMUDA SOURCE CODE DATASTRUCTURES.CS
		public static void Set<T>(T o, String fieldName, object v) {
			Type t = o.GetType();
			FieldInfo info = t.GetField(fieldName);
			string fieldType = info.FieldType.Name;
			

			switch(fieldType) {
				case "Int32":
					info.SetValue(o, System.Convert.ChangeType(v, info.FieldType));
					break;
				case "Double":
					info.SetValue(o, System.Convert.ChangeType(v, info.FieldType));
					break;
				case "Boolean":
					info.SetValue(o, System.Convert.ChangeType(v, info.FieldType));
					break;
				default:
					info.SetValue(o, v);
					break;
			}
		}

	}
	public class Point {
		public int x, y;
		public bool testBool;
		public double z;
		public string name;

		public Point() {
			x = y = 0;
		}
		public Point(int X, int Y) {
			x = X;
			y = Y;
		}
	}
	public class Test {
		public static void Main(String [] args) {	
			Point p1 = new Point(2, 3);
			p1.name = ";;he;;l;';l====oo'oo";
			p1.z = 15.3;			

			String str1 = MySerializer.Serialize(p1);
			Console.WriteLine(str1);
			//string[] toks = Regex.Split(str1, ";(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

			Point newPt = MySerializer.Deserialize<Point>(str1);
			Console.WriteLine(newPt);

			Console.WriteLine("Name of newPt: " + newPt.name);
		}
	}
}

