// #define DEF_PLUS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.ArrayExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

//using static System.Console;

namespace ConsoleApplication1
{
    class Program
    {
        public static int DEF_MAX_HORSE = 10;
        public static int DEF_SLEEP_TIME = 1;

        public const int HEART_1 = 0;
        public const int HEART_2 = 1;
        public const int HEART_3 = 2;
        public const int HEART_4 = 3;
        public const int HEART_5 = 4;
        public const int MAX_HEART_SIZE = 5;
        
        static void CallHeart1(bool[] inarray, out bool[] outarray)
        {
            outarray = new bool[MAX_HEART_SIZE];

            for (int i = 0; i < MAX_HEART_SIZE; i++)
            {
                if(inarray?[i] == false)
                {
                    outarray[i] = true;
                }

                Console.WriteLine("{0} : inarray = {1}, outarray = {2}", i, inarray[i].ToString(), outarray[i].ToString());
            }

            
        }

        [Serializable]
        public struct Student1
        {
            public string name;
            public string korean;
            public string math;

            public Student1(string name, string korean, string math)
            {
                this.name = name;
                this.korean = korean;
                this.math = math;

                //Console.WriteLine("struct Test's construction");
            }

            public override string ToString()
            {
                //return string.Format("{0}'s score : korean : {1}, math : {2}", name, korean, math);
                return @"{name}'s score : korean : {korean}, math : {math}";
            }
        }

        unsafe public struct SUnsafeTest
        {
            public fixed int Field1[4];
            public fixed int Field2[4];
        }

        public class CCapsule
        {
            public SUnsafeTest sUnsafe;
        }

        public const int DEF_MAX_CYLINDER_SENSOR = 4;
        public enum ECylinderType
        {
            UP_DOWN,
            LEFT_RIGHT,
            FRONT_BACK,
            UPSTREAM_DOWNSTREAM,
            CW_CCW,
            OPEN_CLOSE,
            UP_MID_DOWN,
            LEFT_MIDE_RIGHT,
            FRONT_MID_BACK,
            UPSTREAM_MID_DOWNSTREAM,
            UPSTREAM_DOWNSTREAM_VARIOUS_VELOCITY,
            UPSTREAM_MID_DOWNSTREAM_VARIOUS_VELOCITY,
        };

        public enum ESolenoidType
        {
            SINGLE_SOLENOID,
            REVERSE_SINGLE_SOLENOID,
            DOUBLE_SOLENOID,
            DOUBLE_3WAY_SOLENOID,
            DOUBLE_SOLENOID_VARIOUS_VELOCITY
        };

        public class CCylinderData : ICloneable
        {
            // Cylinder의 ID를 지정한다. ID에 따라서 어디에서 사용하는 Cylinder인지가 결정된다.
            public int ID;

            // 생성된 Cylinder 객체와 연관된 Solenoid 단동식일때는 하나 사용, 복동식일때는 2개 사용 
            public int[] Solenoid = new int[2];

            // Up Sensor  : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] UpSensor = new int[DEF_MAX_CYLINDER_SENSOR];

            // Down Sensor : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] DownSensor = new int[DEF_MAX_CYLINDER_SENSOR];

            // @param dMovingTime : Cylinder 이동시 걸리는 최대 시간
            public double MovingTime;

            // @link aggregation Cylinder 타입
            public ECylinderType CylinderType;

            // @link aggregation Solenoid 타입
            public ESolenoidType SolenoidType;

            public CCylinderData()
            {

            }

            protected CCylinderData(CCylinderData that)
            {
                this.MovingTime = that.MovingTime;
                this.UpSensor = that.UpSensor.Clone() as int[];
            }

            public object Clone()
            {
                return new CCylinderData(this);
            }
        }

        [Serializable]
        public class DCylinderData
        {
            public string t1;
            public string t2;
            public int ID;

            public int[] Solenoid = new int[2];
            public int[] UpSensor = new int[DEF_MAX_CYLINDER_SENSOR];
            public int[] DownSensor = new int[DEF_MAX_CYLINDER_SENSOR];

            public double MovingTime { get; set; }
            private double MovingTime2;

            public ECylinderType CylinderType;
            public ESolenoidType SolenoidType;

            public bool[] boolTest = new bool[3];
            public string[] nameTest = new string[4];
            public int[,] TwoDimension = new int[3,4];

            public DCylinderData()
            {
                t1 = "test";
            }

            public void SetMovingTime2(double d)
            {
                MovingTime2 = d;
            }
        }

        public class DCylinderData_Property
        {
            // Cylinder의 ID를 지정한다. ID에 따라서 어디에서 사용하는 Cylinder인지가 결정된다.
            public int ID { get; set; }

            // 생성된 Cylinder 객체와 연관된 Solenoid 단동식일때는 하나 사용, 복동식일때는 2개 사용 
            public int[] Solenoid { get; set; }

            // Up Sensor  : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] UpSensor { get; set; }

            // Down Sensor : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] DownSensor { get; set; }

            // @param dMovingTime : Cylinder 이동시 걸리는 최대 시간
            public double MovingTime { get; set; }

            // @link aggregation Cylinder 타입
            public ECylinderType CylinderType { get; set; }

            // @link aggregation Solenoid 타입
            public ESolenoidType SolenoidType { get; set; }

            public DCylinderData_Property()
            {
                Solenoid = new int[2];
                UpSensor = new int[DEF_MAX_CYLINDER_SENSOR];
                DownSensor = new int[DEF_MAX_CYLINDER_SENSOR];

            }
        }

        public struct SCylinderData
        {
            // Cylinder의 ID를 지정한다. ID에 따라서 어디에서 사용하는 Cylinder인지가 결정된다.
            public int ID;

            // 생성된 Cylinder 객체와 연관된 Solenoid 단동식일때는 하나 사용, 복동식일때는 2개 사용 
            public int[] Solenoid/* = new int[2]*/;

            // 생성된 Cylinder 객체와 연관된 가감속 Solenoid  +,- 방향 1개씩  
            public int[] AccSolenoid/* = new int[2]*/;

            // Up Sensor  : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] UpSensor/* = new int[DEF_MAX_CYLINDER_SENSOR]*/;

            // Down Sensor : 체크하고자 하는 갯수 만큼 지정 하고 나머지는 NULL로 한다.
            public int[] DownSensor/* = new int[DEF_MAX_CYLINDER_SENSOR]*/;

            // Middle Sensor : 등록된 Sensor들의 상태 체크 
            public int[] MiddleSensor/* = new int[DEF_MAX_CYLINDER_SENSOR]*/;

            // 가감속 센서 : +방향, - 방향 2개 밖에 지정할 수 없다.
            public int[] AccSensor/* = new int[2]*/;

            // @param dMovingTime : Cylinder 이동시 걸리는 최대 시간
            public double MovingTime;

            // @param dNoSenMovingTIme : Cylinder 이동시 Sensor가 없을때의 Moving Time	
            public double NoSenMovingTime;

            // dSettlingTime : Cylinder가 1동작후 안정화 되는데 걸리는 시간
            public double SettlingTime1;

            // dSettlingTime : Cylinder가 2동작후 안정화 되는데 걸리는 시간
            public double SettlingTime2;

            // @link aggregation Cylinder 타입
            public ECylinderType CylinderType;

            // @link aggregation Solenoid 타입
            public ESolenoidType SolenoidType;

            public int[,] TwoDimension;

            public SCylinderData(int obsolete) : this()
            {
                Solenoid     = new int[2];
                AccSolenoid  = new int[2];
                UpSensor     = new int[DEF_MAX_CYLINDER_SENSOR];
                DownSensor   = new int[DEF_MAX_CYLINDER_SENSOR];
                MiddleSensor = new int[DEF_MAX_CYLINDER_SENSOR];
                AccSensor    = new int[2];
                TwoDimension = new int[3, 4];
            }
        }

        public class CCapsuleStructure
        {
            public SCylinderData CylinderData;

            public CCapsuleStructure()
            {
                CylinderData = new SCylinderData(1);
            }
        }

        public struct SCapsuleStructure
        {
            public SCylinderData CylinderData;

            public SCapsuleStructure(int obsolete) : this()
            {
                CylinderData = new SCylinderData(1);
            }
        }

        public struct SLayer1
        {
            public int Member1;
            public int[,] Array1;

            public SLayer1(int obsolete) : this()
            {
                Array1 = new int[2,2];
            }

            public override string ToString()
            {
                return $"Member1 = {Member1,2}, Array1 = {{{{{Array1[0,0],2},{Array1[0,1],2}}},{{{Array1[1,0],2},{Array1[1,1],2}}}}}";
            }
        }

        public struct SLayer2
        {
            public int Member1;
            public int[,] Array1;
            public SLayer1 Child1;

            public SLayer2(int obsolete) : this()
            {
                Array1 = new int[2,2];
                Child1 = new SLayer1(1);
            }

            public override string ToString()
            {
                return $"Member1 = {Member1,2}, Array1 = {{{{{Array1[0, 0],2},{Array1[0, 1],2}}},{{{Array1[1, 0],2},{Array1[1, 1],2}}}}}, Child1 : {Child1}";
            }
        }


        //// DeepCopy() has problem
        //static public T DeepCopy<T>(T obj)
        //{
        //    BinaryFormatter s = new BinaryFormatter();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        s.Serialize(ms, obj);
        //        ms.Position = 0;
        //        T t = (T)s.Deserialize(ms);

        //        return t;
        //    }
        //}

        public static T DeepCopyStruct<T>(T source) where T : struct
        {
            return (T)DeepCopyStruct(source, typeof(T));
        }

        public static object DeepCopyStruct(object anything, Type anyType)
        {
            return RawDeserialize(RawSerialize(anything), 0, anyType);
        }

        /* Source: http://bytes.com/topic/c-sharp/answers/249770-byte-structure */
        public static object RawDeserialize(byte[] rawData, int position, Type anyType)
        {
            int rawsize = Marshal.SizeOf(anyType);
            if (rawsize > rawData.Length)
                return null;
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(rawData, position, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, anyType);
            Marshal.FreeHGlobal(buffer);
            return retobj;
        }

        /* Source: http://bytes.com/topic/c-sharp/answers/249770-byte-structure */
        public static byte[] RawSerialize(object anything)
        {
            int rawSize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawSize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawDatas = new byte[rawSize];
            Marshal.Copy(buffer, rawDatas, 0, rawSize);
            Marshal.FreeHGlobal(buffer);
            return rawDatas;
        }

        public static void SetValueDirect(TypedReference tr, FieldInfo fieldInfo, string value)
        {
            string keyType = fieldInfo.FieldType.Name;
            switch (keyType)
            {
                case "Int32":
                    int n;
                    if (Int32.TryParse(value, out n))
                    {
                        fieldInfo.SetValueDirect(tr, n);
                    }
                    break;

                case "Double":
                    double d;
                    if (Double.TryParse(value, out d))
                    {
                        fieldInfo.SetValueDirect(tr, d);
                    }
                    break;
            }
        }

        public static void SetFieldValue(Object target, FieldInfo fieldInfo, string value)
        {
            string fieldType = fieldInfo.FieldType.Name;
            fieldType = fieldType.ToLower();

            switch (fieldType)
            {
                case "boolean":
                    bool b;
                    fieldInfo.SetValue(target, bool.TryParse(value, out b) ? b : false);
                    break;

                case "int32":
                    int n;
                    fieldInfo.SetValue(target, int.TryParse(value, out n) ? n : 0);
                    break;

                case "double":
                    double d;
                    fieldInfo.SetValue(target, double.TryParse(value, out d) ? d : 0);
                    break;

                case "string":
                    fieldInfo.SetValue(target, value);
                    break;
            }
        }

