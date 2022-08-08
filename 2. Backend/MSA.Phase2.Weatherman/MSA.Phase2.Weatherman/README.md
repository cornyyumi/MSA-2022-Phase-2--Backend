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

I have setup the launch settings so if you application on Visual Studio 2022 (highly recommennded), you will be able to switch between the `Production` and `Development` environment.
[img]

### Section Two

Middleware 
* Demonstrate an understanding of how these middleware via DI (dependency injection) simplifies your code.

### Section Three

I have used Nunit to unit test my application's service functions.
Using middware libraries such as Nunit make the code much easy to run multiple tests by simplying the code.
For example, by using the []

[Img]

* Demonstrate the use of NUnit to unit test your code.
* Use at least one substitute to test your code.
* Demonstrate an understanding of why the middleware libraries made your code easier to test.
