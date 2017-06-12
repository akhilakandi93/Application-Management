* Implemented REST Web Service that handles http GET/POST requests on port 4825.

* <b>Controllers:</b></br>
  •	createProjectController</br>
  •	RequestProjectController</br>
  
<b>CreateProjectController:</b></br>
	 •Post([FromBody]Project proj):</br>
	   - Creates a new project and saves it to project.txt file.</br>
      
<b>RequestProjectController:</b></br>
	 •Get(int id):</br>
	  - Returns the project with project id=id </br>
	 •Get(string country, int number, string keyword):</br>
	   - Returns the project with satisfies the country, number, keyword </br>
   •Get(String country)</br>
	   - Returns the project with satisfies the country</br>
  •Get(int id, String country, int number)</br>
	   - Returns the project with the given id, that has the country and the keyword </br>
   •Get(String country, int number)</br>
     - Returns the project that has the country and number greater than or equal to the given number. </br>
	 •Post([FromBody]Project proj)</br>
	  - Adds a valid project into the .json file.</br>

	Implemented and developed the REST Web service on Visual Studio 2015 using ASP.NET Web API 2 on port 4825.</br>

	Post(Project proj)- </br>
       posts a new project into project.txt file and prints “Campaign is successfully created” in the Console.</br>
       
	All the project are first checked if they are enabled,  if the project URL is not null and if the expiry date is valid.</br>

	http://localhost:4825/api/RequestProject : </br>
         Returns the project that has the maximum project cost.</br>
         
	http://localhost:4825/api/RequestProject/1 : </br>
         Returns the project with id=1 if exists else displays  message saying “No Such valid Project Found”. </br>
         
	http://localhost:4825/api/RequestProject?id=5&country=india&number=5: </br>
         Returns  a project with id=5, country=india and number>=5 ; if country and number doesn’t satisfy, then returns a project with id = 5 if exists else displays a message=["No such project where id=5, country=india and number=5"]. </br>
         
	http://localhost:4825/api/RequestProject?country=brazil: </br>
         Returns the project which has country “BRAZIL” in its list of countries and that has highest cost out of all satisfying the conditions.</br>
         
	http://localhost:4825/api/RequestProject?country=brazil&number=10: </br>
         Returns the project that has country “BRAZIL” in its list of countries, number>=10 and that has highest cost out of all satisfying the conditions.</br>
         
	http://localhost:4825/api/RequestProject?country=india&number=5&keyword=music: </br>
         Returns the project that has country “INDIA” , number>=5 and keyword =music. If no such project exists, then displays a message ["No such project where country=india , keyword=music and number=5 is found"].</br>
         