        public static void SetFieldValue(Object target, FieldInfo fieldInfo, string[] arr)
        {
            string fieldType = fieldInfo.FieldType.GetElementType().Name;
            fieldType = fieldType.ToLower();

            switch (fieldType)
            {
                case "boolean":
                    bool b;
                    bool[] arr_b = Array.ConvertAll(arr, s => bool.TryParse(s, out b) ? b : false);
                    fieldInfo.SetValue(target, arr_b);
                    break;

                case "int32":
                    int n;
                    int[] arr_n = Array.ConvertAll(arr, s => int.TryParse(s, out n) ? n : 0);
                    //int[] arr_n1 = Array.ConvertAll(arr, int.Parse);
                    //int[] arr_n2 = arr.Select(s => int.TryParse(s, out n) ? n : 0).ToArray();
                    fieldInfo.SetValue(target, arr_n);
                    break;

                case "double":
                    double d;
                    double[] arr_d = Array.ConvertAll(arr, s => double.TryParse(s, out d) ? d : 0);
                    fieldInfo.SetValue(target, arr_d);
                    break;

                case "string":
                    fieldInfo.SetValue(target, arr);
                    break;
            }
        }

        public static void SetFieldValue(Object target, FieldInfo fieldInfo, string[,] arr)
        {
            string fieldType = fieldInfo.FieldType.GetElementType().Name;
            fieldType = fieldType.ToLower();

            // 0. string return
            switch (fieldType)
            {
                case "string":
                    fieldInfo.SetValue(target, arr);
                    return;
                    break;
            }

            // 1. initialize
            int n;
            double d;
            bool b;

            dynamic output = Array.CreateInstance(fieldInfo.FieldType.GetElementType(), arr.GetLength(0), arr.GetLength(1));
            var converter = TypeDescriptor.GetConverter(fieldInfo.FieldType.GetElementType());

            // 2. convert
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    switch (fieldType)
                    {
                        case "boolean":
                            output[i, j] = (bool)converter.ConvertFromString((string)arr[i, j]);
                            break;

                        case "int32":
                            output[i, j] = (int)converter.ConvertFromString((string)arr[i, j]);
                            break;

                        case "double":
                            output[i, j] = (double)converter.ConvertFromString((string)arr[i, j]);
                            break;
                    }
                }
            }

