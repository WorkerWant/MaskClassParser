using System;
using System.Collections.Generic;
using System.Reflection;

namespace FilmParser_tz
{
    internal sealed class MainParser<T> where T : class, new()
    {
        static string OutputString = string.Empty;
        public List<string> FormatList(List<T> InputList, string FormatMask)
        {
            
            List<string> OutputList = new List<string>();
            for (int i = 0; i < InputList.Count; i++)
            {
                for (int k = 0; k < FormatMask.Length; k++)
                {
                    OutputString += CheckSquare(FormatMask[k], ref k, FormatMask, InputList[i]);
                    OutputString += CheckFigure(FormatMask[k], ref k, FormatMask, InputList[i]);
                    if (k >= FormatMask.Length ||
                        FormatMask[k] == ']'   ||
                        FormatMask[k] == '}')
                    {
                        continue;
                    }
                    if (OutputString.Length > 0 &&
                        OutputString[OutputString.Length - 1] == ' ' &&
                        FormatMask[k] == ' ')
                    {
                        continue;
                    }
                    OutputString += FormatMask[k];
                }
                OutputList.Add(OutputString);
                OutputString = string.Empty;
            }
            return OutputList;
        }
        static string CheckFigure(char CurrentSymbol, ref int iter, string FormatMask, T DataObject)
        {
            if (CurrentSymbol == '{')
            {
                string returned = string.Empty;
                int indexer = 0;
                int shiftletter = 0;
                for (int i = iter + 1; i < FormatMask.Length; i++)
                {
                    if (FormatMask[i] == '[')
                    {
                        if (!string.IsNullOrEmpty(CheckSquare(FormatMask[i], ref i, FormatMask, DataObject)))
                        {
                            indexer++;
                        }
                    }
                    else if (FormatMask[i] == '}')
                    {
                        shiftletter = i;
                        break;
                    }
                }
                int counter = indexer;
                if (indexer > 0)
                {
                    for (int i = iter + 1; i < FormatMask.Length; i++)
                    {
                        if (FormatMask[i] == '[')
                        {
                            returned += CheckSquare(FormatMask[i], ref i, FormatMask, DataObject);
                        }
                        else if (FormatMask[i] == '<')
                        {
                            string separator = string.Empty;
                            sbyte sepCount = 0;
                            for (int j = i+1; j < FormatMask.Length; j++)
                            {
                                if (FormatMask[j] == '>')
                                {
                                    break;
                                }
                                ++sepCount;
                                separator += FormatMask[j];
                            }
                            if (indexer > 1)
                            {
                                bool[] TwoTrue = new bool[2];
                                counter--;
                                for (int j = i; j >= 0; --j)
                                {
                                    if (FormatMask[j] == '[')
                                    {
                                        if (!string.IsNullOrEmpty(CheckSquare(FormatMask[j], ref j, FormatMask, DataObject)))
                                        {
                                            TwoTrue[0] = true;
                                        }
                                        break;
                                    }
                                }
                                for (int j = i; j < FormatMask.Length; j++)
                                {
                                    if (FormatMask[j] == '[')
                                    {
                                        if (!string.IsNullOrEmpty(CheckSquare(FormatMask[j], ref j, FormatMask, DataObject)))
                                        {
                                            TwoTrue[1] = true;
                                        }
                                        break;
                                    }
                                }
                                bool isAdded = false;
                                if (TwoTrue[0] && TwoTrue[1])
                                {
                                    returned += separator;
                                    isAdded = true;
                                }
                                if (TwoTrue[0] && counter > 0 && !isAdded)
                                {
                                    returned += separator;
                                }
                                i += 1 + sepCount;

                            }
                            else
                            {
                                i += 1 + sepCount;
                            }
                        }
                        else if (FormatMask[i] == '}')
                        {
                            if (i >= FormatMask.Length)
                            {
                                iter = i - 1;
                            }
                            else
                            {
                                iter = i;
                            }
                            return returned;
                        }
                        else
                        {
                            returned += FormatMask[i];
                        }
                    }
                }
                else
                {
                    iter = shiftletter;
                    return null;
                }
            }
            return null;
        }
        static string CheckSquare(char CurrentSymbol, ref int iter, string FormatMask, T DataObject)
        {
            if (CurrentSymbol == '[')
            {
                string SquareStr = string.Empty;
                for (int i = iter + 1; i < FormatMask.Length; i++)
                {
                    if (FormatMask[i] == ']')
                    {
                        iter = i;
                        return InSquareFields(SquareStr, DataObject);
                    }
                    SquareStr += FormatMask[i];
                }
            }
            return null;
        }
        static string InSquareFields(string Square, T DataField)
        {
            Type typeObj = DataField.GetType();
            FieldInfo[] fields = typeObj.GetFields();
            foreach (var field in fields)
            {
                if (field.Name == Square)
                {
                    if (field.GetValue(DataField) is null)
                    {
                        return null;
                    }
                    return field.GetValue(DataField).ToString();
                }
            }
            return null;
        }
    }
}
