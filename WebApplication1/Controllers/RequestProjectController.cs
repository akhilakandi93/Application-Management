using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace WebApplication1.Controllers
{
    public class RequestProjectController : ApiController
    {
        public IEnumerable<string> Get()
        {
            double max = 0;
            int id = -1;
            try
            {
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Read existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serialize to object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                foreach (Project proj in projectList)
                {

                    string s = proj.expiryDate.ToString();
                    DateTime dat = Convert.ToDateTime(s);
                    //Current Time
                    DateTime datenow = System.DateTime.Now;

                    //Checking if expiry date is valid or not 
                    //Checking if project is enabled or not
                    //Checking if ProjectUrl is null or not
                    if (proj.enabled == true && proj.projectURL != null && dat > datenow)
                    {
                        //Getting the maximum projectCost of all the projects
                        if (proj.projectCost > max)
                        {
                            max = proj.projectCost;
                            //Storing the id of the project that has maximum Project Cost
                            id = proj.ProjectId;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //if given path is not correct
                Debug.WriteLine("File Not Found");
                String[] p = { "File Not Found" };
                return p;
            }
            //Printing to console
            Debug.WriteLine("Project Loaded");

            //Logging into log file
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                String log = DateTime.Now.ToString() + "\n" +"Project with maximum Project Cost has been loaded" + "\n";
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }
            
            //returning the list of project containing project name, project url, project cost
            return requestProject(id);
        }

        // GET: api/RequestProject/5
        public IEnumerable<string> Get(int id)
        {
            //returns the project with the given id
            
            return requestProject(id);

        }


        public IEnumerable<string> requestProject(int id)
        {
            //Opening the directory of logFiles to store logs
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            //if directory doesn't exists, then create one
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            //if file foesn't exists , then create one
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            //creating an array to store project name, project url and project cost
            string[] project = new string[3];
           
           try { 
                //storing the file location
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Reading the existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serializing object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                foreach (Project proj in projectList)
                {

                    if (proj.ProjectId == id)
                    {
                        string s = proj.expiryDate.ToString();
                        DateTime dat = Convert.ToDateTime(s);
                        DateTime datenow = System.DateTime.Now;

                    //checking if the project is enabled
                   //checking if the projecturl is not null
                   //checking if expiry date is valid

                        if (proj.enabled == true && proj.projectURL != null && dat > datenow)
                        {
                            project[0] = "Project Name : "+proj.projectName;
                            project[1] = "Project Cost : "+proj.projectCost.ToString();
                            project[2] = "Project URL : "+proj.projectURL;
                        }

                    }
                }
                //logging to log file
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String log = "";
                    //if no such project exists
                    if (project[0] == null)
                    {
                        String[] p = { "No Such valid Project found" };
                        //writing to console 
                        Debug.WriteLine("No Such valid Project found");
                        log = DateTime.Now.ToString() + " \n" + "No valid Project with id-" + id + "Not found.\n";
                        //writing to log file
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                        return p;

                    }
                    else
                    {
                        log = DateTime.Now.ToString() + " \n" + "Project with id-" + id + "has been loaded.\n";
                        //writing to log file
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }

                }

                return project;
            }
            catch (Exception)
            {
                //if file location is not valid
                String[] p = { "File Not Found" };
                Debug.WriteLine("File not found");

                return p;
            }
           
        }

        //returning project with the given country, number and keyword
        // GET: api/RequestProject?country=usa&number=10&keyword=movies
        public IEnumerable<string> Get(string country, int number, string keyword)
        {

            //Opening the directory of logFiles to store logs
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            //if directory doesn't exists, then create one
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            //if file foesn't exists , then create one
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            //creating an array to store project name, project url and project cost
            string[] project = new string[3];
            double max = 0;
            try
            {
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Reading the existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serializing the object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                foreach (Project proj in projectList)
                {
                    //checking if such country exists in its list
                    //checking if such keyword   exists in its list
                    //checking if such number is greater than or equal to that exists in its list
                    if (proj.targetCountries.Contains(country.ToString().ToUpper()) && proj.projectCost > max &&  proj.getKeyWords(keyword) && proj.getNumbers(number))
                        {
                            max = proj.projectCost;
                        project[0] = "Project Name : " + proj.projectName;
                        project[1] = "Project Cost : " + proj.projectCost.ToString();
                        project[2] = "Project URL : " + proj.projectURL;
                    }
                    
                }
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String log = "";
                    if (project[0] == null)
                    {
                        //If no such project
                        String[] p = { "No such project where country=" + country + " , keyword=" + keyword + " and number=" + number + " is found" };
                        Debug.WriteLine("No such project where country=" + country + " , keyword=" + keyword + " and number=" + number + " is found");
                        log= DateTime.Now.ToString() + "\n" + "No such project where country=" + country + " , keyword=" + keyword + " and number=" + number + " is found \n";
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                        return p;

                    }else
                    {
                        log = DateTime.Now.ToString() + "\n" + "Project with country= " + country + " keyword= " + keyword + " number= " + number + " has been loaded" + "\n";
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //if file not found
                String[] p = { "File Not Found" };
                Debug.WriteLine("File not found");
                return p;
            }
           
            //returning the project
            return project;
        }

        // GET: api/RequestProject?country=usa
        public IEnumerable<string> Get(String country)
        {
            //Opening the directory of logFiles to store logs
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            //if directory doesn't exists, then create one
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            //if file foesn't exists , then create one
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            String[] project = new String[3];
            double max = 0;
            try
            {
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Reading the existing json data
                Debug.WriteLine("Just testing..........");
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serializing the object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                Debug.WriteLine("Came till here..");
                foreach (Project proj in projectList)
                {
                    //Checking if such country exists in the list of countries of the project
                    if (proj.targetCountries.Contains(country.ToString().ToUpper()))
                    {
                        if (proj.projectCost > max)
                        {
                            max = proj.projectCost;
                            project[0] = "Project Name : " + proj.projectName;
                            project[1] = "Project Cost : " + proj.projectCost.ToString();
                            project[2] = "Project URL : " + proj.projectURL;
                        }
                    }
                }
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String log = "";
                    //if no such exists
                    if (project[0] == null)
                    {
                        //printing to console
                        String[] p = { "No such project found where country= " + country };
                        Debug.WriteLine("No such project found where country= " + country);
                        log = DateTime.Now.ToString() + "\n" + "No project that has country= " + country + " was found" + "\n";
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                        return p;
                    }
                    else
                    {
                        log = DateTime.Now.ToString() + "\n" + "Project with country= " + country + " has been loaded" + "\n";
                        sw.WriteLine(log);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //file not found
                String[] p = { "File Not Found" };
                Debug.WriteLine("File not found");
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String log = DateTime.Now.ToString() + "\n" + "File Not Found Exception\n";
                    sw.WriteLine(log);
                    sw.Flush();
                    sw.Close();
                }
                return p;
            }
           
            return project;
        }

        //GET: api/RequestProject?id=2&country=usa&number=10
        public IEnumerable<string> Get(int id, String country, int number)
        {
            String[] project = new String[3];
            Debug.WriteLine("");
            try
            {
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Reading from the existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serializing the object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                foreach(Project proj in projectList)
                {
                    //if project with id , id exists
                    if (proj.ProjectId == id)
                    {
                        project[0] = "Project Name : " + proj.projectName;
                        project[1] = "Project Cost : " + proj.projectCost.ToString();
                        project[2] = "Project URL : " + proj.projectURL;

                    }
                }
                if (project[0] == null)
                {
                    //if no such project exists
                    String[] p = { "No such project where id=" + id + ", country=" + country + " and number=" + number};
                    Debug.WriteLine("No such project where id=" + id + ", country=" + country + " and number=" + number);
                    return p;
                }
                }catch(Exception ex)
            {
                //if file path is not found
                Debug.WriteLine("File Not found");
            }
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                String log = DateTime.Now.ToString() + "\n" + "Project with id="+id+" country= "+country+" number="+number + "has been loaded\n";
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }


            return project;
        }


        //GET: api/RequestProject?country=usa&number=10
        public IEnumerable<string> Get(String country, int number)
        {
            String[] project = new String[3];
            Debug.WriteLine("Copying all Project Files");
            try
            {
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Reading from the existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serializing the object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();

                foreach(Project proj in projectList)
                {
                    string s = (proj.expiryDate.ToString());
                    DateTime dat = Convert.ToDateTime(s);
                    DateTime datenow = System.DateTime.Now;

                    //checking if the expiry date is valid
                    //checking if the project url is not null
                    //checking if the project os enabled or not
                    if (proj.enabled == true && proj.projectURL != null && dat>datenow)
                    {
                        //checking if a country exists in the list of countries of the project
                        if (proj.targetCountries.Contains(country.ToString().ToUpper()))
                        {
                            // if(proj.target_Keys.Contains(x=>int.Parse()))

                            //checking if a number exists with greater than or equal to the given number
                            if (proj.getNumbers(number))
                            {
                                project[0] = "Project Name : " + proj.projectName;
                                project[1] = "Project Cost : " + proj.projectCost.ToString();
                                project[2] = "Project URL : " + proj.projectURL;
                            }
                        }
                    }
                    }
                if (project[0] == null)
                {
                    String[] p = { "No such project where country= " + country + " and number=" + number};
                    //if no such project exists
                    Debug.WriteLine("No such project where country= "+country+" and number="+number);
                    return p;
                }
               
                }catch(Exception)
            {
                //if file path is not correct
                Debug.WriteLine("File Not Found");
               
            }
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            using (StreamWriter sw = File.AppendText(fileName))
            {
                String error = DateTime.Now.ToString() + "\n" + "Project with country= "+country+" number="+number + "has been loaded \n";
                sw.WriteLine(error);
                sw.Flush();
                sw.Close();
            }

            return project;
        }

    }
}
