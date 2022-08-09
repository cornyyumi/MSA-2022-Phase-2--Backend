# MSA-2022-Phase-2

## Phase 2 - Backend

### Weatherman Web API
This API will call the OpenWeatherAPI to get a response of the current weather at a City.\
It will also deserailize the Json response given by the OpenWeatherAPI and  it to save it to "WeatherDatabase.sqlite".\
The user can also call the API to process the weather data and be given a warning, if that are any concerns for the weather.

The `WeathermanController.cs` implements each of the endpoints with the CRUD operations (GET, POST, PUT, DELETE).
![Swagger endpoints](https://user-images.githubusercontent.com/51344267/183555292-f3b9c66c-8bb7-4c7d-a4c9-6c6cff47fdd1.PNG)
**Note: Inputs are Case-Sensitive**: Please capitalize the first letter of the city name (auckland -> **Auckland**)
- `GET /Weatherman/{city}`: Get the Json response of Weather information by entering a city name, Weather information is retrieved from database.
- `GET /Weatherman/temp/{city}`: Get a string of temperature warnings by enterting a city name, Weather information is retrieved from database.
- `GET /Weatherman/weather/{city}`: Get a string of temperature warnings by enterting a city name, Weather information is retrieved from database.

- `POST /Weatherman/add/{city}`: Post new Weather information by entering a city name. The Weatherman API will call the OpenWeatherAPI to find the data, and add it to the database.

- `PUT /Weatherman/update/{city}`: Update Weather information of a city by entering a city name.

   If the Weather information does not exist on the database, the Weatherman API will all the OpenWeatherAPI to find the data, and add it to the database.
   
- `Delete /Weatherman/delete/{city}`: Delete the existing Weather information from database by entering a city name.


### Configuration files
There are two configuration files for 'Production' and 'Development' environments.
Having mutiple confiuration files are useful as it can easily configure the settings depending on the build environment.

When the application is ran in the `Production` environment, it will use the sqlite database 'WeatherDatabase.sqlite' to get, add, update, delete the data.

When the application is ran in the `Development` environment, it will use the in-memory database to to get, add, update, delete the data.\
This is espcially useful for the development stage as it has a faster access speed than a real database, and data stored in in-memory database will be reset each time the the application is started.

I have setup the launch settings so if you application on Visual Studio 2022 (highly recommennded), you will be able to switch between the `Production` and `Development` environment if you follow the screenshot below.
![change environment](https://user-images.githubusercontent.com/51344267/183554816-bb50baa1-5516-47a1-960c-43e8e2a724c4.PNG)

**Note: There is currently a bug where a response isn't given when the user tries to add (POST) the weather detail of a city, and there is already a city with the same Weather id in the database**.\
![image](https://user-images.githubusercontent.com/51344267/183556712-f3d9977c-71ec-4335-9a0c-fe646669ba44.png)\
Weather id of `804` is to indicate that the current weather is "Cloudy".
For example, the user tries to add the weather for "London", and the Weather id of London is `804`. However the user will not get a response back as the weather id of "
" is already in the database, with the same weather id of `804`.

This error is due to the error in my database not allowing duplicate sets of the same `Weather` data with the same `id` (As it is the primary key). This is definitely fixable, however due to the lack of time since submission date of Phase 2 is close, I will leave it untouched.
If you encounter this error, **please enter a different city name to test the code.**

### Middleware via Dependency Injection

Depedency Injection simplfies the code by passing the created objects/middleware into the application.
The registration of the services happen in the `Program.cs`, then middleware services can be injected by depedency injection through using `builder.Services`.
Example of configuring the middleware pipeline and HTTPClient in `Program.cs`:
[IMG]

### Testing/Unit Testing (NUnit/Swagger)

I demonstrated the use of NUnit for my application's unit testing, you will be able see the tests pass succesfully when it runs.
Using NUnit for unit testing was espcially useful for making multiple test cases for my functions, as it helped me to reduce the code duplication.

![NUnit testing](https://user-images.githubusercontent.com/51344267/183555042-32f8d46d-2a0e-4e5e-990a-49426eeb9bee.PNG)


As seen in the image, I was able to use the [TestCases] annotation to assign multiple variables to substitute for testing, instead of having  to change the test variables and assert repeatedly.
This helps the code to be much cleaner, and readable for testing.

Aside from using the NUnit library for Unit testing, the Swagger (NSwag) made the code easier to test my endpoints.
Configuring my application with the  OpenAPI specification generator and Swagger UI middleware allows API documentation and enables the user to call the API directly onto the localhost browser.
So I could easily test if my endpoints for the CRUD operations were receiving the correct response from the OpenWeatherAPI, and check if my API is inputting the correct data into the SQLite database.

Demonstration of how Swagger allows the user to directly see the API documentation and test the inputs to view the response:

Searching for the current weather at "Auckland":
![Swagger add input](https://user-images.githubusercontent.com/51344267/183555287-15db9e41-6222-4a19-a795-a90f721f6d36.PNG)
Successful JSON response of Auckland Weather details:
![Swagger add output](https://user-images.githubusercontent.com/51344267/183555413-a91c0848-3656-4ef7-b808-4d690570e0c4.PNG)