            // 2. setvalue
            fieldInfo.SetValue(target, output);
        }

        public static T deepClone<T>(T toClone) where T : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            DefaultContractResolver dcr = new DefaultContractResolver();
            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            settings.ContractResolver = dcr;

            string tmp = JsonConvert.SerializeObject(toClone, settings);
            return JsonConvert.DeserializeObject<T>(tmp);
        }

        private static string DateTimeSQLite(DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine($"Current Window : {Console.WindowWidth}, {Console.WindowHeight}");
            Console.SetWindowSize(120, 40);
            Console.WriteLine($"Change Size : {Console.WindowWidth}, {Console.WindowHeight}");
            /*
                        int a = 55;
                        int b = 45;
                        Console.WriteLine("a + b = {0}", a + b);
                        char c = '1';
                        Console.WriteLine("c = {0} ({1})", c, c+0);
                        Console.WriteLine("c + 1 = {0}", c + 1);

                        Test st2;
                        st2.name = "seo";
                        st2.korean = "90";
                        st2.math = "93";
                        st2.Show();

                        Test st1 = new Test("kim", "80", "40");
                        st1.Show();


                        string s;

                        RETRY:
                        s = Console.ReadLine();
                        Console.WriteLine("you input : " + s);

                        s = s.ToLower();
                        switch (s)
                        {
                        case "월":
                        case "mon":
                            Console.WriteLine("today is monday");
                            break;

                        default:
                            Console.WriteLine("what today is it?");
                            goto RETRY;
                            break;
                        }
             */
            /*
                        ArrayList alist = new ArrayList();

                        for (int i = 0; i < 10; i++)
                        {
                            alist.Add(i);
                        }

                        Console.WriteLine("alist : ");
                        ShowArrayList(alist);

                        alist.Remove(5);
                        ShowArrayList(alist);
                        alist.Insert(5, 20);
                        ShowArrayList(alist);

                        alist.Reverse();
                        ShowArrayList(alist);
                        alist.Sort();
                        ShowArrayList(alist);
                        alist.Clear();
                        ShowArrayList(alist);
            */
            /*

                        MyDelegate d1 = new MyDelegate(Plus);
                        Console.WriteLine("10 + 5 = " + d1(10, 5));

                        d1 += Minus;
                        Console.WriteLine("10 - 5 = " + d1(10, 5));

                        d1 += Plus;
                        Console.WriteLine("10 + 5 = " + d1(10, 5));

                        d1 += Minus;
                        Console.WriteLine("10 - 5 = " + d1(10, 5));

                        d1 -= Minus;
                        Console.WriteLine("10 + 5 = " + d1(10, 5));

                        d1 -= Plus;
                        Console.WriteLine("10 + 5 = " + d1(10, 5));

                        Console.WriteLine("------------------");
                        MyDelegate d2 = (MyDelegate)Delegate.Combine(new MyDelegate(Plus), new MyDelegate(Minus), new MyDelegate(Plus), new MyDelegate(Plus), new MyDelegate(Minus));
                        Console.WriteLine("AAAA " + d2(20, 5));
            */


            //             AAA(5);

            //             MessageBox(0, "content", "title", 3);

            /*
                        string [] sarray = {"a.txt", "b.txt", "c.txt", "d.txt"};
                        foreach (string fname in sarray)
                        {
                            File.Delete(fname);
                        }
            //             FileStream fsa = File.Create("a.txt");
                        FileInfo file = new FileInfo("b.txt");
                        FileStream fsb = file.Create();


            //             fsa.Close();
                        fsb.Close();

                        StreamWriter sw = new StreamWriter("a.txt");
                        sw.Write("sw.Write()");
                        sw.Write("sw.Write()");
                        sw.Console.WriteLine("sw.Console.WriteLine()");
                        sw.Console.WriteLine("sw.Console.WriteLine()");

                        sw.Write(Console.ReadLine());

                        sw.Close();

                        File.Copy("a.txt", "c.txt");
                        FileInfo dst = file.CopyTo("d.txt");


                        StreamReader sr = new StreamReader("a.txt");
                        int n = 0;
                        while (sr.Peek() >= 0)
                        {
            //                 Console.WriteLine(n + " : " + sr.ReadToEnd());
            //                 Console.WriteLine(n + " : " + sr.Console.ReadLine());
                            Console.WriteLine(n + " : " + sr.Read());
                            n++;
                        }
            */

            /*
                        Program tsObject = new Program();
                        Thread spThread = new Thread(new ThreadStart(tsObject.ThreadSample));
                        Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);

                        spThread.Start();
                        Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);
                        Console.WriteLine("현재 스레드 IsAlive? : {0}", spThread.IsAlive);

                        Console.ReadLine();

                        spThread.Abort();
                        Console.WriteLine("현재 스레드 상태 : {0}", spThread.ThreadState);
            */

            bool runTest;

            // sql + SQLite
            runTest = CheckExecute("Do you want to execute sql db test with using SQLite?");
            if (runTest)
            {
                string strConn = @"Data Source=T:\mydb.db";

                using (SQLiteConnection conn = new SQLiteConnection(strConn))
                {
                    try
                    {
                        // 0. initialize
                        conn.Open();
                        string sql;
                        SQLiteCommand cmd;

                        // 1. drop table
                        runTest = CheckExecute("Do you want to delete table?");
                        if (runTest)
                        {
                            sql = "DROP TABLE IF EXISTS member";
                            cmd = new SQLiteCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. create table
                        sql = "CREATE TABLE IF NOT EXISTS member (id integer primary key, name string, age integer, Created datetime)";
                        cmd = new SQLiteCommand(sql, conn);
                        cmd.ExecuteNonQuery();

                        // 3. insert
                        for (int i = 0; i < 3; i++)
                        {
                            sql = $"INSERT INTO member VALUES (10{i}, '가나다라Tom{i}', {i * 2}, '{DateTimeSQLite(DateTime.Now)}')";
                            cmd = new SQLiteCommand(sql, conn);

                            cmd.ExecuteNonQuery();
                        }

                        // 4. select : SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
                        Console.WriteLine("======== using SQLiteDataReader =========");
                        sql = "SELECT * FROM member WHERE (Id >= 2)";
                        cmd = new SQLiteCommand(sql, conn);
                        SQLiteDataReader rdr = cmd.ExecuteReader();
                        DateTime t;
                        while (rdr.Read())
                        {
                            string str = "id : " + rdr["id"] + ", name : " + rdr["name"] + ", age : " + rdr["age"] + ", Created : " + rdr["Created"];
                            Console.WriteLine(str);
                            t = (DateTime)rdr["Created"];
                        }
                        rdr.Close();

                        // 4.1 select2 : SQLiteDataAdapter 클래스를 이용 비연결 모드로 데이타 읽기
                        Console.WriteLine("======== using DataSet =========");
                        DataSet ds = new DataSet();
                        //sql = "SELECT * FROM member";
                        var adpt = new SQLiteDataAdapter(sql, strConn);
                        adpt.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            foreach (DataRow r in ds.Tables[0].Rows)
                            {
                                Console.WriteLine("id : " + r["id"] + ", name : " + r["name"] + ", age : " + r["age"] + ", Created : " + r["Created"]);
                            }
                        }

                        Console.WriteLine("======== print ds using xml =========");
                        Console.WriteLine(ds.GetXml());

                        // 5. delete
                        runTest = CheckExecute("Do you want to delete data in table?");
                        if (runTest)
                        {
                            Console.WriteLine("======== delete data =========");
                            cmd.CommandText = "DELETE FROM member WHERE Id>100";
                            cmd.ExecuteNonQuery();

                            Console.WriteLine("======== using SQLiteDataReader =========");
                            sql = "SELECT * FROM member WHERE Id>=2";
                            cmd = new SQLiteCommand(sql, conn);
                            rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                Console.WriteLine("id : " + rdr["id"] + ", name : " + rdr["name"] + ", age : " + rdr["age"] + ", Created : " + rdr["Created"]);
                            }
                            rdr.Close();
                        }
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }

            // linq + SQLite
            runTest = CheckExecute("Do you want to execute linq test with using SQLite?");
            if (runTest)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }

            // reflection
            runTest = CheckExecute("Do you want to execute reflection test using structure?");
            if (runTest)
            {
                var fieldBook = new Dictionary<string, string>();

                SCylinderData cylData = new SCylinderData(1);
                cylData.ID = 99;
                cylData.MovingTime = 1.1;
                cylData.CylinderType = ECylinderType.UPSTREAM_DOWNSTREAM;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }

                // 1. Structure -> Dictionary
                Type type = typeof(SCylinderData);
                FieldInfo[] fields = type.GetFields();
                foreach(FieldInfo field in fields)
                {
                    if(field.FieldType.IsValueType)
                    {
                        fieldBook.Add(field.Name, field.GetValue(cylData).ToString());
                    }
                    else if(field.FieldType.IsArray)
                    {
                        Array array = (Array)field.GetValue(cylData);
                        int index = 0;
                        foreach(var v1 in array)
                        {
                            fieldBook.Add(field.Name+"__"+index++, v1.ToString());
                        }
                    }

                }

                // 2. print Dictionary
                foreach(KeyValuePair<string, string> item in fieldBook)
                {
                    Console.WriteLine($"{item.Key} : {item.Value}");
                }

                // 3. Dictionary -> Structure
                SCylinderData copyData = new SCylinderData(1);
                TypedReference tr = __makeref(copyData);
                foreach (FieldInfo field in fields)
                {
                    if (field.FieldType.IsValueType && fieldBook.ContainsKey(field.Name))
                    {
                        SetValueDirect(tr, field, fieldBook[field.Name]);
                    }
                    else if (field.FieldType.IsArray)
                    {
                        string key = field.Name;
                        //Array array = (Array)field.GetValue(cylData);
                        //int index = 0;
                        //foreach (var v1 in array)
                        //{
                        //    fieldBook.Add(field.Name + "__" + index++, v1.ToString());
                        //}
                    }

                }

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();

            }

            // reflection
            runTest = CheckExecute("Do you want to execute reflection test using class?");
            if (runTest)
            {
                // 0. initialize
                DCylinderData cylData = new DCylinderData();
                cylData.ID = 99;
                cylData.MovingTime = 1.1;
                cylData.CylinderType = ECylinderType.UPSTREAM_DOWNSTREAM;
                cylData.Solenoid = new int[]{ 2, 3};
                for (int i = 0; i < 2 ; i++)
                {
                    cylData.Solenoid[i] = i + 2;
                    cylData.nameTest[i] = $"NameTest_{i}";
                }
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }
                for (int i = 0; i < cylData.TwoDimension.GetLength(0) ; i++)
                {
                    for (int j = 0; j < cylData.TwoDimension.GetLength(1) ; j++)
                    {
                        cylData.TwoDimension[i, j] = i * cylData.TwoDimension.GetLength(1) + j;
                    }
                }
                cylData.boolTest[0] = true;
                cylData.boolTest[1] = false;
                cylData.boolTest[2] = true;

                // 1. Class -> Dictionary
                Dictionary<string, string> fieldBook = new Dictionary<string, string>();

                Type type = typeof(DCylinderData);
                fieldBook = ObjectExtensions.ToStringDictionary(cylData, type);

                //object d1 = cylData;
                //FieldInfo[] fields = type.GetFields();
                //foreach (FieldInfo field in fields)
                //{
                //    // 1.1 element
                //    if (field.FieldType.IsValueType)
                //    {
                //        fieldBook.Add(field.Name, field.GetValue(d1).ToString());
                //    }
                //    // 1.2 array
                //    else if (field.FieldType.IsArray)
                //    {
                //        Array array = (Array)field.GetValue(d1);

                //        // 1.2.1 1-D array
                //        if (array.Rank == 1)
                //        {
                //            for (int i = 0; i < array.GetLength(0); i++)
                //            {
                //                fieldBook.Add($"{field.Name}__{i}", array.GetValue(i).ToString());
                //            }
                //        }
                //        // 1.2.2 2-D array
                //        else if (array.Rank == 2)
                //        {
                //            for (int i = 0; i < array.GetLength(0); i++)
                //            {
                //                for (int j = 0; j < array.GetLength(1); j++)
                //                {
                //                    fieldBook.Add($"{field.Name}__{i}__{j}", array.GetValue(i, j).ToString());
                //                }
                //            }
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Not support {field.Name}'s array {array.Rank} dimension.");
                //        }
                //    }
                //    else
                //    {
                //        Console.WriteLine($"Not support to handle {field.Name}'s {field.FieldType.ToString()}");
                //    }
                //}

                // 2. print Dictionary
                foreach (KeyValuePair<string, string> item in fieldBook)
                {
                    Console.WriteLine($"FieldBook {item.Key} : {item.Value}");
                }

                // 3. Dictionary -> Class
                DCylinderData copyData = new DCylinderData();
                object d2 = copyData;
                ObjectExtensions.FromStringDicionary(copyData, type, fieldBook);
                //foreach (FieldInfo field in fields)
                //{
                //    // 3.1 handle element
                //    if (field.FieldType.IsValueType && fieldBook.ContainsKey(field.Name))
                //    {
                //        SetFieldValue(d2, field, fieldBook[field.Name]);
                //    }
                //    // 3.2 handle array
                //    else if (field.FieldType.IsArray)
                //    {
                //        Array array = (Array)field.GetValue(d2);
                //        string key, value;

                //        // 3.2.1 1-D array
                //        if (array.Rank == 1)
                //        {
                //            var arr_1d = new string[array.GetLength(0)];
                //            for (int i = 0; i < array.GetLength(0); i++)
                //            {
                //                key = $"{field.Name}__{i}";
                //                value = fieldBook.ContainsKey(key) ? fieldBook[key] : "";
                //                arr_1d.SetValue(value, i);
                //            }
                //            SetFieldValue(d2, field, arr_1d);
                //        }
                //        // 3.2.1 2-D array
                //        else if (array.Rank == 2)
                //        {
                //            var arr_2d = new string[array.GetLength(0), array.GetLength(1)];
                //            for (int i = 0; i < array.GetLength(0); i++)
                //            {
                //                for (int j = 0; j < array.GetLength(1); j++)
                //                {
                //                    key = $"{field.Name}__{i}__{j}";
                //                    value = fieldBook.ContainsKey(key) ? fieldBook[key] : "";
                //                    arr_2d.SetValue(value, i, j);
                //                }
                //            }
                //            SetFieldValue(d2, field, arr_2d);
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Not support {field.Name}'s array {array.Rank} dimension.");
                //        }
                //    }
                //    // 3.3 not support
                //    else
                //    {
                //        Console.WriteLine($"Not support to handle {field.Name}'s {field.FieldType.ToString()}");
                //    }
                //}

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();

            }

            // reflection
            runTest = CheckExecute("Do you want to execute serialization test with using file stream and json?");
            if (runTest)
            {
                // 0. initialize
                DCylinderData cylData = new DCylinderData();
                cylData.ID = 99;
                cylData.MovingTime = 1.1;
                cylData.CylinderType = ECylinderType.UPSTREAM_DOWNSTREAM;
                cylData.Solenoid = new int[] { 2, 3 };
                for (int i = 0; i < 2; i++)
                {
                    cylData.Solenoid[i] = i + 2;
                    cylData.nameTest[i] = $"NameTest_{i}";
                }
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }
                for (int i = 0; i < cylData.TwoDimension.GetLength(0); i++)
                {
                    for (int j = 0; j < cylData.TwoDimension.GetLength(1); j++)
                    {
                        cylData.TwoDimension[i, j] = i * cylData.TwoDimension.GetLength(1) + j;
                    }
                }
                cylData.boolTest[0] = true;
                cylData.boolTest[1] = false;
                cylData.boolTest[2] = true;
                cylData.SetMovingTime2(1.123);

                // 1. serialize
                Stream ws = new FileStream("DCylinderData.txt", FileMode.Create);
                BinaryFormatter serializer = new BinaryFormatter();

                serializer.Serialize(ws, cylData);
                ws.Close();

                // 2. print 

                // 3. deserialize
                Stream rs = new FileStream("a.dat", FileMode.Open);
                BinaryFormatter deserializer = new BinaryFormatter();

                DCylinderData copyData;
                copyData = (DCylinderData)deserializer.Deserialize(rs);
                rs.Close();

                // using json serializer without copying private
                // 1. serialize
                string output = JsonConvert.SerializeObject(cylData);

                // output = "null"
                cylData = null;
                output = JsonConvert.SerializeObject(cylData);

                // 2. print 
                // "{\"ID\":99,\"Solenoid\":[2,3],\"UpSensor\":[0,1,2,3],\"DownSensor\":[0,4,8,12],\"MovingTime\":1.1,\"CylinderType\":3,\"SolenoidType\":0,\"boolTest\":[true,false,true],\"nameTest\":[\"NameTest_0\",\"NameTest_1\"],\"TwoDimension\":[[0,1,2,3],[4,5,6,7],[8,9,10,11]]}"
                Console.WriteLine(output);

                // 3. deserialize
                DCylinderData copyData2;
                copyData2 = JsonConvert.DeserializeObject<DCylinderData>(output);

                // 4. change string
                output.Replace("UpSensor", "Down2");
                copyData2 = JsonConvert.DeserializeObject<DCylinderData>(output);

                // copyData2 = null
                output = "";
                copyData2 = JsonConvert.DeserializeObject<DCylinderData>(output);

                output = "{\"t1\":\"test\"}";
                copyData2 = JsonConvert.DeserializeObject<DCylinderData>(output);

                try
                {
                    // throw exception
                    output = "aaa";
                    copyData2 = JsonConvert.DeserializeObject<DCylinderData>(output);
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                // 5. using json serializer with copying private
                DCylinderData copyData3 = deepClone(cylData);

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();

            }

            // Socket Server
            runTest = CheckExecute("Do you want to execute Socket Server?");
            if (runTest)
            {
                try
                {
                    Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    server_socket.Bind(new IPEndPoint(IPAddress.Any, 2134));
                    server_socket.Listen(100);

                    Socket accepted_socket = server_socket.Accept();

                    byte[] send_buffer = Encoding.UTF8.GetBytes("Hello Client");
                    accepted_socket.Send(send_buffer, 0, send_buffer.Length, SocketFlags.None);

                    byte[] received_buffer = new byte[accepted_socket.SendBufferSize];
                    int bytesRead = accepted_socket.Receive(received_buffer);

                    Array.Resize(ref received_buffer, bytesRead);
                    string str = Encoding.UTF8.GetString(received_buffer);
                    Console.WriteLine("Received : " + str);
                    Console.WriteLine("Data Received!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();

                    accepted_socket.Close();
                    server_socket.Close();
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            // Socket Client
            runTest = CheckExecute("Do you want to execute Socket Client?");
            if (runTest)
            {
                Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2134);

                try
                {
                    client_socket.Connect(localEndPoint);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Main(args);
                }

                Console.Write("Enter Text : ");
                string str = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(str);

                client_socket.Send(data);
                Console.WriteLine("Data Sent!");

                byte[] received_buffer = new byte[client_socket.ReceiveBufferSize];
                int bytesRead = client_socket.Receive(received_buffer);

                Array.Resize(ref received_buffer, bytesRead);
                Console.WriteLine("Received : " + Encoding.UTF8.GetString(received_buffer));

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();

                client_socket.Close();
            }

            // Async Socket Server
            runTest = CheckExecute("Do you want to execute Chat Server?");
            if (runTest)
            {
                enterHostPort:
                UInt16 hostPort;
                Console.Write("수신 대기할 포트 입력: ");
                try
                {
                    hostPort = UInt16.Parse(Console.ReadLine().Trim());
                }
                catch
                {
                    Console.WriteLine("다시 입력하세요");
                    goto enterHostPort;
                }

                ChatServer cs = new ChatServer();
                cs.StartServer(hostPort);

                while (true)
                {
                    String msg;
                    Console.Write("보낼 메세지 (종료키: X): ");
                    msg = Console.ReadLine().Trim();

                    // 입력받은 문자열이 null 인 경우, 다시 반복문의 처음으로 돌아간다.
                    if (String.IsNullOrEmpty(msg))
                        continue;

                    // 입력받은 문자열이 X 인 경우, 프로그램을 종료한다.
                    if (msg.Equals("X"))
                    {
                        cs.StopServer();
                        break;
                    }

                    // 그 외의 경우엔 메세지를 보낸다.
                    cs.SendMessage(msg);
                }

            }

            // Async Socket Client
            runTest = CheckExecute("Do you want to execute Chat Client?");
            if (runTest)
            {
                enterHostName:
                String hostName;
                UInt16 hostPort;

                Console.Write("서버 주소 입력: ");

                // 키를 입력받는다.
                hostName = Console.ReadLine().Trim();

                // null 값이 입력됬다면 (아무것도 입력되지 않았다면)
                if (String.IsNullOrEmpty(hostName))
                {
                    Console.WriteLine("다시 입력하세요");
                    goto enterHostName;
                }

                enterHostPort:
                Console.Write("서버 포트 입력: ");

                try
                {
                    // 포트를 입력받고, UInt16 형으로 변환을 시도한다.
                    hostPort = UInt16.Parse(Console.ReadLine().Trim());
                }
                catch
                {
                    Console.WriteLine("다시 입력하세요");
                    goto enterHostPort;
                }

                ChatClient cc = new ChatClient();
                Console.WriteLine("접속 중...");

                cc.ConnectToServer(hostName, hostPort);
                if (!cc.Connected)
                {
                    Console.WriteLine("접속 중 오류 발생!");
                    goto enterHostName;

                    Console.Write("아무 키나 누르면 종료합니다.");
                    Console.ReadKey(true);
                    return;
                }

                // 무한반복
                while (true)
                {
                    String msg;
                    Console.Write("보낼 메세지 (종료키: X): ");
                    msg = Console.ReadLine().Trim();

                    // 입력받은 문자열이 null 인 경우, 다시 반복문의 처음으로 돌아간다.
                    if (String.IsNullOrEmpty(msg))
                        continue;

                    // 입력받은 문자열이 X 인 경우, 프로그램을 종료한다.
                    if (msg.Equals("X"))
                    {
                        cc.StopClient();
                        return;
                    }

                    // 그 외의 경우엔 메세지를 보낸다.
                    cc.SendMessage(msg);
                }

            }

            runTest = CheckExecute("Do you want to execute MethodCall?");
            if (runTest)
            {
                bool[] inarray = new bool[5] { true, false, true, false, true };
                bool[] outarray = null;

                for (int i = 0; i < outarray?.Length; i++)
                {
                    Console.WriteLine("before call. for {0} = {1}", i, outarray[i]);
                }

                CallHeart1(inarray, out outarray);
                
                foreach(bool bStatus in outarray)
                {
                    Console.WriteLine($"return. foreach bStatus = {bStatus}");
                }

                for(int i = 0; i < outarray?.Length ; i++)
                {
                    Console.WriteLine("return. for {0} = {1}", i, outarray[i]);
                }

            }

            // test unsafe code
            runTest = CheckExecute("Do you want to test unsafe code?");
            if (runTest)
            {
                SUnsafeTest sTest = new SUnsafeTest();
                SUnsafeTest sTest1;
                unsafe
                {
                    for (int i = 0; i < 4 ; i++)
                    {
                        sTest.Field1[i] = i * 10;
                        sTest1.Field1[i] = i * 20;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"sTest {i}_field = {sTest.Field1[i]}");
                        Console.WriteLine($"sTest1 {i}_field = {sTest1.Field1[i]}");
                    }
                }

                CCapsule capsule = new CCapsule();
                unsafe
                {
                    fixed(int *pValue = capsule.sUnsafe.Field1)
                    {
                        fixed(int *pValue2 = capsule.sUnsafe.Field2)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                //Console.WriteLine($"Capsule class's Unsafe {i}_field = {capsule.sUnsafe.Field1[i]}");
                                pValue[i] = i * 11;
                                Console.WriteLine($"Capsule class's Unsafe {i}_field = {pValue[i]}");

                                pValue2[i] = i * 7;
                                Console.WriteLine($"Capsule class's Unsafe {i}_field = {pValue2[i]}");
                            }
                        }
                    }
                }
            }

            // test copying class
            // result : Clone + new 조합은 deep copy는 가능하나, 생성자에서 멤버 변수를 일일히 복사해줘야 하는 단점이 있다.
            runTest = CheckExecute("Do you want to test copying class using clone method?");
            if (runTest)
            {
                CCylinderData cylData = new CCylinderData();
                cylData.MovingTime = 1.1;
                cylData.CylinderType = ECylinderType.UPSTREAM_DOWNSTREAM;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }

                CCylinderData cylData1 = new CCylinderData();
                cylData1 = cylData;
                CCylinderData cylData2 = cylData.Clone() as CCylinderData;

                Console.WriteLine("--------------before changing value-------------------");
                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.Write($"CylinderType original = {cylData.CylinderType}");
                Console.Write($", shallow = {cylData1.CylinderType}");
                Console.WriteLine($", deep = {cylData2.CylinderType}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }

                Console.WriteLine("--------------after changing value-------------------");

                cylData1.MovingTime = 2.2;
                cylData2.MovingTime = 3.3;

                cylData1.CylinderType = ECylinderType.FRONT_MID_BACK;
                cylData2.CylinderType = ECylinderType.LEFT_MIDE_RIGHT;

                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.Write($"CylinderType original = {cylData.CylinderType}");
                Console.Write($", shallow = {cylData1.CylinderType}");
                Console.WriteLine($", deep = {cylData2.CylinderType}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.UpSensor[i] = i * 2;
                    cylData2.UpSensor[i] = i * 3;

                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.DownSensor[i] = i * 5;
                    cylData2.DownSensor[i] = i * 6;

                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }
            }

            // test copying class
            // result : it's ok. wow~
            runTest = CheckExecute("Do you want to test copying class using ObjectExtensions Method?");
            if (runTest)
            {
                DCylinderData cylData = new DCylinderData();
                cylData.MovingTime = 1.1;
                cylData.CylinderType = ECylinderType.UPSTREAM_DOWNSTREAM;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }
                cylData.SetMovingTime2(1.123);

                DCylinderData cylData1 = new DCylinderData();
                cylData1 = cylData;
                DCylinderData cylData2 = ObjectExtensions.Copy(cylData);

                Console.WriteLine("--------------before changing value-------------------");
                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.Write($"CylinderType original = {cylData.CylinderType}");
                Console.Write($", shallow = {cylData1.CylinderType}");
                Console.WriteLine($", deep = {cylData2.CylinderType}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }

                Console.WriteLine("--------------after changing value-------------------");

                cylData1.MovingTime = 2.2;
                cylData2.MovingTime = 3.3;

                cylData1.CylinderType = ECylinderType.FRONT_MID_BACK;
                cylData2.CylinderType = ECylinderType.LEFT_MIDE_RIGHT;

                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.Write($"CylinderType original = {cylData.CylinderType}");
                Console.Write($", shallow = {cylData1.CylinderType}");
                Console.WriteLine($", deep = {cylData2.CylinderType}");

                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.UpSensor[i] = i * 2;
                    cylData2.UpSensor[i] = i * 3;

                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.DownSensor[i] = i * 5;
                    cylData2.DownSensor[i] = i * 6;

                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }

                Console.WriteLine("----------------------------------------------------------");

            }

            // test copying structure
            // result : DeepCopyStruct 함수 정상 동작함 
            runTest = CheckExecute("Do you want to test copying structure?");
            if (runTest)
            {
                SCylinderData cylData = new SCylinderData(1);
                cylData.MovingTime = 1.1;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData.UpSensor[i] = i * 1;
                    cylData.DownSensor[i] = i * 4;
                }

                SCylinderData cylData1 = new SCylinderData(1);
                cylData1 = (SCylinderData)DeepCopyStruct(cylData, typeof(SCylinderData));
                SCylinderData cylData2 = cylData;

                Console.WriteLine("--------------before changing value-------------------");
                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }

                Console.WriteLine("--------------after changing value-------------------");

                cylData1.MovingTime = 2.2;
                cylData2.MovingTime = 3.3;

                Console.Write($"movingtime original = {cylData.MovingTime}");
                Console.Write($", shallow = {cylData1.MovingTime}");
                Console.WriteLine($", deep = {cylData2.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.UpSensor[i] = i * 2;
                    cylData2.UpSensor[i] = i * 3;

                    Console.Write($"UpSensor {i}_field original = {cylData.UpSensor[i]}");
                    Console.Write($", shallow = {cylData1.UpSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    cylData1.DownSensor[i] = i * 5;
                    cylData2.DownSensor[i] = i * 6;

                    Console.Write($"DownSensor {i}_field original = {cylData.DownSensor[i]}");
                    Console.Write($", shallow = {cylData1.DownSensor[i]}");
                    Console.WriteLine($", deep = {cylData2.DownSensor[i]}");

                }
            }

            // test copying structure
            // result : DeepCopyStruct 함수가 class에서는 에러 발생
            runTest = CheckExecute("Do you want to test copying structure in class?");
            if (runTest)
            {
                CCapsuleStructure capsule = new CCapsuleStructure();
                capsule.CylinderData.MovingTime = 1.1;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule.CylinderData.UpSensor[i] = i * 1;
                    capsule.CylinderData.DownSensor[i] = i * 4;
                }

                CCapsuleStructure capsule1 = new CCapsuleStructure();
                capsule1 = (CCapsuleStructure)DeepCopyStruct(capsule, typeof(CCapsuleStructure));

                Console.WriteLine("--------------before changing value-------------------");
                Console.Write($"movingtime original = {capsule.CylinderData.MovingTime}");
                Console.WriteLine($", deep = {capsule1.CylinderData.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"UpSensor {i}_field original = {capsule.CylinderData.UpSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"DownSensor {i}_field original = {capsule.CylinderData.DownSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.DownSensor[i]}");

                }

                Console.WriteLine("--------------after changing value-------------------");

                capsule1.CylinderData.MovingTime = 2.2;

                Console.Write($"movingtime original = {capsule.CylinderData.MovingTime}");
                Console.WriteLine($", deep = {capsule1.CylinderData.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule1.CylinderData.UpSensor[i] = i * 3;

                    Console.Write($"UpSensor {i}_field original = {capsule.CylinderData.UpSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule1.CylinderData.DownSensor[i] = i * 5;

                    Console.Write($"DownSensor {i}_field original = {capsule.CylinderData.DownSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.DownSensor[i]}");
                }
            }

            // test copying structure
            runTest = CheckExecute("Do you want to test copying structure that has array in multiple layer?");
            // result : DeepCopyStruct 함수가 에러 발생
            if (runTest)
            {
                SCapsuleStructure capsule = new SCapsuleStructure(1);
                capsule.CylinderData.MovingTime = 1.1;
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule.CylinderData.UpSensor[i] = i * 1;
                    capsule.CylinderData.DownSensor[i] = i * 4;
                }

                SCapsuleStructure capsule1 = new SCapsuleStructure(1);
                capsule1 = (SCapsuleStructure)DeepCopyStruct(capsule, typeof(SCapsuleStructure));

                Console.WriteLine("--------------before changing value-------------------");
                Console.Write($"movingtime original = {capsule.CylinderData.MovingTime}");
                Console.WriteLine($", deep = {capsule1.CylinderData.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"UpSensor {i}_field original = {capsule.CylinderData.UpSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    Console.Write($"DownSensor {i}_field original = {capsule.CylinderData.DownSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.DownSensor[i]}");

                }

                Console.WriteLine("--------------after changing value-------------------");

                capsule1.CylinderData.MovingTime = 2.2;

                Console.Write($"movingtime original = {capsule.CylinderData.MovingTime}");
                Console.WriteLine($", deep = {capsule1.CylinderData.MovingTime}");
                Console.WriteLine("----------------------------------------------------------");

                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule1.CylinderData.UpSensor[i] = i * 3;

                    Console.Write($"UpSensor {i}_field original = {capsule.CylinderData.UpSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.UpSensor[i]}");
                }

                Console.WriteLine("----------------------------------------------------------");
                for (int i = 0; i < DEF_MAX_CYLINDER_SENSOR; i++)
                {
                    capsule1.CylinderData.DownSensor[i] = i * 5;

                    Console.Write($"DownSensor {i}_field original = {capsule.CylinderData.DownSensor[i]}");
                    Console.WriteLine($", deep = {capsule1.CylinderData.DownSensor[i]}");
                }
            }

            // test copying structure
            runTest = CheckExecute("Do you want to test deep/shallow copying structure in multiple layer?");
            // result : SUCCESS
            if (runTest)
            {
                SLayer2 ALayer = new SLayer2(1);
                SLayer2 BLayer = new SLayer2(1);
                SLayer2 CLayer = new SLayer2(1);

                int index = 1;
                ALayer.Member1             = index++;
                ALayer.Array1[0, 0]        = index++;
                ALayer.Array1[0, 1]        = index++;
                ALayer.Array1[1, 0]        = index++;
                ALayer.Array1[1, 1]        = index++;
                ALayer.Child1.Member1      = index++;
                ALayer.Child1.Array1[0, 0] = index++;
                ALayer.Child1.Array1[0, 1] = index++;
                ALayer.Child1.Array1[1, 0] = index++;
                ALayer.Child1.Array1[1, 1] = index++;

                //BLayer = (SLayer2)DeepCopyStruct(ALayer, typeof(SLayer2));
                BLayer = DeepCopyStruct(ALayer);
                CLayer = ALayer;

                Console.WriteLine("--------------before changing Origin Structure-------------------");
                Console.WriteLine($"Origin  : {ALayer}");
                Console.WriteLine($"Deep    : {BLayer}");
                Console.WriteLine($"Shallow : {CLayer}");
                Console.WriteLine("-----------------------------------------------------------------");

                ALayer.Member1             = index++;
                ALayer.Array1[0, 0]        = index++;
                ALayer.Array1[0, 1]        = index++;
                ALayer.Array1[1, 0]        = index++;
                ALayer.Array1[1, 1]        = index++;
                ALayer.Child1.Member1      = index++;
                ALayer.Child1.Array1[0, 0] = index++;
                ALayer.Child1.Array1[0, 1] = index++;
                ALayer.Child1.Array1[1, 0] = index++;
                ALayer.Child1.Array1[1, 1] = index++;

                Console.WriteLine("--------------after changing Origin Structure-------------------");
                Console.WriteLine($"Origin  : {ALayer}");
                Console.WriteLine($"Deep    : {BLayer}");
                Console.WriteLine($"Shallow : {CLayer}");
                Console.WriteLine("-----------------------------------------------------------------");
            }

            // operator overriding
            runTest = CheckExecute("Do you want to test operator overriding?");
            // result : SUCCESS
            if (runTest)
            {
                SPos_XY a = new SPos_XY();
                SPos_XY b = new SPos_XY();

                a.Init(1.1, 2.2);
                b.Init(1.1, 4);
                if (a == b) Console.WriteLine("a == b");
                if (a != b) Console.WriteLine("a != b");

                b = a;
                if (a == b) Console.WriteLine("a == b");
                if (a != b) Console.WriteLine("a != b");

                if (a.Equals(b))
                {
                    Console.WriteLine("a equal b");
                }
                else
                {
                    Console.WriteLine("a not equal b");
                }

                if (a.Equals(null))
                {
                    Console.WriteLine("a equal null");
                }
                else
                {
                    Console.WriteLine("a not equal null");
                }

                double[] array1;
                int[] array2 = new int[] { 8, 9 };
                a.TransToArray(out array1);
                a.TransFromArray(array2);

                a = a + b;
                a = a + 5;
                a = a - 5;
                a = a * 5;
                a = a / 5;
                a = a / 0;

                Console.WriteLine("end of test");
            }

            // start of Horse Test
            runTest = CheckExecute("Do you want to run HorseGame?");
            if (runTest)
            {
                ArrayList al_h = new ArrayList();
                for (int i = 0; i < DEF_MAX_HORSE; i++)
                {
                    al_h.Add(new Horse(i));
                }

                ArrayList al_th = new ArrayList();
                for (int n = 0; n < 100; n++)
                {
                    Thread.Sleep(DEF_SLEEP_TIME);
                    Console.WriteLine("ready to next battle");

                    al_th.Clear();
                    for (int i = 0; i < DEF_MAX_HORSE; i++)
                    {
                        al_th.Add(new Thread(new ThreadStart(((Horse)al_h[i]).Go)));
                    }


                    Horse.bAllEnd = false;
                    for (int i = 0; i < DEF_MAX_HORSE; i++)
                    {
                        ((Thread)al_th[i]).Start();
                    }

                    while (true)
                    {
                        if (Horse.bAllEnd == true)
                        {
                            break;
                        }
                        Thread.Sleep(DEF_SLEEP_TIME);
                    }


                    for (int i = 0; i < DEF_MAX_HORSE; i++)
                    {
                        while (((Horse)al_h[i]).bStart == true)
                        {
                            Thread.Sleep(DEF_SLEEP_TIME);
                        }
                        ((Thread)al_th[i]).Abort();
                    }

                }
                Console.WriteLine("ended all game");
                Console.WriteLine("Game Result =======================");
                for (int i = 0; i < DEF_MAX_HORSE; i++)
                {
                    Console.WriteLine("Result h{0} : {1}", i, ((Horse)al_h[i]).Result(i));
                }
            }
            // end of Horse Test

