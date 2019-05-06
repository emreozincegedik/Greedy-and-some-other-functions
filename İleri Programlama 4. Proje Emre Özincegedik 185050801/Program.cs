using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İleri_Programlama_4.Proje_Emre_Özincegedik_185050801
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> jobs = new List<int>
            {
                16, 51, 20, 45, 20, 2, 42, 50, 26, 16, 25, 3, 13, 14, 38, 15, 48, 32, 55, 7, 35,
                46, 11, 5, 51, 56, 40, 38, 5, 23, 5, 55, 58, 32, 6, 24, 31, 19, 56, 54, 27, 15, 1,
                7, 31, 27, 58, 19, 58, 6, 7, 26, 49, 51, 42, 29, 41, 16, 53, 24, 21, 4, 45, 4, 12,
                30, 5, 41, 9, 14, 44, 30, 35, 1, 40, 20, 46, 4, 34, 25, 58, 21, 40, 59, 16, 38, 6,
                8, 50, 36, 42, 16, 26, 32, 34, 23, 29, 57, 55, 1
            };

            List<int> dummy=new List<int>();
            Random rnd=new Random();
            for (int i = 0; i < 1000; i++)
            {
                dummy.Add(rnd.Next(100));
            }

            Console.WriteLine("\nOptimum time is: " + Math.Ceiling((float)jobs.Take(jobs.Count).Sum()/4) +"\n\n");
            Stopwatch st =new Stopwatch();
            st.Start();
            Func(dummy, "Greedy");
            st.Stop();
            Console.WriteLine("time is: "+st.ElapsedMilliseconds);
            st.Reset();
            st.Start();
            Func(dummy, "My Method");
            st.Stop();
            Console.WriteLine("time is: " + st.ElapsedMilliseconds);
            st.Reset();
            /*st.Start();
            Func(dummy, "Min");
            st.Stop();
            Console.WriteLine("time is: " + st.ElapsedMilliseconds);
            st.Reset();
            st.Start();
            Func(dummy, "Toggle Max");
            st.Stop();
            Console.WriteLine("time is: " + st.ElapsedMilliseconds);
            st.Reset();
            st.Start();
            Func(dummy, "Toggle Min");
            st.Stop();
            Console.WriteLine("time is: " + st.ElapsedMilliseconds);
            st.Reset();*/
            
            Console.ReadLine();
        }
        static void Func(List<int> jobs,string which)
        {
            bool toggle = true, toggle_question = true, max = false,mytest=false; //changes depending on the wanted function
            List<int> 
                P1 = new List<int>(), //processors
                P2 = new List<int>(), //processors
                P3 = new List<int>(), //processors
                P4 = new List<int>(), //processors
                jobss = jobs.ToList(); //copy jobs so original stays same
            List<string> l2 = jobss.ConvertAll<string>(x => x.ToString()), //used for getting the index of item before removal so the list length doesn't change

                P2_job = new List<string>(),  //processor job indexes
                P1_job = new List<string>(),  //processor job indexes
                P3_job = new List<string>(),  //processor job indexes
                P4_job = new List<string>();  //processor job indexes
            if (which=="Greedy") //define the function to greedy variation
            {
                toggle_question = toggle = false;
                max = true;
            }
            else if (which=="Min") //define the function to Min variation
                toggle_question = toggle = false;
            else if (which=="Toggle Min") //define the function to Toggle Min variation
                toggle = false;
            else if (which=="My Method")
            {
                toggle_question = toggle = false;
                mytest = true;
            }
            Console.WriteLine($"Starting {which} function");

            for (int i = 0; i < jobs.Count; i++)
            {
                //var median = jobss.Aggregate((x, y) => Math.Abs(x - jobss.Average()) < Math.Abs(y - jobss.Average()) ? x : y); //gives closest to the average of jobss
                var median = jobss.Aggregate((x, y) => Math.Abs(x - jobss.Average()) > Math.Abs(y - jobss.Average()) ? x : y); //gives furthest to the average of jobss
                if (Math.Min(Math.Min(P1.Take(P1.Count).Sum(), P2.Take(P2.Count).Sum()),
                        Math.Min(P3.Take(P3.Count).Sum(), P4.Take(P4.Count).Sum())) == P1.Take(P1.Count).Sum()) //if the sum of each processors min is P1
                {
                    if ((toggle || max)&&!mytest) //get the max of remaining items
                    {
                        P1.Add(jobss.Max()); //add the max of remaining items to P1
                        P1_job.Add("J"+l2.IndexOf(jobss.Max().ToString())); //add the item's index in original array
                        l2[l2.IndexOf(jobss.Max().ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Max()); //remove used item
                    }
                    else if(!mytest) //get the min of remaining items
                    {
                        P1.Add(jobss.Min()); //add min of jobss to P1
                        P1_job.Add("J" + l2.IndexOf(jobss.Min().ToString())); //add the item's index in original array
                        l2[l2.IndexOf(jobss.Min().ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Min()); //remove used item
                    }
                    else//get the median variable of remaining items
                    {
                        P1.Add(median); //add median variable to P1
                        P1_job.Add("J" + l2.IndexOf(median.ToString())); //add the item's index in original array
                        l2[l2.IndexOf(median.ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(median); //remove  used item
                    }
                }
                else if (Math.Min(Math.Min(P1.Take(P1.Count).Sum(), P2.Take(P2.Count).Sum()),
                             Math.Min(P3.Take(P3.Count).Sum(), P4.Take(P4.Count).Sum())) == P2.Take(P2.Count).Sum())  //if the sum of each processors min is P2
                {
                    if ((toggle || max)&&!mytest) //get the max of remaining items
                    {
                        P2.Add(jobss.Max()); //add the max of remaining items to P2
                        P2_job.Add("J" + l2.IndexOf(jobss.Max().ToString())); //add the item's index in original array
                        l2[l2.IndexOf(jobss.Max().ToString())] = null;   //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Max());  //remove used item
                    }
                    else if (!mytest)//get the min of remaining items
                    {
                        P2.Add(jobss.Min());  //add min of jobss to P2
                        P2_job.Add("J" + l2.IndexOf(jobss.Min().ToString())); //add the item's index in original array
                        l2[l2.IndexOf(jobss.Min().ToString())] = null;  //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Min()); //remove used item
                    }
                    else //get the median variable of remaining items
                    {
                        P2.Add(median);//add median variable to P2
                        P2_job.Add("J" + l2.IndexOf(median.ToString())); //add the item's index in original array
                        l2[l2.IndexOf(median.ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(median);  //remove used item
                    }
                }
                else if (Math.Min(Math.Min(P1.Take(P1.Count).Sum(), P2.Take(P2.Count).Sum()),
                             Math.Min(P3.Take(P3.Count).Sum(), P4.Take(P4.Count).Sum())) == P3.Take(P3.Count).Sum())  //if the sum of each processors min is P3
                {
                    if ((toggle || max)&&!mytest) //get the max of remaining items
                    {
                        P3.Add(jobss.Max()); //add the max of remaining items to P3
                        P3_job.Add("J" + l2.IndexOf(jobss.Max().ToString())); //add the item's index in original array
                        l2[l2.IndexOf(jobss.Max().ToString())] = null;  //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Max()); //remove used item
                    }
                    else if(!mytest)//get the min of remaining items
                    {
                        P3.Add(jobss.Min()); //add min of jobss to P3
                        P3_job.Add("J" + l2.IndexOf(jobss.Min().ToString()));//add the item's index in original array
                        l2[l2.IndexOf(jobss.Min().ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Min()); //remove used item
                    }
                    else//get the median variable of remaining items
                    {
                        P3.Add(median);//add median variable to P3
                        P3_job.Add("J" + l2.IndexOf(median.ToString())); //add the item's index in original array
                        l2[l2.IndexOf(median.ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(median);  //remove used item
                    }
                }
                else  //if the sum of each processors min is P4
                {
                    if ((toggle || max)&&!mytest) //get the max of remaining items
                    {
                        P4.Add(jobss.Max());
                        P4_job.Add("J" + l2.IndexOf(jobss.Max().ToString()));
                        l2[l2.IndexOf(jobss.Max().ToString())] = null;
                        jobss.Remove(jobss.Max());
                    }
                    else if(!mytest)//get the min of remaining items
                    {
                        P4.Add(jobss.Min()); //add the max of remaining items to P4
                        P4_job.Add("J" + l2.IndexOf(jobss.Min().ToString()));//add the item's index in original array
                        l2[l2.IndexOf(jobss.Min().ToString())] = null;//change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(jobss.Min());//remove used item
                    }
                    else//get the median variable of remaining items
                    {
                        P4.Add(median);//add median variable to P3
                        P4_job.Add("J" + l2.IndexOf(median.ToString()).ToString()); //add the item's index in original array
                        l2[l2.IndexOf(median.ToString())] = null; //change the used item in l2 to null so it doesn't get used for future
                        jobss.Remove(median); //remove used item
                    }
                }
                if (toggle_question) //activate when the method is toggleable
                    toggle = !toggle; //toggle the state
            }

            Console.WriteLine("P1 total is: " + P1.Take(P1.Count).Sum());
            Console.WriteLine(string.Join("\t", P1_job));
            Console.WriteLine(string.Join("\t", P1)+"\n");

            Console.WriteLine("P2 total is: " + P2.Take(P2.Count).Sum());
            Console.WriteLine(string.Join("\t", P2_job));
            Console.WriteLine(string.Join("\t", P2) + "\n");

            Console.WriteLine("P3 total is: " + P3.Take(P3.Count).Sum());
            Console.WriteLine(string.Join("\t", P3_job));
            Console.WriteLine(string.Join("\t", P3) + "\n");

            Console.WriteLine("P4 total is: " + P4.Take(P4.Count).Sum());
            Console.WriteLine(string.Join("\t", P4_job));
            Console.WriteLine(string.Join("\t", P4) + "\n");

            Console.WriteLine(which+" Total time is: " + Math.Max(Math.Max(P1.Take(P1.Count).Sum(), P2.Take(P2.Count).Sum()),
                                  Math.Max(P3.Take(P3.Count).Sum(), P4.Take(P4.Count).Sum())));
            Console.WriteLine("\n");

           
            using (StreamWriter str = new StreamWriter(which+".txt")) //write to the file according to the method name
            {
                str.WriteLine("P1 total is: " + P1.Take(P1.Count).Sum());
                for (int i = 0; i < P1.Count; i++)
                    str.Write(P1_job[i]+":"+P1[i]+"\t"); 

                str.WriteLine("\n\nP2 total is: " + P2.Take(P2.Count).Sum());
                for (int i = 0; i < P2.Count; i++)
                    str.Write(P2_job[i] + ":" + P2[i] + "\t");

                str.WriteLine("\n\nP3 total is: " + P3.Take(P3.Count).Sum());
                for (int i = 0; i < P3.Count; i++)
                    str.Write(P3_job[i] + ":" + P3[i] + "\t");

                str.WriteLine("\n\nP4 total is: " + P4.Take(P4.Count).Sum());
                for (int i = 0; i < P4.Count; i++)
                    str.Write(P4_job[i] + ":" + P4[i] + "\t");

                str.WriteLine("\n\n" + which + " Total time is: " + Math.Max(Math.Max(P1.Take(P1.Count).Sum(), P2.Take(P2.Count).Sum()),
                                   Math.Max(P3.Take(P3.Count).Sum(), P4.Take(P4.Count).Sum())));
            }
            
        }
    }
}
