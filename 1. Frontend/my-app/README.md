# Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `npm install`: Install the requirements to run the web application.

### `npm start`: Run the web application in development mode.

Open [http://localhost:3000](http://localhost:3000) to view it in the browser.


## About the web application

This is a simple web application is created by using typescript and react.\
It connects to the OpenLibrary API: https://openlibrary.org/ and allows the user to search for a bookk using its ISBN 13 number.\
UI library: Blueprint UI
<hr>
When the user inputs the ISBN 13 of a book, they can view its title, cover and the description.

Here are some values you could try:

Fantastic Mr Fox: 9780141349978\
The BFG: 9782070513727\
The power of Positive thinking: 9781936594221\
Papá rico, papá pobre: 9780446679954\
It Ends With Us: 9781501110368
![image](https://user-images.githubusercontent.com/51344267/183573251-d8e4838a-2866-466e-840c-ae80a84a6ce1.png)
<hr>
When an invalid input (eg-Empty string) or if the book cannot be found, it will display "Book not found".

![image](https://user-images.githubusercontent.com/51344267/183573693-52c13e19-419c-4cbf-84a6-24f0a76ce771.png)