//             Program mc = new Program();
//             mc.Console.WriteLine(h1);
            
            
            // start of class hierarchy test
            runTest = CheckExecute("Do you want to run Class Hierarchy test?");
            
            if(runTest)
            {
                // init Hardware Layer
                int nIndex = 10;
                HMotion hm1 = new HMotion("HMotion-Yaskawa", nIndex++, "Motion");
                HMotion hm2 = new HMotion("HMotion-Panasonic", nIndex++, "Motion");
                HMotion hm3 = new HMotion("HMotion-Omron", nIndex++, "Motion");

                Console.WriteLine(hm1);
                Console.WriteLine(hm2);

                nIndex = 20;
                HVision hv1 = new HVision("HVision-Matrox", nIndex++, "Vision");
                HVision hv2 = new HVision("HVision-Cognex", nIndex++, "Vision");

                Console.WriteLine(hv1);
                Console.WriteLine(hv2);

                // init Mechanical Layer
                nIndex = 100;
                MStage stage1 = new MStage("M-Stage1", nIndex++);
                MStage stage2 = new MStage("M-Stage2", nIndex++);

                Console.WriteLine(stage1);
                Console.WriteLine(stage2);

                // show original component
                stage1.setReference(hm1, hv1);
                stage2.setReference(hm2, hv2);

                HMotion pm;
                HVision pv;
                pm = stage1.getMotion();
                pv = stage1.getVision();

                Console.WriteLine("component of stage1");
                Console.WriteLine(pm);
                Console.WriteLine(pv);

                pm = stage2.getMotion();
                pv = stage2.getVision();

                Console.WriteLine("component of stage2");
                Console.WriteLine(pm);
                Console.WriteLine(pv);

                //             // change component
                //             pm = stage1.getMotion();
                //             pv = stage1.getVision();
                //             stage2.setReference(pm, pv);
                //             pm = stage2.getMotion();
                //             pv = stage2.getVision();
                // 
                //             Console.WriteLine("component of stage2");
                //             Console.WriteLine(pm);
                //             Console.WriteLine(pv);

                // init Ctrl Layer
                Console.WriteLine("=======Init Control Layer");

                nIndex = 200;
                CStage1 ctrlStage1 = new CStage1("CtrlStage1", nIndex++);
                ctrlStage1.setReference(stage1);
                Console.WriteLine(ctrlStage1);

                Console.WriteLine("=== member of CtrlStage1");

                MStage pstage;
                pstage = ctrlStage1.getStage();
                Console.WriteLine(pstage);

                pm = pstage.getMotion();
                pv = pstage.getVision();

                Console.WriteLine(pm);
                Console.WriteLine(pv);

                Console.WriteLine("=== change member of CtrlStage1");
                ctrlStage1.setReference(stage2);

                Console.WriteLine("=== member of CtrlStage1");

                pstage = ctrlStage1.getStage();
                Console.WriteLine(pstage);

                pm = pstage.getMotion();
                pv = pstage.getVision();

                Console.WriteLine(pm);
                Console.WriteLine(pv);

                Console.WriteLine("=== change member of CtrlStage1");
                ctrlStage1.setReference(stage1);

                Console.WriteLine("=== member of CtrlStage1");

                pstage = ctrlStage1.getStage();
                Console.WriteLine(pstage);

                pm = pstage.getMotion();
                pv = pstage.getVision();

                Console.WriteLine(pm);
                Console.WriteLine(pv);

                Console.WriteLine("=== test reference of address");
                pm.name = "new motion by reference";
                Console.WriteLine(pm);
                pm = pstage.getMotion();
                Console.WriteLine(pm);

                Console.WriteLine("=== test deep copy");
                hm3 = (HMotion)pm.Clone();
                Console.WriteLine(hm3);
                hm3.name = "new motion by Clone";
                Console.WriteLine(hm3);
                pm = pstage.getMotion();
                Console.WriteLine(pm);

                // start of copy by reference / value
                Console.WriteLine("=== test struct");
                Student d1 = new Student("aaa", "90", "80");
                ctrlStage1.setData(d1);

                Student d2 = ctrlStage1.getData();
                d2.Show();
                d2.korean = "100";
                d2.Show();

                ctrlStage1.setData(d2);
                d2 = ctrlStage1.getData();
                d2.Show();
                // start of copy by reference / value

                ctrlStage1.WriteLog("====Test Log");
            }
            // end of class hierarchy test

            //             // start of copy by reference / value
            //             Console.WriteLine("Do you want to run copy by reference  / value test?");
            //             reply = Console.ReadLine();
            //             runTest = false;
            //             if (!string.IsNullOrEmpty(reply))
            //             {
            //                 if (string.Compare(reply, "Yes", true) >= 0)
            //                 {
            //                     runTest = true;
            //                 }
            //             }
            // 
            runTest = CheckExecute("Do you want to run string format?");
            if (runTest)
            {
                string code = @"
public string ReadFile(string filename)
{
    if (!string.IsNullOrEmpty(filename))
    {
        return File.ReadAllText(filename);
    }
    return string.Empty;
}
";
                Console.WriteLine(code);

                decimal val = 1234.5678M;
                string s = string.Format("{0,12:N02}", val);
                //string s = @"{val,12:N02}";   // this is not valid
                // 출력: "  1,234.57"
                Console.WriteLine(s); 
            }

            // start of Action / Func / Predicate
            runTest = CheckExecute("Do you want to run Action / Func / Predicate test?");

            if (runTest)
            {
                System.Action<string, string> act2 = delegate(string msg, string title)
                {
//                     MessageBox.Show(msg, title);
                    StringBuilder sb = new StringBuilder();
                    sb.Append(title + " : " + msg);
                    Console.WriteLine(sb);
                };
                act2("no data found", "error");

                Action<int> act3 = code => Console.WriteLine("Code : {0}", code);
                act3(1033);

                int _state = 1;
                Func<int, bool> fa = delegate(int n)
                {
                    return _state == n;
                };
                bool result = fa(1);

                Console.WriteLine("result : {0}", result);

                Func<int, bool> fb = n => _state == n;
                _state = 0;
                result = fb(1);
                Console.WriteLine("result : {0}", result);

                Predicate<int> pa = delegate(int n)
                {
                    return n == 0;
                };
                result = pa(0);
                Console.WriteLine("result : {0}", result);

                Predicate<int> pb = n => n > 0;
                result = pb(1);
                Console.WriteLine("result : {0}", result);

                int[] arr = { -10, 20, -30, 4, -5 };
                int pos = Array.FindIndex(arr, pb);
                Console.WriteLine("pos : {0}", pos);

                var v = arr.Where(n => true);
                IEnumerable<int> arr2 = v;
//                IEnumerable<int> arr2 = arr.Where(n => true);
                foreach (int n in arr2)
                {
                    Console.WriteLine("array list : {0}", n);
                }

                IEnumerator enumerator = arr.Where(n => n < 0).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Console.WriteLine("negative value : {0}", enumerator.Current);
                }

                foreach(int n in arr.Where(n => n > 0))
                {
                    Console.WriteLine("positive value : {0}", n);
                }
            }
            // end of Action / Func / Predicate

            // start of using pointer
            runTest = CheckExecute("Do you want to run using pointer test?");
            if (runTest)
            {
                unsafe
                {
                    Console.WriteLine("Test of using point ");
                    int p;
                    GetAddResult(&p, 10, 20);
                    Console.WriteLine("add by using point : 10 + 20 = {0}", p);

                    Robot r = new Robot();
                    fixed (int* pCount = &r.Type)
                    {

                    }

                    HMotion hm0 = new HMotion("HMotion-Yaskawa", 99, "Motion");
                    fixed (int* pIndex = &hm0.nIndex)
                    {
                        Console.WriteLine(hm0);
                        *pIndex = 55;
                        Console.WriteLine(hm0);
                    }

                }

            }
            // end of using pointer

            // start of Timer Test
            runTest = CheckExecute("Do you want to run Timer Test?");
            if (runTest)
            {
                Console.WriteLine("Test of Timer ");

                string txt = "Hello, World";
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 100000; i++)
                {
                    txt = txt + "1";
                }
                sw.Stop();
                Console.WriteLine("string txt apped : {0} sec", sw.Elapsed);

                StringBuilder sb = new StringBuilder();
                sb.Append(txt);
                sw.Restart();
                for (int i = 0; i < 100000; i++)
                {
                    sb.Append("1");
                }
                sw.Stop();
                Console.WriteLine("stringbuilder txt apped : {0} sec", sw.Elapsed);

            }
            // end of

            // start of serialization Test
            runTest = CheckExecute("Do you want to run serialization Test?");
            if (runTest)
            {
                Console.WriteLine("Test of serialization");

                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(Student));
                MemoryStream ms = new MemoryStream();
                Student st1 = new Student("김태희的成果", "80", "100");
                Student st2 = new Student("김희선", "30", "50");

                dcjs.WriteObject(ms, st1);

                ms.Position = 0;
                //Student jj = dcjs.ReadObject(ms) as Student;
                Student jj = (Student)dcjs.ReadObject(ms);

                byte[] buf = ms.ToArray();
                Console.WriteLine(Encoding.UTF8.GetString(buf));
                Console.WriteLine(jj);

                using (FileStream fs = new FileStream("test.log", FileMode.Create))
                {
                    dcjs.WriteObject(fs, st1);
                    //dcjs.WriteObject(fs, st2);
                }

                using (FileStream fs = new FileStream("test.log", FileMode.Open))
                {
                    Console.WriteLine("read from file");
                    jj = (Student)dcjs.ReadObject(fs);
                    Console.WriteLine(jj);
                }

            }
            // end of

            MessageBeep(0);

            Console.WriteLine("=====================================================");
            Console.WriteLine("All test has been finished.");
            Console.ReadLine();

        }

        static bool CheckExecute(string txt)
        {
            Console.WriteLine(txt + " [Yes][Quit]");
            string reply = Console.ReadLine();
            if (!string.IsNullOrEmpty(reply))
            {
                if (string.Compare(reply, "Yes", true) >= 0)
                {
                    return true;
                }
                if (string.Compare(reply, "Quit", true) >= 0)
                {
                    Environment.Exit(0);
                    return false;
                }
            }
            return false;
        }

        class Robot
        {
            public int Type;
        }


        [DllImport("user32.dll")]
        static extern int MessageBeep(uint uType);

        unsafe static void GetAddResult(int* p, int a, int b)
        {
            *p = a + b;
        }

        bool Divide(int n1, int n2, out int result)
        {
            if (n2 == 0)
            {
                result = 0;
                return false;
            }

            result = n1 / n2;
            return true;
        }


        //public static void Console.WriteLine(ObjectBase c)
        //{
        //    Console.WriteLine("this class : {0}, index : {1}", c.name, c.nIndex);
        //}


        public void ThreadSample()
        {
            int n = 0;
            while (true)
            {
                Console.WriteLine(n + " : Thread is Running");
                n++;
                Thread.Sleep(1000);
            }
        }

         delegate int MyDelegate(int a, int b);

