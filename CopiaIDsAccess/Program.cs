using System;
using System.Data.OleDb;
using System.IO;

namespace CopiaIDsAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Recuerde, escriba CopiaIDsAccess.exe [horario antiguo con identificadores].mdb [horario nuevo sin identificadores].mdb");
                return;
            }

            string source = args[0];

            File.Delete("estructura-output.mdb");
            File.Copy(args[1],"estructura-output.mdb");

            string destination = "estructura-output.mdb";

            string mACCESSDBconnectionStringOrigin = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\" + source;
            string mACCESSDBconnectionStringDestination = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Directory.GetCurrentDirectory() + "\\" + destination;

             /*using (OleDbConnection MyConnDestinationAlterTable = new OleDbConnection(mACCESSDBconnectionStringDestination))
             {
                MyConnDestinationAlterTable.Open();
                OleDbCommand cmdAltertable = new OleDbCommand("ALTER TABLE Horarios ADD COLUMN IdProfesor NUMBER;", MyConnDestinationAlterTable); ;
                cmdAltertable.ExecuteNonQuery();                       
             }*/

            //mACCESSDBconnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Documents\\Visual Studio 2015\\Projects\\CampanillasControlPrototype\\CampanillasControlPrototype\\DB\\estructura.mdb;";
            
            using (OleDbConnection MyConnSource = new OleDbConnection(mACCESSDBconnectionStringOrigin))
            {
                    MyConnSource.Open();
                    OleDbCommand Cmd = new OleDbCommand("SELECT IdProfesor,Profesorado FROM Horarios;", MyConnSource); ;
                    OleDbDataReader ObjReader = Cmd.ExecuteReader();
                    using (OleDbConnection MyConnDestination = new OleDbConnection(mACCESSDBconnectionStringDestination))
                    {
                        MyConnDestination.Open();
                        while (ObjReader.Read())
                        {
                            if (ObjReader[0].ToString().Length > 0)
                            {
                                OleDbCommand Cmd2 = new OleDbCommand("UPDATE Horarios SET IdProfesor = \"" + ObjReader[0] + "\" WHERE Profesorado = \"" + ObjReader[1] + "\";", MyConnDestination); ;
                                Cmd2.ExecuteNonQuery();
                                Console.WriteLine("Actualizado Id de profesor " + ObjReader[0] + " a " + ObjReader[1]);
                            }
                        }
                    }
            }

        }
    }
}
