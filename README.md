# FourConnectWinner
Find the winner of four-connect game making a complete development including a web and REST service to get the solution.
# Connect Four - Find the winner - Code Challenge

The objective of this project is find the winner of four-connect game making a complete development including a web and REST service to get the solution.
The algorith visit every hole of the board and check if this position and the next three form a chain. Check Horizontally, Vertically, Diagonally Right Up, Diagonly Left Up

    ^^    ^
    | \  /      
    |  \/  
    |  /\   
    | /  \  
    |/    \  
    X------>

If there is more than one solution only get the first and return to improve the time. 
In the worst case (there is no winner or game is ongoing) the algorithm visit every position of the board, so the complexity is quadratic O(NxM) being NxM de size of the board.
The algorithm requires the size of the board and input to solve the problem.


## Project

The solution was created with DDD Microservice layers
* Domain entity model 
* Service with the logic bussiness
* Web.API REST

There is not Infrastructure project because it was not need database.
Logger with Serilog extension with sink in file
Validator implemented with composite pattern

### Installation

Install API REST in IIS or launch IIS Express with Visual Studio

Web in folder ConnectFourWinner\ConnectFourWeb
* Configure URL of service in main.js 
* Open index.html in web browser. 
```
 var serviceURL = "http://localhost:1429/api/connect-four"; 
```

## Exemples to test

Examples 7x6:
OK:
```
XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
AAXXXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
AAABXABBBXXAABXXXXBXXXXXXXXXXXXXXXXXXXXXXX
AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX
AXXXXXAAXXXXAXXXXXXXXXXXBBBBXXXXXXXXXXXXXX
ABBAXXABAXXXAAXXXXAXXXXXBBBXXXXXXXXXXXXXXX
XXXXXXBABAXXBBAXXXAAXXXXAXXXXXBXXXXXXXXXXX
ABXXXXABXXXXABXXXXAXXXXXABXXXXABXXXXABXXXX
ABABABBABABABABABABABABAABABABABABABABABAB
AAXXXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```
Error length:
```
AAXXXXBBXXXXXXXXXXXXXXXXXXXX
```

Error symbols number:
```
AAAAXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```

Error symbol pieces:
```
AAXXXXBCXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```

Error position:
```
ABBBXBXAABXXXXAAXXXXXAXXXXXXXXXXXXXXXXXXXX
```

Examples 5x4:
```
AXXXAXXXAXXXAXXXBBBX
```
Examples 9x7:
```
AXXXXXXAXXXXXXAXXXXXXAXXXXXXBBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```

## Unit Testing

Test solution with a total coverage of 95%.

Service Test to test the algorithm

Validators tests to validations classes for input

## Tests Execution 

Unit Tests executed by Visual Studio

Added YAML (.gitlab-ci.yml) file for CI/CD to build and run test in gitlab (Not tested) 

Added Json file (ConnectFourWinner.Api.postman_collection.json) to test API with postman

## Tools

* [Visual Studio 2019] : API REST .Net (Core) 5
* [Sublime Tex]: Web HTML 5 + JQuery 
* [Postman]: API tests 
* [Google Chrome]: Web tests

## References

* https://docs.microsoft.com/es-es/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice
* https://jquery.com/
* https://en.wikipedia.org/wiki/Connect_Four