//         [Obsolete("plus function is not safe", false)]
        [Conditional("DEBUG0")]
        static void AAA(int k)
        {
            Console.WriteLine("this function is valid when debug mode");
        }

        [DllImport("User32.dll")]
        public static extern int MessageBox(int hParent, string Message, string Caption, int Type);

        static int Plus(int a, int b) {
            Console.WriteLine("Plus");
            return a + b;
        }
        static int Minus(int a, int b) {
            Console.WriteLine("Minus");
            return a - b;
        }

        static void ShowArrayList(ArrayList list)
        {
            Console.Write("Total {0} : ", list.Count);

            foreach (object obj in list)
            {
                Console.Write(obj + " ");
            }
            Console.WriteLine();
    
        }


    }

    class Horse
    {
        private int number;
        public static bool bAllEnd { get; set; }
        static int[] WinGame = new int[Program.DEF_MAX_HORSE];
        public bool bStart { get; set; }

        public Horse(int number)
        {
            this.number = number;
        }

        public void Init()
        {
            bAllEnd = false;
        }

        /// <summary>
        /// 경주마들의 승리 결과값을 리턴
        /// </summary>
        /// <param name="number">경주마 번호</param>
        /// <returns>승리 횟수</returns>
        public int Result(int number)
        {
            return WinGame[number];
        }

        public int nWinCount { get; set;  }

        /// <summary>
        /// 실제 경주마들 경주 쓰레드 함수
        /// </summary>
        public void Go()
        {
            //                 lock (this)
            {
                int dist = 0;
                bStart = true;

                while (true)
                {
                    if (bStart == false) break;
                    if (bAllEnd == true)
                    {
                        //Console.WriteLine("---{0}번마 탈락----", number);
                        bStart = false;
                        break;
                    }

                    int seed = Environment.TickCount;
                    Random rd = new Random(seed + number);

                    dist += rd.Next(50, 100);

                    if (dist >= 1000)
                    {
                        bStart = false;
                        bAllEnd = true;
                        WinGame[number]++;
                        Console.WriteLine("***{0}번마 우승 ***", number);
                        break;
                    }

                    Console.WriteLine("{0}번마 현재 {1}m 지나는 중..", number, dist);
                    Thread.Sleep(Program.DEF_SLEEP_TIME);
                }

                //                 if (bAllEnd == false)
            }
        }
    }

    class ObjectBase : ICloneable
    {
        public string name { get; set; }
        //public int nIndex { get; set; }
        public int nIndex;

        public ObjectBase(string name, int nIndex)
        {
            this.name = name;
            this.nIndex = nIndex;
        }

        public object Clone()
        {
            ObjectBase ob = new ObjectBase(name, nIndex);
            return ob;
        }

        public override string ToString()
        {
            //return string.Format("this class : {0}, index : {1}", this.name, this.nIndex);
            return @"this class : {name}, index : {nIndex}";
        }

        public void WriteLog(string strLog)
        {
            StackFrame sf = new StackFrame(true);
            string str = $"stackframe file:{sf.GetFileName()}, line:{sf.GetFileLineNumber()}, method:{sf.GetMethod()}";
            Console.WriteLine(str);

            StackFrame sf2 = new StackFrame(0, true);
            str = $"stackframe file:{sf2.GetFileName()}, line:{sf2.GetFileLineNumber()}, method:{sf2.GetMethod()}";
            Console.WriteLine(str);

            StackFrame sf3 = new StackFrame(1, true);
            str = $"stackframe file:{sf3.GetFileName()}, line:{sf3.GetFileLineNumber()}, method:{sf3.GetMethod()}";
            Console.WriteLine(str);

            Console.WriteLine(strLog);
        }
    }

    class HMotion : ObjectBase, ICloneable
    {
        public string strType { get; set; }

        public HMotion(string name, int nIndex, string strType) : base(name, nIndex)
        {
            this.strType = strType;
        }

        public new object Clone()
        {
            HMotion hm = new HMotion(name, nIndex, strType);
            return hm;
        }
    }

    class HVision : ObjectBase
    {
        public string strType { get; set; }

        public HVision(string name, int nIndex, string strType)
            : base(name, nIndex)
        {
            this.strType = strType;
        }
    }

    class MStage : ObjectBase
    {
        private HMotion m_Motor;
        private HVision m_Vision;

        public MStage(string name, int nIndex)
            : base(name, nIndex)
        {
        }

        public void setReference(HMotion hm, HVision hv)
        {
            m_Motor = hm;
            m_Vision = hv;
        }

        public HMotion getMotion()
        {
            return m_Motor;
        }

        public HVision getVision()
        {
            return m_Vision;
        }
    }

    struct Student
    {
        public string name;
        public string korean;
        public string math;

        public Student(string name, string korean, string math)
        {
            this.name = name;
            this.korean = korean;
            this.math = math;

            Console.WriteLine("struct Student's construction");
        }

        public void Show()
        {
            Console.WriteLine(name + "'s score : ");
            Console.WriteLine("korean : " + korean + ", math : " + math);
        }
    }

    class CStage1 : ObjectBase
    {
        private MStage m_Stage;
        private Student m_Data;

        public CStage1(string name, int nIndex)
            : base(name, nIndex)
        {
        }

        public void setReference(MStage stage)
        {
            m_Stage = stage;
        }

        public MStage getStage()
        {
            return m_Stage;
        }

        public void setData(Student data)
        {
            m_Data = data;
        }

        public Student getData()
        {
            return m_Data;
        }
    }

    public struct SPos_XY
    {
        public double dX;
        public double dY;

        public SPos_XY(double dX, double dY)
        {
            this.dX = dX;
            this.dY = dY;
        }

        public void Init<T>(T x, T y)
        {
            try
            {
                dX = Convert.ToDouble(x);
                dY = Convert.ToDouble(y);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void TransToArray(out double[] array)
        {
            array = new double[] { dX, dY };
        }

        public void TransFromArray<T>(T[] array)
        {
            if (array.Length != 2) return;
            try
            {
                dX = Convert.ToDouble(array[0]);
                dY = Convert.ToDouble(array[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is SPos_XY)) return false;

            SPos_XY s2 = (SPos_XY)obj;
            return Math.Equals(dX, s2.dX) && Math.Equals(dY, s2.dY);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SPos_XY s1, SPos_XY s2)
        {
            return Math.Equals(s1.dX, s2.dX) && Math.Equals(s1.dY, s2.dY);
        }

        public static bool operator !=(SPos_XY s1, SPos_XY s2)
        {
            return !(s1 == s2);
        }

        public static SPos_XY operator +(SPos_XY s1, SPos_XY s2)
        {
            SPos_XY s = new SPos_XY();

            s.dX = s1.dX + s2.dX;
            s.dY = s1.dY + s2.dY;

            return s;
        }

        public static SPos_XY operator +(SPos_XY s1, double dAdd)
        {
            SPos_XY s = new SPos_XY();

            s.dX = s1.dX + dAdd;
            s.dY = s1.dY + dAdd;

            return s;
        }

        public static SPos_XY operator -(SPos_XY s1, SPos_XY s2)
        {
            SPos_XY s = new SPos_XY();

            s.dX = s1.dX - s2.dX;
            s.dY = s1.dY - s2.dY;

            return s;
        }

        public static SPos_XY operator -(SPos_XY s1, double dSub)
        {
            SPos_XY s = new SPos_XY();

            s.dX = s1.dX - dSub;
            s.dY = s1.dY - dSub;

            return s;
        }

        public static SPos_XY operator *(SPos_XY s1, double dMul)
        {
            SPos_XY s = new SPos_XY();

            s.dX = s1.dX * dMul;
            s.dY = s1.dY * dMul;

            return s;
        }

        public static SPos_XY operator /(SPos_XY s1, double dDiv)
        {
            SPos_XY s = new SPos_XY();
            if (dDiv == 0) return s;

            s.dX = s1.dX / dDiv;
            s.dY = s1.dY / dDiv;

            return s;
        }
    }

    public struct SPos_XYT
    {
        public double dX;
        public double dY;
        public double dT;

        public SPos_XYT(double dX, double dY, double dT)
        {
            this.dX = dX;
            this.dY = dT;
            this.dT = dT;
        }

        public void Init<T>(T x, T y, T t)
        {
            try
            {
                dX = Convert.ToDouble(x);
                dY = Convert.ToDouble(y);
                dT = Convert.ToDouble(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void TransToArray(out double[] array)
        {
            array = new double[] { dX, dY, dT };
        }

        public void TransFromArray<T>(T[] array)
        {
            if (array.Length != 3) return;
            try
            {
                dX = Convert.ToDouble(array[0]);
                dY = Convert.ToDouble(array[1]);
                dT = Convert.ToDouble(array[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is SPos_XYT)) return false;

            SPos_XYT s2 = (SPos_XYT)obj;
            return Math.Equals(dX, s2.dX) && Math.Equals(dY, s2.dY)
                && Math.Equals(dT, s2.dT);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SPos_XYT s1, SPos_XYT s2)
        {
            return Math.Equals(s1.dX, s2.dX) && Math.Equals(s1.dY, s2.dY)
                 && Math.Equals(s1.dT, s2.dT);
        }

        public static bool operator !=(SPos_XYT s1, SPos_XYT s2)
        {
            return !(s1 == s2);
        }

        public static SPos_XYT operator +(SPos_XYT s1, SPos_XYT s2)
        {
            SPos_XYT s = new SPos_XYT();

            s.dX = s1.dX + s2.dX;
            s.dY = s1.dY + s2.dY;
            s.dT = s1.dT + s2.dT;

            return s;
        }

        public static SPos_XYT operator +(SPos_XYT s1, double dAdd)
        {
            SPos_XYT s = new SPos_XYT();

            s.dX = s1.dX + dAdd;
            s.dY = s1.dY + dAdd;
            s.dT = s1.dT + dAdd;

            return s;
        }

        public static SPos_XYT operator -(SPos_XYT s1, SPos_XYT s2)
        {
            SPos_XYT s = new SPos_XYT();

            s.dX = s1.dX - s2.dX;
            s.dY = s1.dY - s2.dY;
            s.dT = s1.dT - s2.dT;

            return s;
        }

        public static SPos_XYT operator -(SPos_XYT s1, double dSub)
        {
            SPos_XYT s = new SPos_XYT();

            s.dX = s1.dX - dSub;
            s.dY = s1.dY - dSub;
            s.dT = s1.dT - dSub;

            return s;
        }

        public static SPos_XYT operator *(SPos_XYT s1, double dMul)
        {
            SPos_XYT s = new SPos_XYT();

            s.dX = s1.dX * dMul;
            s.dY = s1.dY * dMul;
            s.dT = s1.dT * dMul;

            return s;
        }

        public static SPos_XYT operator /(SPos_XYT s1, double dDiv)
        {
            SPos_XYT s = new SPos_XYT();
            if (dDiv == 0) return s;

            s.dX = s1.dX / dDiv;
            s.dY = s1.dY / dDiv;
            s.dT = s1.dT / dDiv;

            return s;
        }
    }

    public struct SPos_XYTZ
    {
        public double dX;
        public double dY;
        public double dT;
        public double dZ;

        public SPos_XYTZ(double dX, double dY, double dT, double dZ)
        {
            this.dX = dX;
            this.dY = dT;
            this.dT = dT;
            this.dZ = dZ;
        }

        public void Init<T>(T x, T y, T t, T z)
        {
            try
            {
                dX = Convert.ToDouble(x);
                dY = Convert.ToDouble(y);
                dT = Convert.ToDouble(t);
                dZ = Convert.ToDouble(z);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void TransToArray(out double[] array)
        {
            array = new double[] { dX, dY, dT, dZ };
        }

        public void TransFromArray<T>(T[] array)
        {
            if (array.Length != 4) return;
            try
            {
                dX = Convert.ToDouble(array[0]);
                dY = Convert.ToDouble(array[1]);
                dT = Convert.ToDouble(array[2]);
                dZ = Convert.ToDouble(array[3]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is SPos_XYTZ)) return false;

            SPos_XYTZ s2 = (SPos_XYTZ)obj;
            return Math.Equals(dX, s2.dX) && Math.Equals(dY, s2.dY)
                && Math.Equals(dT, s2.dT) && Math.Equals(dZ, s2.dZ);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(SPos_XYTZ s1, SPos_XYTZ s2)
        {
            return Math.Equals(s1.dX, s2.dX) && Math.Equals(s1.dY, s2.dY)
                 && Math.Equals(s1.dT, s2.dT) && Math.Equals(s1.dZ, s2.dZ);
        }

        public static bool operator !=(SPos_XYTZ s1, SPos_XYTZ s2)
        {
            return !(s1 == s2);
        }

        public static SPos_XYTZ operator +(SPos_XYTZ s1, SPos_XYTZ s2)
        {
            SPos_XYTZ s = new SPos_XYTZ();

            s.dX = s1.dX + s2.dX;
            s.dY = s1.dY + s2.dY;
            s.dT = s1.dT + s2.dT;
            s.dZ = s1.dZ + s2.dZ;

            return s;
        }

        public static SPos_XYTZ operator +(SPos_XYTZ s1, double dAdd)
        {
            SPos_XYTZ s = new SPos_XYTZ();

            s.dX = s1.dX + dAdd;
            s.dY = s1.dY + dAdd;
            s.dT = s1.dT + dAdd;
            s.dZ = s1.dZ + dAdd;

            return s;
        }

        public static SPos_XYTZ operator -(SPos_XYTZ s1, SPos_XYTZ s2)
        {
            SPos_XYTZ s = new SPos_XYTZ();

            s.dX = s1.dX - s2.dX;
            s.dY = s1.dY - s2.dY;
            s.dT = s1.dT - s2.dT;
            s.dZ = s1.dZ - s2.dZ;

            return s;
        }

        public static SPos_XYTZ operator -(SPos_XYTZ s1, double dSub)
        {
            SPos_XYTZ s = new SPos_XYTZ();

            s.dX = s1.dX - dSub;
            s.dY = s1.dY - dSub;
            s.dT = s1.dT - dSub;
            s.dZ = s1.dZ - dSub;

            return s;
        }

        public static SPos_XYTZ operator *(SPos_XYTZ s1, double dMul)
        {
            SPos_XYTZ s = new SPos_XYTZ();

            s.dX = s1.dX * dMul;
            s.dY = s1.dY * dMul;
            s.dT = s1.dT * dMul;
            s.dZ = s1.dZ * dMul;

            return s;
        }

        public static SPos_XYTZ operator /(SPos_XYTZ s1, double dDiv)
        {
            SPos_XYTZ s = new SPos_XYTZ();
            if (dDiv == 0) return s;

            s.dX = s1.dX / dDiv;
            s.dY = s1.dY / dDiv;
            s.dT = s1.dT / dDiv;
            s.dZ = s1.dZ / dDiv;

            return s;
        }
    }

    public class ChatServer
    {
        public class AsyncObject
        {
            public Byte[] Buffer;
            public Socket WorkingSocket;
            public AsyncObject(Int32 bufferSize)
            {
                this.Buffer = new Byte[bufferSize];
            }
        }

        private Socket m_ConnectedClient = null;
        private Socket m_ServerSocket = null;
        private AsyncCallback m_fnReceiveHandler;
        private AsyncCallback m_fnSendHandler;
        private AsyncCallback m_fnAcceptHandler;

        public ChatServer()
        {

            // 비동기 작업에 사용될 대리자를 초기화합니다.
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            m_fnSendHandler = new AsyncCallback(handleDataSend);
            m_fnAcceptHandler = new AsyncCallback(handleClientConnectionRequest);

        }

        public void StartServer(UInt16 port)
        {

            // TCP 통신을 위한 소켓을 생성합니다.
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            // 특정 포트에서 모든 주소로부터 들어오는 연결을 받기 위해 포트를 바인딩합니다.
            m_ServerSocket.Bind(new IPEndPoint(IPAddress.Any, port));

            // 연결 요청을 받기 시작합니다.
            m_ServerSocket.Listen(5);

            // BeginAccept 메서드를 이용해 들어오는 연결 요청을 비동기적으로 처리합니다.
            // 연결 요청을 처리하는 함수는 handleClientConnectionRequest 입니다.
            m_ServerSocket.BeginAccept(m_fnAcceptHandler, null);
        }

        public void StopServer()
        {
            // 가차없이 서버 소켓을 닫습니다.
            m_ServerSocket.Close();
        }

        public void SendMessage(String message)
        {
            // 추가 정보를 넘기기 위한 변수 선언
            // 크기를 설정하는게 의미가 없습니다.
            // 왜냐하면 바로 밑의 코드에서 문자열을 유니코드 형으로 변환한 바이트 배열을 반환하기 때문에
            // 최소한의 크기르 배열을 초기화합니다.
            AsyncObject ao = new AsyncObject(1);

            // 문자열을 바이트 배열으로 변환
            ao.Buffer = Encoding.Unicode.GetBytes(message);

            ao.WorkingSocket = m_ConnectedClient;

            // 전송 시작!
            try
            {
                m_ConnectedClient.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnSendHandler, ao);
            }
            catch (Exception ex)
            {
                Console.WriteLine("전송 중 오류 발생!\n메세지: {0}", ex.Message);
            }
        }



        private void handleClientConnectionRequest(IAsyncResult ar)
        {
            Socket sockClient;
            try
            {
                // 클라이언트의 연결 요청을 수락합니다.
                sockClient = m_ServerSocket.EndAccept(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("연결 수락 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
            AsyncObject ao = new AsyncObject(4096);

            // 작업 중인 소켓을 저장하기 위해 sockClient 할당
            ao.WorkingSocket = sockClient;

            // 클라이언트 소켓 저장
            m_ConnectedClient = sockClient;

            try
            {
                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                sockClient.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 수신 대기 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }
        }
        private void handleDataReceive(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            // AsyncState 속성의 자료형은 Object 형식이기 때문에 형 변환이 필요합니다~!
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 받은 바이트 수 저장할 변수 선언
            Int32 recvBytes;

            try
            {
                // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
                recvBytes = ao.WorkingSocket.EndReceive(ar);
            }
            catch
            {
                // 예외가 발생하면 함수 종료!
                return;
            }

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                // 공백 문자들이 많이 발생할 수 있으므로, 받은 바이트 수 만큼 배열을 선언하고 복사한다.
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                // 받은 메세지를 출력
                Console.WriteLine("메세지 받음: {0}", Encoding.Unicode.GetString(msgByte));
            }

            try
            {
                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 수신 대기 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }
        }
        private void handleDataSend(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 보낸 바이트 수를 저장할 변수 선언
            Int32 sentBytes;

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                // 여기도 마찬가지로 보낸 바이트 수 만큼 배열 선언 후 복사한다.
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                Console.WriteLine("메세지 보냄: {0}", Encoding.Unicode.GetString(msgByte));
            }
        }
    }

    public class ChatClient
    {
        public class AsyncObject
        {
            public Byte[] Buffer;
            public Socket WorkingSocket;
            public AsyncObject(Int32 bufferSize)
            {
                this.Buffer = new Byte[bufferSize];
            }
        }

        private Boolean g_Connected;
        private Socket m_ClientSocket = null;
        private AsyncCallback m_fnReceiveHandler;
        private AsyncCallback m_fnSendHandler;

        public ChatClient()
        {

            // 비동기 작업에 사용될 대리자를 초기화합니다.
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            m_fnSendHandler = new AsyncCallback(handleDataSend);

        }

        public Boolean Connected
        {
            get
            {
                return g_Connected;
            }
        }

        public void ConnectToServer(String hostName, UInt16 hostPort)
        {
            // TCP 통신을 위한 소켓을 생성합니다.
            m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            Boolean isConnected = false;
            try
            {
                // 연결 시도
                m_ClientSocket.Connect(hostName, hostPort);

                // 연결 성공
                isConnected = true;
            }
            catch
            {
                // 연결 실패 (연결 도중 오류가 발생함)
                isConnected = false;
            }
            g_Connected = isConnected;

            if (isConnected)
            {

                // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
                AsyncObject ao = new AsyncObject(4096);

                // 작업 중인 소켓을 저장하기 위해 sockClient 할당
                ao.WorkingSocket = m_ClientSocket;

                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                m_ClientSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);

                Console.WriteLine("연결 성공!");

            }
            else {

                Console.WriteLine("연결 실패!");

            }
        }

        public void StopClient()
        {
            // 가차없이 클라이언트 소켓을 닫습니다.
            m_ClientSocket.Close();
        }

        public void SendMessage(String message)
        {
            // 추가 정보를 넘기기 위한 변수 선언
            // 크기를 설정하는게 의미가 없습니다.
            // 왜냐하면 바로 밑의 코드에서 문자열을 유니코드 형으로 변환한 바이트 배열을 반환하기 때문에
            // 최소한의 크기르 배열을 초기화합니다.
            AsyncObject ao = new AsyncObject(1);

            // 문자열을 바이트 배열으로 변환
            ao.Buffer = Encoding.Unicode.GetBytes(message);

            ao.WorkingSocket = m_ClientSocket;

            // 전송 시작!
            try
            {
                m_ClientSocket.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnSendHandler, ao);
            }
            catch (Exception ex)
            {
                Console.WriteLine("전송 중 오류 발생!\n메세지: {0}", ex.Message);
            }
        }

        private void handleDataReceive(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            // AsyncState 속성의 자료형은 Object 형식이기 때문에 형 변환이 필요합니다~!
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 받은 바이트 수 저장할 변수 선언
            Int32 recvBytes;

            try
            {
                // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
                recvBytes = ao.WorkingSocket.EndReceive(ar);
            }
            catch
            {
                // 예외가 발생하면 함수 종료!
                return;
            }

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                // 공백 문자들이 많이 발생할 수 있으므로, 받은 바이트 수 만큼 배열을 선언하고 복사한다.
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                // 받은 메세지를 출력
                Console.WriteLine("메세지 받음: {0}", Encoding.Unicode.GetString(msgByte));
            }

            try
            {
                // 자료 처리가 끝났으면~
                // 이제 다시 데이터를 수신받기 위해서 수신 대기를 해야 합니다.
                // Begin~~ 메서드를 이용해 비동기적으로 작업을 대기했다면
                // 반드시 대리자 함수에서 End~~ 메서드를 이용해 비동기 작업이 끝났다고 알려줘야 합니다!
                ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 수신 대기 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }
        }
        private void handleDataSend(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 보낸 바이트 수를 저장할 변수 선언
            Int32 sentBytes;

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                // 여기도 마찬가지로 보낸 바이트 수 만큼 배열 선언 후 복사한다.
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                Console.WriteLine("메세지 보냄: {0}", Encoding.Unicode.GetString(msgByte));
            }
        }
    }
}

namespace System
{
    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(Object).GetTypeInfo().GetDeclaredMethod("MemberwiseClone");

        private static bool IsValue(this Type type)
        {
            if (type == typeof(String)) return true;
            return type.GetTypeInfo().IsValueType;
        }

        private static Object Copy(this Object originalObject)
        {
            return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }
        private static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsValue(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).GetTypeInfo().IsAssignableFrom(typeToReflect.GetTypeInfo())) return null;
            var cloneObject = CloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsValue(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect, info => !info.IsStatic && !info.FieldType.GetTypeInfo().IsPrimitive);
            RecursiveCopyBaseTypeFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypeFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.GetTypeInfo().BaseType != null)
            {
                RecursiveCopyBaseTypeFields(originalObject, visited, cloneObject, typeToReflect.GetTypeInfo().BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.GetTypeInfo().BaseType, info => !info.IsStatic && !info.FieldType.GetTypeInfo().IsPrimitive);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, Predicate<FieldInfo> filter = null)
        {
            List<FieldInfo> filtered = new List<FieldInfo>(typeToReflect.GetTypeInfo().DeclaredFields);
            if (filter != null)
            {
                filtered = filtered.FindAll(filter);
            }
            foreach (FieldInfo fieldInfo in filtered)
            {
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }

        public static T Copy<T>(this T original)
        {
            return (T)Copy((Object)original);
        }

        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            T someObject = new T();
            Type someObjectType = someObject.GetType();

            foreach (KeyValuePair<string, object> item in source)
            {
                someObjectType.GetProperty(item.Key).SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }

        public static T ObjectFromDictionary<T>(IDictionary<string, object> dict) where T : class
        {
            Type type = typeof(T);
            T result = (T)Activator.CreateInstance(type);
            foreach (var item in dict)
            {
                type.GetProperty(item.Key).SetValue(result, item.Value, null);
            }
            return result;
        }

        public static IDictionary<string, object> ObjectToDictionary<T>(T item) // where T : class
        {
            Type myObjectType = item.GetType();
            IDictionary<string, object> dict = new Dictionary<string, object>();
            var indexer = new object[0];
            PropertyInfo[] properties = myObjectType.GetProperties();
            foreach (var info in properties)
            {
                var value = info.GetValue(item, indexer);
                dict.Add(info.Name, value);
            }
            return dict;
        }

        public static T[,] To2DArray<T>(this List<List<T>> lst)
        {

            if ((lst == null) || (lst.Any(subList => subList.Any() == false)))
                throw new ArgumentException("Input list is not properly formatted with valid data");

            int index = 0;
            int subindex;

            return

               lst.Aggregate(new T[lst.Count(), lst.Max(sub => sub.Count())],
                             (array, subList) =>
                             {
                                 subindex = 0;
                                 subList.ForEach(itm => array[index, subindex++] = itm);
                                 ++index;
                                 return array;
                             });
        }

        // 아래의 ToStringDictionary, FromStringDictionary functions are pair set by ranian
        public static Dictionary<string, string> ToStringDictionary(this object source, Type type) 
        {
            Dictionary<string, string> fieldBook = new Dictionary<string, string>();

            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                // 1.1 element
                if (field.FieldType.IsValueType || field.FieldType.Name.ToLower() == "string")
                {
                    fieldBook.Add(field.Name, field.GetValue(source)?.ToString());
                }
                // 1.2 array
                else if (field.FieldType.IsArray)
                {
                    Array array = (Array)field.GetValue(source);

                    // 1.2.1 1-D array
                    if (array.Rank == 1)
                    {
                        for (int i = 0; i < array.GetLength(0); i++)
                        {
                            fieldBook.Add($"{field.Name}__{i}", array.GetValue(i)?.ToString());
                        }
                    }
                    // 1.2.2 2-D array
                    else if (array.Rank == 2)
                    {
                        for (int i = 0; i < array.GetLength(0); i++)
                        {
                            for (int j = 0; j < array.GetLength(1); j++)
                            {
                                fieldBook.Add($"{field.Name}__{i}__{j}", array.GetValue(i, j)?.ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Not support {field.Name}'s array {array.Rank} dimension.");
                    }
                }
                else
                {
                    Console.WriteLine($"Not support to handle {field.Name}'s {field.FieldType.ToString()}");
                }
            }

            for (int i = 0; i < fieldBook.Count; i++)
            {
                if (fieldBook.Values.ToList()[i] == null)
                {
                    fieldBook[fieldBook.Keys.ToList()[i]] = "";
                }

            }

            return fieldBook;
        }

        public static void FromStringDicionary(object target, Type type, Dictionary<string, string> fieldBook)
        {
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                // 3.1 handle element
                if (field.FieldType.IsValueType || field.FieldType.Name.ToLower() == "string")
                {
                    if (fieldBook.ContainsKey(field.Name))
                    {
                        SetFieldValue(target, field, fieldBook[field.Name]);
                    }
                }
                // 3.2 handle array
                else if (field.FieldType.IsArray)
                {
                    Array array = (Array)field.GetValue(target);
                    string key, value;

                    // 3.2.1 1-D array
                    if (array.Rank == 1)
                    {
                        var arr_1d = new string[array.GetLength(0)];
                        for (int i = 0; i < array.GetLength(0); i++)
                        {
                            key = $"{field.Name}__{i}";
                            value = fieldBook.ContainsKey(key) ? fieldBook[key] : "";
                            arr_1d.SetValue(value, i);
                        }
                        SetFieldValue(target, field, arr_1d);
                    }
                    // 3.2.1 2-D array
                    else if (array.Rank == 2)
                    {
                        var arr_2d = new string[array.GetLength(0), array.GetLength(1)];
                        for (int i = 0; i < array.GetLength(0); i++)
                        {
                            for (int j = 0; j < array.GetLength(1); j++)
                            {
                                key = $"{field.Name}__{i}__{j}";
                                value = fieldBook.ContainsKey(key) ? fieldBook[key] : "";
                                arr_2d.SetValue(value, i, j);
                            }
                        }
                        SetFieldValue(target, field, arr_2d);
                    }
                    else
                    {
                        Console.WriteLine($"Not support {field.Name}'s array {array.Rank} dimension.");
                    }
                }
                // 3.3 not support
                else
                {
                    Console.WriteLine($"Not support to handle {field.Name}'s {field.FieldType.ToString()}");
                }
            }
        }

        private static void SetFieldValue(Object target, FieldInfo fieldInfo, string value)
        {
            string fieldType = fieldInfo.FieldType.Name;
            fieldType = fieldType.ToLower();

            switch (fieldType)
            {
                case "boolean":
                    bool b;
                    fieldInfo.SetValue(target, bool.TryParse(value, out b) ? b : false);
                    break;

                case "int32":
                    int n;
                    fieldInfo.SetValue(target, int.TryParse(value, out n) ? n : 0);
                    break;

                case "double":
                    double d;
                    fieldInfo.SetValue(target, double.TryParse(value, out d) ? d : 0);
                    break;

                case "string":
                    fieldInfo.SetValue(target, value ?? "");
                    break;
            }
        }

        private static void SetFieldValue(Object target, FieldInfo fieldInfo, string[] arr)
        {
            string fieldType = fieldInfo.FieldType.GetElementType().Name;
            fieldType = fieldType.ToLower();

            switch (fieldType)
            {
                case "boolean":
                    bool b;
                    bool[] arr_b = Array.ConvertAll(arr, s => bool.TryParse(s, out b) ? b : false);
                    fieldInfo.SetValue(target, arr_b);
                    break;

                case "int32":
                    int n;
                    int[] arr_n = Array.ConvertAll(arr, s => int.TryParse(s, out n) ? n : 0);
                    //int[] arr_n1 = Array.ConvertAll(arr, int.Parse);
                    //int[] arr_n2 = arr.Select(s => int.TryParse(s, out n) ? n : 0).ToArray();
                    fieldInfo.SetValue(target, arr_n);
                    break;

                case "double":
                    double d;
                    double[] arr_d = Array.ConvertAll(arr, s => double.TryParse(s, out d) ? d : 0);
                    fieldInfo.SetValue(target, arr_d);
                    break;

                case "string":
                    fieldInfo.SetValue(target, arr);
                    break;
            }
        }

        private static void SetFieldValue(Object target, FieldInfo fieldInfo, string[,] arr)
        {
            string fieldType = fieldInfo.FieldType.GetElementType().Name;
            fieldType = fieldType.ToLower();

            // 0. string return
            switch (fieldType)
            {
                case "string":
                    fieldInfo.SetValue(target, arr);
                    return;
                    break;
            }

            // 1. initialize
            int n;
            double d;
            bool b;

            dynamic output = Array.CreateInstance(fieldInfo.FieldType.GetElementType(), arr.GetLength(0), arr.GetLength(1));
            var converter = TypeDescriptor.GetConverter(fieldInfo.FieldType.GetElementType());

            // 2. convert
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    switch (fieldType)
                    {
                        case "boolean":
                            output[i, j] = (bool)converter.ConvertFromString((string)arr[i, j]);
                            break;

                        case "int32":
                            output[i, j] = (int)converter.ConvertFromString((string)arr[i, j]);
                            break;

                        case "double":
                            output[i, j] = (double)converter.ConvertFromString((string)arr[i, j]);
                            break;
                    }
                }
            }

            // 2. setvalue
            fieldInfo.SetValue(target, output);
        }

    }

    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

    namespace ArrayExtensions
    {
        public static class ArrayExtensions
        {
            public static void ForEach(this Array array, Action<Array, int[]> action)
            {
                if (array.Length == 0) return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do action(array, walker.Position);
                while (walker.Step());
            }
        }

        internal class ArrayTraverse
        {
            public int[] Position;
            private int[] maxLengths;

            public ArrayTraverse(Array array)
            {
                maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }

}