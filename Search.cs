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
            string output;
            int column_index;
            bool found;

            //Checks if 3 arguments has been passed or not
            if(args.Length != 3)
            {
                Console.WriteLine("Expected: Search.exe <csv_file_path> <column_index> <search_key>");
                return;
            }

            //Assigns the csv_file_path
            csv_file_path = args[0];

            //Checks whether the csv file exists within the passed file path
            if(!File.Exists(csv_file_path))
            {
                Console.WriteLine("File path " + "\"" + csv_file_path + "\"" + " cannot be found.");
                return;
            }

            //Checks whether the second argument is an integer
            if(!int.TryParse(args[1], out column_index))
            {
                Console.WriteLine("Invalid " + "\"" + args[1] + "\"" + " <column_index> ") ;
                return;
            }

            //Assigns the column_index
            column_index = int.Parse(args[1]);

            //Assigns the search_key
            search_key = args[2].ToLower();

            //Flag to check whether the search_key has been found inside the csv file
            found = false;
        
            //Opens the csv file and reads the content
            using(StreamReader streamReader = new StreamReader(csv_file_path))
            {
                //Reads the csv file till end of file
                while(streamReader.EndOfStream == false)
                {
                    //Reads the current line
                    record = streamReader.ReadLine().Trim();
                    output = record;

                    //Checks whether the line is empty or column_index is out of range
                    if(record == "" || record == null || record.Split(',').ToList().Count < column_index)
                    {
                        continue;
                    }

                    //Removes the ";" at the end of the line
                    record = record.Remove(record.IndexOf(";"));

                    //Converted the line to a list
                    fields = record.Split(',').ToList();
                    
                    //Assigns the field with the given column index
                    field = fields[column_index].Trim();

                    //Checks whether the field equals to the search_key
                    if(field.ToLower().Equals(search_key.ToLower()))
                    {
                        //Sets flag to true
                        found = true;

                        //Returns the line
                        Console.WriteLine(output);
                        break;
                    }
                }
            }

            //Checks whether search_key has been found or not
            if(!found)
            {
                Console.WriteLine("\"" + search_key + "\"" + " not found.");
            }
        }
    }
}