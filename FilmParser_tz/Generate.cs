using System;
using System.Collections.Generic;
using System.Reflection;
namespace FilmParser_tz
{
    internal sealed class Generate<T> where T : class, new()
    {
        private static int seed = 0;
        private static T Fill(int num)
        {
            T SomeFilm = new T();
            Type typeObj = SomeFilm.GetType();
            var fields = typeObj.GetFields();
            foreach (FieldInfo field in fields)
            {
                switch (field.FieldType.ToString())
                {
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.UInt16":
                    case "System.UInt32":
                    case "System.UInt64":
                        field.SetValue(SomeFilm, FieldsGen.GenInt(++seed, 0, 2022, 0));
                        break;
                    case "System.Nullable`1[System.Int32]":
                        field.SetValue(SomeFilm, FieldsGen.GenInt(++seed, 0, 2022));
                        break;
                    case "System.String":
                        field.SetValue(SomeFilm, FieldsGen.GenStr(++seed));
                        break;
                    default:
                        break;
                }
            }
            return SomeFilm;
        }
        public static List<T> GenObjects()
        {
            List<T> DataList = new List<T>();
            for (int i = 0; i <= 100000; i++)
            {
                DataList.Add(Fill(i));
            }
            return DataList;
        }
    }
}
