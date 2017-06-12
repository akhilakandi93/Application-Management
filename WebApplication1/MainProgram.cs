using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1
{
    public class MainProgram
    {
        public  void startProject()
        {
            Project proj = new Project();
            proj.ProjectId = 1;
            proj.projectName = "Test Project Number 1";
            proj.creationDate = DateTime.Now;
            DateTime startDate = DateTime.Now;
            DateTime expiryDate = startDate.AddDays(30);
            proj.enabled = true;
            proj.expiryDate = expiryDate;
            proj.projectCost = 5.5;
            proj.projectURL = "http://www.unity3d.com";
            proj.targetCountries = new List<string>(new string[] { "USA", "CANADA", "MEXICO", "BRAZIL" });
            //proj.target_Keys.number = 25;
            //proj.target_Keys.keyword = "movie";
            createProject(proj);
            DB2XMLPersistance(proj);
        }


        //copying into XML file

        public static void DB2XMLPersistance(Project proj)
        {
            System.Xml.Linq.XDocument XD = new XDocument();
            XD.Declaration = new XDeclaration("1.0", "utf-8", "yes");


            XElement rootDB = new XElement("Project");
            XD.Add(rootDB);


            XElement ID = new XElement("projectID", proj.ProjectId);
            rootDB.Add(ID);

            XElement name = new XElement("projectName", proj.projectName);
            rootDB.Add(name);

            XElement creationTime = new XElement("creationDate", proj.creationDate);
            rootDB.Add(creationTime);

            XElement expiryDate = new XElement("expiryDate", proj.expiryDate);
            rootDB.Add(expiryDate);

            XElement enabled = new XElement("enabled", proj.enabled);
            rootDB.Add(enabled);

            foreach (String key in proj.targetCountries)
            {
                XElement country = new XElement("Country", key);
                rootDB.Add(country);
            }

            XElement projectCost = new XElement("projectCost", proj.projectCost);
            rootDB.Add(projectCost);

            XElement projectURL = new XElement("projectURL", proj.projectURL);
            rootDB.Add(projectURL);

            

            XD.Save("C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/DBase.xml");

        }


        //copying ito txt file

        public static void createProject(Project proj)
        {
            String fileName = "C:/Users/Akhila Kandi/Documents/Visual Studio 2015/Projects/WebApplication1/WebApplication1/projectFiles/";
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            fileName = fileName + "projects.txt";
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Dispose();
            }
            
            using (StreamWriter sw = File.AppendText(fileName))
            {
                proj.creationDate = DateTime.Now;
                DateTime startDate = DateTime.Now;
                DateTime expiryDate = startDate.AddDays(30);
                proj.expiryDate = expiryDate;
                sw.WriteLine(proj.ProjectId);
                sw.WriteLine(proj.creationDate);
                sw.WriteLine(proj.expiryDate);
                sw.WriteLine(proj.projectName);
                sw.WriteLine(proj.projectURL);

            }
        }
    }
}
