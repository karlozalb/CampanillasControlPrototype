using CampanillasControlPrototype;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace MassiveTesting
{
    class Program
    {
        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
        public extern static void Win32GetSystemTime(ref SystemTime sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);

        public static void setTime(ushort pyear, ushort pmonth, ushort pday, ushort phour, ushort pminute, ushort pdayofweek)
        {
            SystemTime st = new SystemTime
            {
                Year = pyear,
                Month = pmonth,
                Day = pday,
                Hour = (ushort)(phour-2),
                Minute = pminute,
                DayOfWeek = pdayofweek
            };
            Win32SetSystemTime(ref st);

            Console.WriteLine("Hora y fecha cambiada a: "+DateTime.Now);           
        }

        static TestingParadoxDBController dbController;
        static AccessScheduleDBController accessController;

        static void Main(string[] args)
        {
            dbController = new TestingParadoxDBController();
            dbController.clearClockins();

            accessController = new AccessScheduleDBController();

            Thread t = new Thread(new ThreadStart(threadTask2));
            t.Start();             
        }       

        public static void wait()
        {
            Console.WriteLine("Espera de 25 segundos");

            Thread.Sleep(25000);
        }

        public static void threadTask2()
        {
            setTime(2015, 9, 7, 7, 42, 1);
            wait();

            //Entradas a las 7:42 (Pre guardia de transportes)
            checkIn(10, "ELEmo"); //33 minutos de antelación
            checkIn(11, "ELEpo");
            checkIn(12, "ENRcr");
            checkIn(14, "FRram");
            checkIn(17, "JmNAV");
            wait();

            setTime(2015, 9, 7, 7, 44, 1);
            checkIn(500, "Walter Guardia"); //1 minuto de antelación o 0

            //GUARDIA DE TRANSPORTES 1

            setTime(2015, 9, 7, 7, 45, 1); // GT1 con espera <===============================
            wait();

            setTime(2015, 9, 7, 7, 47, 1);
            checkIn(304, "Pepito Directivo"); //2 minutos de retraso
            wait();


            setTime(2015, 9, 7, 8, 5, 1);
            checkIn(19, "MaDOM"); //LEgan con 10 minutos de adelanto
            checkIn(21, "MANjP");
            checkIn(22, "MaSOL");
            checkIn(23, "McVER");
            wait();

            setTime(2015, 9, 7, 8, 14, 1);
            checkIn(24, "MdLAC"); //LLegan con 0-1 minutos de adelanto
            checkIn(25, "MdROG");
            checkIn(27, "MjALA");
            checkIn(29, "Msanj");
            wait();

            //PRIMERA HORA

            //Faltas en esta hora: AjesG, ALFgo, BEGlo, BELla

            setTime(2015, 9, 7, 8, 17, 1);
            wait();

            checkIn(30, "MsSAS"); //Llegan con dos minutos de retraso
            checkIn(31, "RAQme");
            checkIn(501, "Roger Guardia");
            checkIn(502, "James Guardia"); 
            checkIn(300, "Carlitos Directivo");
            wait();

            setTime(2015, 9, 7, 9, 08, 1);
            checkIn(3, "ALFgo"); //Entran con 7 minutos de antelación
            checkIn(6, "CARdo");
            checkIn(7, "CARfP");
            wait();

            //SEGUNDA HORA

            //Faltas en esta hora: CLEME
            //Faltas acumuladsa: ALFgo, BEGlo, BELla

            setTime(2015, 9, 7, 9, 26, 1);
            checkIn(13, "ESTRc"); //Entran 10 minutos tarde
            checkIn(15, "IGNgF");
            checkIn(20, "MAgar");
            checkIn(2, "AjesG"); //Llega 1 hora 11 minutos tarde.
            wait();

            setTime(2015, 9, 7, 10, 10, 1);
            checkIn(37, "VICTO"); //Llegan 5 minutos antes.
            checkIn(28, "MlMAT");
            checkIn(1, "ADRfa");
            checkIn(16, "JaGAL");
            wait();

            //TERCERA HORA

            //Faltas acumuladsa: CLEME,ALFgo, BEGlo, BELla

            setTime(2015, 9, 7, 10, 18, 1);
            wait();

            checkIn(26, "Misab"); //Llegan 3 minutos tarde.
            checkIn(35, "SONIo");

            checkIn(503, "Jon Guardia");
            checkIn(302, "Juanito Directivo");
            checkIn(303, "Luisito Directivo");

            checkIn(22, "MaSOL"); //Salida, acabó en segunda

            wait();

            //RECREO
            setTime(2015, 9, 7, 11, 18, 1);
            wait();

            setTime(2015, 9, 7, 11, 39, 1);
            checkIn(36, "VICfr"); //Llegan con 6 minutos de antelación
            checkIn(504, "Michael Guardia");
            checkIn(10, "ELEmo"); //Salida, acaba a tercera.
            checkIn(24, "MdLAC"); //Salida, acaba a tercera.
            checkIn(30, "MsSAS"); //Salida, acaba a tercera.

            checkIn(1, "ADRfa"); //Salida, acaba a tercera pero entra a quinta.

            wait();

            //Faltas acumuladsa: CLEME,ALFgo, BEGlo, BELla

            //CUARTA HORA
            setTime(2015, 9, 7, 11, 45, 1);
            wait();

            checkIn(300, "Carlitos Directivo"); //Salida, acababa en el recreo.
            checkIn(302, "Juanito Directivo"); //Salida, acababa en el recreo.
            checkIn(12, "ENRcr"); //Salida, acababa en la tercera hora.

            setTime(2015, 9, 7, 12, 39, 1);
            checkIn(18, "JmOST"); //Llega con 6 minutos de antelación
            checkIn(1, "ADRfa"); //Entra a quinta de nuevo (salió a descansar) con 6 minutos de antelación.
            wait();

            //QUINTA HORA
            setTime(2015, 9, 7, 12, 45, 1);
            wait();
            checkIn(33, "SANDA"); //Llega justo

            checkIn(17, "JmNAV"); //Salida, salia a cuarta
            checkIn(21, "MANjP"); //Salida, salia a cuarta
            checkIn(25, "MdROG"); //Salida, salia a cuarta
            checkIn(504, "Michael Guardia"); //Salida, salia a cuarta
            checkIn(27, "MjALA"); //Salida, salia a cuarta
            checkIn(31, "RAQme"); //Salida, salia a cuarta

            checkIn(6, "CARdo"); //Salida, salia a cuarta y entra a sexta

            setTime(2015, 9, 7, 13, 42, 1);
            checkIn(6, "CARdo"); //Entra de nuevo a a sexta hora con 3 minutos de antelación

            //SEXTA HORA
            setTime(2015, 9, 7, 13, 45, 1);
            wait();

            checkIn(80, "ANGEs"); //Llega justo

            setTime(2015, 9, 7, 13, 49, 1);
            //Fichajes de salida
            checkIn(1, "ADRfa"); //SALIDA Sale a quinta.
            checkIn(7, "CARfP"); //SALIDA Sale a quinta.
            checkIn(15, "IGNgF"); //SALIDA Sale a quinta.
            checkIn(503, "Jon Guardia"); //SALIDA Sale a quinta.
            checkIn(26, "Misab"); //SALIDA Sale a quinta.
            checkIn(29, "Msanj"); //SALIDA Sale a quinta.
            checkIn(36, "VICfr"); //SALIDA Sale a quinta.
            checkIn(37, "VICTO"); //SALIDA Sale a quinta.

            //GUARDIA DE TRANSPORTES 2
            setTime(2015, 9, 7, 14, 45, 1);
            wait();

            setTime(2015, 9, 7, 14, 48, 1);
            checkIn(80, "ANGEs"); //SALIDA Sale a sexta.
            checkIn(6, "CARdo"); //SALIDA Sale a sexta.
            checkIn(11, "ELEpo"); //SALIDA Sale a sexta.
            checkIn(13, "ESTRc"); //SALIDA Sale a sexta.
            checkIn(14, "FRram"); //SALIDA Sale a sexta.
            checkIn(16, "JaGAL"); //SALIDA Sale a sexta.
            checkIn(502, "James Guardia"); //SALIDA Sale a sexta.
            checkIn(18, "JmOST"); //SALIDA Sale a sexta.
            checkIn(19, "MaDOM"); //SALIDA Sale a sexta.
            checkIn(20, "MAgar"); //SALIDA Sale a sexta.
            checkIn(23, "McVER"); //SALIDA Sale a sexta.
            checkIn(28, "MlMAT"); //SALIDA Sale a sexta.

            //Se olvidan de fichar a la salida (para pruebas de fichajes impares).

            //checkIn(501, "Roger Guardia"); //SALIDA Sale a sexta.
            //checkIn(33, "SANDa"); //SALIDA Sale a sexta.
            //checkIn(35, "SONIo"); //SALIDA Sale a sexta.
            //checkIn(500, "Walter Guardia"); //SALIDA Sale a sexta.

            setTime(2015, 9, 7, 15, 20, 1);
            wait();
            checkIn(304, "Pepito Directivo"); //SALIDA Sale a sexta.

            //Faltas al final de día: CLEME,ALFgo, BEGlo, BELla
        }

        public static void checkIn(int pid,string pname)
        {
            dbController.insertTESTClockIn(pid); //Entrada
            Console.WriteLine(DateTime.Now.ToShortTimeString()+" - ClockIn: "+pid+" "+pname);
        }        

        public static void threadTask()
        {
            Console.WriteLine("Esperando 40 segundos para iniciar");
            setTime(2015, 9, 7, 7, 45, 1);
            Thread.Sleep(40000);
            //Lunes 7 de septiembre de 2015
            //Guardia de transporte (500-Walter guardia y 305-Pepito Directivo a primera hora).
            /*********************************************************************************/
            Console.WriteLine("1 - Tests de guardia de transportes");
            Console.WriteLine("***************************************************************");
            dbController.insertTESTClockIn(304); //Entrada
            Console.WriteLine("Entrada 304");
            ///7:48
            setTime(2015, 9, 7, 7, 48, 1);
            dbController.insertTESTClockIn(500); //Entrada
            Console.WriteLine("Entrada 500");
            Thread.Sleep(35000);
            //8:05
            setTime(2015, 9, 7, 8, 05, 1);
            dbController.insertTESTClockIn(304); //Salida
            Console.WriteLine("Salida 304");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            //8:13
            setTime(2015, 9, 7, 8, 13, 1);
            dbController.insertTESTClockIn(304); //Entrada
            Console.WriteLine("Entrada 304");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            Console.WriteLine("2 - Tests de primera hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 8, 15, 1);
            //Primera hora
            /*********************************************************************************/
            dbController.insertTESTClockIn(19); //Entrada MaDOM
            Console.WriteLine("Entrada MaDOM");
            dbController.insertTESTClockIn(3); //Entrada ALFgo
            Console.WriteLine("Entrada ALFgo");
            dbController.insertTESTClockIn(31); //Entrada RAQme
            Console.WriteLine("Entrada RAQme");
            dbController.insertTESTClockIn(22); //Entrada MaSOL           
            Console.WriteLine("Entrada MaSOL");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 8, 19, 1);
            dbController.insertTESTClockIn(14); //Entrada FRram
            Console.WriteLine("Entrada FRram");
            dbController.insertTESTClockIn(30); //Entrada MsSAS
            Console.WriteLine("Entrada MsSAS");
            dbController.insertTESTClockIn(12); //Entrada ENRcr
            Console.WriteLine("Entrada ENRcr");
            dbController.insertTESTClockIn(21); //Entrada MANjP
            Console.WriteLine("Entrada MANjP");
            dbController.insertTESTClockIn(25); //Entrada MdROG
            Console.WriteLine("Entrada MdROG");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 8, 21, 1);
            dbController.insertTESTClockIn(10); //Entrada ELEmo
            Console.WriteLine("Entrada ELEmo");
            dbController.insertTESTClockIn(27); //Entrada MjALA
            Console.WriteLine("Entrada MjALA");
            dbController.insertTESTClockIn(23); //Entrada McVER
            Console.WriteLine("Entrada McVER");
            dbController.insertTESTClockIn(17); //Entrada JmNAV
            Console.WriteLine("Entrada JmNAV");
            dbController.insertTESTClockIn(11); //Entrada ELEpo
            Console.WriteLine("Entrada ELEpo");
            //Faltan a primera hora: BELIa,AjesG,Msanj,BEGIo y MdLAC.
            Console.WriteLine("Faltan a primera hora: BELIa,AjesG,Msanj,BEGIo y MdLAC");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 9, 10, 1);
            dbController.insertTESTClockIn(17); //SALIDA JmNAV
            Console.WriteLine("Salida JmNAV");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            // Test de la segunda hora.
            /*********************************************************************************/
            Console.WriteLine("3 - Tests de segunda hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 9, 15, 1);
            Console.WriteLine("Espera de 35 segundos");
            Console.WriteLine("Ausencias acumuladas: BELIa,AjesG,Msanj,BEGIo y MdLAC");
            Console.WriteLine("Nueva ausencia (aparte de las nuevas de esta hora): JmNAV,ESTRc,CARfP");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);            
            dbController.insertTESTClockIn(501); //Entrada Roger Guardia
            Console.WriteLine("Entrada Roger Guardia");
            dbController.insertTESTClockIn(20); //Entrada MAGAR
            Console.WriteLine("Entrada MAGAR");
            dbController.insertTESTClockIn(15); //Entrada IGNgF
            Console.WriteLine("Entrada IGNgF");
            dbController.insertTESTClockIn(8); //Entrada CLEME
            Console.WriteLine("Entrada CLEME");
            dbController.insertTESTClockIn(6); //Entrada CARDO
            Console.WriteLine("Entrada CARDO");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 9, 19, 1);
            dbController.insertTESTClockIn(502); //Entrada James Guardia
            Console.WriteLine("Entrada James Guardia");
            dbController.insertTESTClockIn(17); //Entrada JmNAV
            Console.WriteLine("Entrada JmNAV");
            dbController.insertTESTClockIn(5); //Entrada BELla
            Console.WriteLine("Entrada BELla");
            dbController.insertTESTClockIn(29); //Entrada Msanj
            Console.WriteLine("Entrada Msanj con retraso de 64 minutos.");
            Console.WriteLine("Faltas NUEVAS a segunda hora: ESTRc,CARfP"); 
            Console.WriteLine("Faltas ACUMULADAS a segunda hora: AjesG,BEGIo y MdLAC");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            // Test de la tercera hora.
            /*********************************************************************************/
            Console.WriteLine("4 - Tests de tercera hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 10, 15, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            dbController.insertTESTClockIn(503); //Entrada Jon Guardia
            Console.WriteLine("Entrada Jon Guardia");
            dbController.insertTESTClockIn(13); //Entrada ESTRc
            Console.WriteLine("Entrada ESTRc");
            dbController.insertTESTClockIn(37); //Entrada VICTO
            Console.WriteLine("Entrada VICTO");
            dbController.insertTESTClockIn(35); //Entrada SONIo
            Console.WriteLine("Entrada SONIo"); 
            dbController.insertTESTClockIn(28); //Entrada MlMAT
            Console.WriteLine("Entrada MlMAT");
            setTime(2015, 9, 7, 10, 21, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            dbController.insertTESTClockIn(16); //Entrada JAGAL
            Console.WriteLine("Entrada JAGAL");
            dbController.insertTESTClockIn(1); //Entrada ADRFa
            Console.WriteLine("Entrada ADRFa");
            dbController.insertTESTClockIn(302); //Entrada Juanito Directivo
            Console.WriteLine("Entrada Juanito Directivo");            
            dbController.insertTESTClockIn(303); //Entrada Luisito Directivo
            Console.WriteLine("Entrada Luisito Directivo");
            Console.WriteLine("Faltas NUEVAS a tercera hora: Jon Guardia,Misab");
            Console.WriteLine("Faltas ACUMULADAS a tercera hora: CARfP,AjesG,BEGIo y MdLAC");
            Console.WriteLine("Espera de 35 segundos");
            dbController.insertTESTClockIn(503); //Salida Jon Guardia
            Console.WriteLine("Salida Jon Guardia");
            dbController.insertTESTClockIn(22); //Salida MaSOL
            Console.WriteLine("Salida MaSOL");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            // Test del recreo hora.
            /*********************************************************************************/
            Console.WriteLine("5 - Tests del recreo");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 11, 15, 1);
            Console.WriteLine("Espera de 35 segundos");
            Console.WriteLine("Entrada VICFr (Entra despues del recreo, antes de su hora)");

            //Salidas
            dbController.insertTESTClockIn(8); //Salida CLEME
            Console.WriteLine("Salida CLEME que habia acabado a segunda hora");
            dbController.insertTESTClockIn(10); //Salida ELEmo
            Console.WriteLine("Salida ELEmo que había acabado a tercera hora");
            dbController.insertTESTClockIn(12); //Salida ENRcr
            Console.WriteLine("Salida ENRcr que había acabado a tercera hora");
            dbController.insertTESTClockIn(30); //Salida MsSAS
            Console.WriteLine("Salida MsSAS que había acabado a tercera hora");

            dbController.insertTESTClockIn(36); //Entrada VICFr
            Console.WriteLine("Entrada Michael Guardia (Entra despues del recreo, antes de su hora)");
            dbController.insertTESTClockIn(504); //Entrada Michael Guardia
            Console.WriteLine("Entrada Jon Guardia (Entra despues del recreo, antes de su hora pero ha salido antes de tiempo la hora anterior)");
            dbController.insertTESTClockIn(503); //Entrada Jon Guardia
            Console.WriteLine("Faltas NUEVAS en el recreo: Carlitos Directivo, Jon Guardia");
            Console.WriteLine("Faltas ACUMULADAS a tercera hora:Misab,CARfP,AjesG,BEGIo y MdLAC");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            //CUARTA
            Console.WriteLine("6 - Tests de la cuarta hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 11, 45, 1);

            //Salidas del recreo
            dbController.insertTESTClockIn(302); //Salida Juanito Directivo
            Console.WriteLine("Salida Juanito Directivo que acabó despues del recreo");
            dbController.insertTESTClockIn(303); //Salida Luisito Directivo
            Console.WriteLine("Salida Luisito Directivo que acabó despues del recreo");
            dbController.insertTESTClockIn(24); //Salida MdLAC
            Console.WriteLine("Salida MdLAC que acabó a tercera hora");

            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            Console.WriteLine("Faltas NUEVAS en la cuarta hora: nada");
            Console.WriteLine("Faltas ACUMULADAS a cuarta hora:Misab,CARfP,AjesG,BEGIo y MdLAC");

            //QUINTA
            Console.WriteLine("7 - Tests de la quinta hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 12, 45, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            dbController.insertTESTClockIn(7); //Entrada CARfP
            Console.WriteLine("Entrada CARfP muy tarde (debió entrar a segunda hora)");
            dbController.insertTESTClockIn(18); //Entrada JMost
            Console.WriteLine("Entrada JMost");
            dbController.insertTESTClockIn(33); //Entrada SANDa
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 12, 49, 1);

            //Salidas cuarta hora
            dbController.insertTESTClockIn(17); //Salida JmNAV
            Console.WriteLine("Salida JmNAV que acabó a cuarta hora");
            dbController.insertTESTClockIn(21); //Salida MANjP
            Console.WriteLine("Salida MANjP que acabó a cuarta hora");
            dbController.insertTESTClockIn(27); //Salida MjALA
            Console.WriteLine("Salida MjALA que acabó a cuarta hora");


            Console.WriteLine("Entrada SANDa");
            Console.WriteLine("Faltas NUEVAS en la quinta hora: nada");
            Console.WriteLine("Faltas ACUMULADAS a quinta hora:Misab,AjesG,BEGIo y MdLAC");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            setTime(2015, 9, 7, 13, 35, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            dbController.insertTESTClockIn(4); //Entrada ANGEs
            Console.WriteLine("Entrada ANGEs 10 minutos antes de su hora");
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);


            //SEXTA
            Console.WriteLine("8 - Tests de la sexta hora");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 13, 45, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);            
            //Fichajes de salida
            dbController.insertTESTClockIn(29); //Salida Msanj
            Console.WriteLine("Salida Msanj");
            dbController.insertTESTClockIn(1); //Salida ADRfa
            Console.WriteLine("Salida ADRfa");
            dbController.insertTESTClockIn(3); //Salida ALFgo
            Console.WriteLine("Salida ALFgo");
            dbController.insertTESTClockIn(7); //Salida CARfP

            setTime(2015, 9, 7, 13, 47, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            Console.WriteLine("Salida CARfP");
            dbController.insertTESTClockIn(15); //Salida IGNgF
            Console.WriteLine("Salida IGNgF");
            dbController.insertTESTClockIn(503); //Salida Jon Guardia
            Console.WriteLine("Salida Jon Guardia");
            dbController.insertTESTClockIn(25); //Salida MdROG
            Console.WriteLine("Salida MdROG que acabó a cuarta hora");
            dbController.insertTESTClockIn(29); //Salida Msanj
            Console.WriteLine("Salida Msanj");

            setTime(2015, 9, 7, 13, 54, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            dbController.insertTESTClockIn(31); //Salida RAQme
            Console.WriteLine("Salida RAQme");
            dbController.insertTESTClockIn(36); //Salida VICfr
            Console.WriteLine("Salida VICfr");
            dbController.insertTESTClockIn(504); //Salida Michael Guardia
            Console.WriteLine("Salida Michael Guardia que acabó a cuarta hora");

            //SEXTA
            Console.WriteLine("9 - Guardia de transporte 2");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 14, 45, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            //Salidas al final
            dbController.insertTESTClockIn(5); //Salida BELla
            Console.WriteLine("Salida BELla");
            dbController.insertTESTClockIn(6); //Salida BELla
            Console.WriteLine("Salida CARdo");
            dbController.insertTESTClockIn(11); //Salida ELEpo
            Console.WriteLine("Salida ELEpo");
            dbController.insertTESTClockIn(13); //Salida ESTRc
            Console.WriteLine("Salida ESTRc");

            setTime(2015, 9, 7, 14, 54, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            dbController.insertTESTClockIn(14); //Salida FRram
            Console.WriteLine("Salida FRram");
            dbController.insertTESTClockIn(16); //Salida JaGAL
            Console.WriteLine("Salida JaGAL");
            dbController.insertTESTClockIn(502); //Salida JaGAL
            Console.WriteLine("Salida James Guardia");
            dbController.insertTESTClockIn(18); //Salida JmOST
            Console.WriteLine("Salida JmOST");

            setTime(2015, 9, 7, 14, 57, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            dbController.insertTESTClockIn(19); //Salida MaDOM
            Console.WriteLine("Salida MaDOM");
            dbController.insertTESTClockIn(20); //Salida MAgar
            Console.WriteLine("Salida MAgar");
            dbController.insertTESTClockIn(23); //Salida McVER
            Console.WriteLine("Salida McVER");
            dbController.insertTESTClockIn(28); //Salida MlMAT
            Console.WriteLine("Salida MlMAT");

            setTime(2015, 9, 7, 15, 01, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);

            dbController.insertTESTClockIn(33); //Salida SANDa
            Console.WriteLine("Salida SANDa");
            dbController.insertTESTClockIn(35); //Salida SONIo
            Console.WriteLine("Salida SONIo");
            dbController.insertTESTClockIn(37); //Salida VICTO
            Console.WriteLine("Salida VICTO");

            Console.WriteLine("Faltas NUEVAS en la sexta hora: nada");
            Console.WriteLine("Faltas ACUMULADAS a sexta hora:Misab,AjesG,BEGIo y MdLAC");
            Thread.Sleep(35000);

            //SEXTA
            Console.WriteLine("10 - OFFTIME");
            Console.WriteLine("***************************************************************");
            setTime(2015, 9, 7, 15, 18, 1);
            Console.WriteLine("Espera de 35 segundos");
            Thread.Sleep(35000);
            dbController.insertTESTClockIn(304); //Pepito Directivo
            Console.WriteLine("Salida Pepito Directivo");

            Console.WriteLine("Faltas:Misab,AjesG,BEGIo y MdLAC");

            Console.WriteLine("Fin de los tests, pulsa una tecla para finalizar.");
            Console.ReadLine();
        }
    }

   
}
