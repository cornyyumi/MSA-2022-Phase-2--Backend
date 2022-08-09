# MSA-2022-Phase-2

## Phase 2 - Backend

### Weatherman Web API
This API will call the OpenWeatherAPI to get a response of the current weather at a City.\
It will also deserailize the Json response given by the OpenWeatherAPI and  it to save it to "WeatherDatabase.sqlite".\
The user can also call the API to process the weather data and be given a warning, if that are any concerns for the weather.

The `WeathermanController.cs` implements the endpoints with the 4 CRUD operations for the API(GET, POST, PUT, DELETE)

- `GET`: Get the Json response of Weather information by entering a city name, from the database.

- `Post`: Post new Weather information by entering a city name. The Weatherman API will call the OpenWeatherAPI to find the data, and add it to the database.

- `Put`: Update Weather information of a city by entering a city name.

   If the Weather information does not exist on the database, the Weatherman API will all the OpenWeatherAPI to find the data, and add it to the database.
   
- `Delete`: Delete the existing Weather information from database by entering a city name.

There are two configuration files for 'Production' and 'Development' environments.
Having mutiple confiuration files are useful as it can easily configure the settings depending on the build environment.

When the application is ran in the `Production` environment, it will use the sqlite database 'WeatherDatabase.sqlite' to get, add, update, delete the data.

When the application is ran in the `Development` environment, it will use the in-memory database to to get, add, update, delete the data.\
This is espcially useful for the development stage as it has a faster access speed than a real database, and data stored in in-memory database will be reset each time the the application is started.

I have setup the launch settings so if you application on Visual Studio 2022 (highly recommennded), you will be able to switch between the `Production` and `Development` environment if you follow the screenshot below.
[img]

### Section Two

Middleware 
* Demonstrate an understanding of how these middleware via DI (dependency injection) simplifies your code.

### Section Three

I demonstrated the use of NUnit for my applications unit testing, you will be able to run these tests and see them pass the test cases.
Using NUnit for unit testing was espcially useful for making multiple test cases for my functions, as it helped me to reduce the code duplication.
[IMG]
As seen in the image, I was able to use the [TestCases] annotation to assign multiple variables to substitute in for testing, instead of having repeated code.
This helps the code to be much cleaner, and readable for testing.

Aside from using the NUnit library for testing, the Swagger (NSwag) made the code easier to test.
Configuring my application with the  OpenAPI specification generator and Swagger UI middleware allows API documentation and enables the user to call the API directly onto the localhost browser. So I could easily test if my endpoints for the CRUD operations were receiving the correct response from the OpenWeatherAPI, and check if my API is inputting the correct data into the SQLite database.
emonstrate an understanding of why the middleware libraries made your code easier to test.
