using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CSscript 
{
    class Search 
    {
        static void Main(string[] args)
        {
            List<string> fields;
            string field;
            string csv_file_path;
            string search_key;
            string record;
            int column_index;
            bool found;

            if(args.Length != 3)
            {
                Console.WriteLine("Expected: Search.exe <csv_file_path> <column_index> <search_key>");
                return;
            }

            csv_file_path = args[0];

            if(!File.Exists(csv_file_path))
            {
                Console.WriteLine("File path " + "\"" + csv_file_path + "\"" + " cannot be found.");
                return;
            }

            if(!int.TryParse(args[1], out column_index))
            {
                Console.WriteLine("Invalid " + "\"" + args[1] + "\"" + " <column_index> ") ;
                return;
            }
    
            column_index = int.Parse(args[1]);
            search_key = args[2].ToLower();
            found = false;
        
            using(StreamReader streamReader = new StreamReader(csv_file_path))
            {
                while(streamReader.EndOfStream == false)
                {
                    record = streamReader.ReadLine().Trim();

                    if(record == "" || record == null || record.Split(',').ToList().Count < column_index)
                    {
                        continue;
                    }

                    fields = record.Split(',').ToList();
                    
                    field = fields[column_index].Trim().ToLower();

                    if(field.Equals(search_key))
                    {
                        found = true;
                        record = "";

                        foreach(string data in fields)
                        {
                            record += data.Trim() + ",";
                        }

                        Console.WriteLine(record);
                        break;
                    }
                }
            }

            if(!found)
            {
                Console.WriteLine("\"" + search_key + "\"" + " not found.");
            }
        }
    }
}