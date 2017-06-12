using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class createProjectController : ApiController
    {
       
        // POST: api/createProject
        public IEnumerable<String> Post([FromBody]Project proj)
        {
            //for logging the request and response into log file
            String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
            //if directory ~/logFiles/ doesn't exists
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "log.txt";
            //if file doesn't exists
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }

            proj.creationDate = DateTime.Now;

            if (proj.ProjectId == null || proj.projectName == null || proj.projectURL == null || proj.enabled == null || proj.expiryDate == null)
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String log = DateTime.Now.ToString() +"\n Creation of Project Failed : Requirements not satisfied";
                    
                    sw.WriteLine(log);
                    sw.Flush();
                    sw.Close();
                }

                String[] p = { "Project ID, Project Name, Project URL , Status of the project and expiry Date are Required Fields. Please Check if you have not mentioned any of those." };
                return p;
            }
            
            String[] new_project = new String[1];
            //creating an empty list of type Project(project model) to store the project
            List<Project> prList = new List<Project>();
            prList.Add(proj);


            //Storing file location 
            string fileLocation = "C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
            //if no such file exixts, then create a new file
            if (!File.Exists(fileLocation))
            {
                File.WriteAllText(@"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt", JsonConvert.SerializeObject(proj));
                using (StreamWriter file = File.CreateText("C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, prList);

                }
            }
            else
            {
                //Loading file
                var filePath = @"C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/project.txt";
                // Read existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serialize to object or create new list
                var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonData)
                                      ?? new List<Project>();
                foreach (Project project in projectList)
                {
                    if (project.ProjectId == proj.ProjectId)
                    {
                        Debug.WriteLine("A project with id=" + proj.ProjectId + " already exists");
                        String[] p = { "A project with id=" + proj.ProjectId + " already exists" };
                        using (StreamWriter sw = File.AppendText(fileName))
                        {
                            String log = DateTime.Now.ToString() + "\n Project with id: "+proj.ProjectId+" already exists. Attempt Failed";
                            sw.WriteLine(log);
                            sw.Flush();
                            sw.Close();
                        }
                        return p;
                    }
                }
                // Adding project to the json file
                projectList.Add(proj);

                // Update json data string
                jsonData = JsonConvert.SerializeObject(projectList);
                System.IO.File.WriteAllText(filePath, jsonData);
            }

            new_project[0] = "Project has been created and saved Successfully";

           //Logging into log file
            using (StreamWriter sw = File.AppendText(fileName))
            {
                String log = "";
                if (new_project[0] == null)
                {
                    log = DateTime.Now.ToString() + "\n" + "Attempt to create a project with id: " + proj.ProjectId + " has been failed \n";
                }else
                {
                    log = DateTime.Now.ToString() + "\n" + "Project created ans saved successfully";
                }
                sw.WriteLine(log);
                sw.Flush();
                sw.Close();
            }


            Debug.WriteLine("\n----Campaign is successfully created----\n");
            return new_project;
        }
        
    }
}
