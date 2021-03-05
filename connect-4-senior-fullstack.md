# Connect Four - Find the winner

## Introduction

Welcome to the coding challenge for **phios ag**, we are glad you have decided to give it a try.

The goal of this challenge is to show your coding, organizational and problem-solving skills. Specifically, we will be taking a look at:

- your usage of **git** (workflow, branches, commits, messages, consistency)
- your **coding style** (clean code, comments,.. )
- your **problem-solving** approach (requirements, questions, tests)
- your **documentation** (README.md, other documentation)
- your **algorithm** (efficiency, clarity)

The problem is easy to solve on purpose, so that you can focus on showing all the points that we mentioned above. The desing of the web page will NOT be considered, do not spend time trying to make it look nice, we are used to deal with raw HTML..

## Problem

The problem to be solved in this challenge is to deliver a working [REST Web Service](https://en.wikipedia.org/wiki/Representational_state_transfer) that allows any User to check whether a specific position in the game of "Connect Four" is a winning position or not. If you do not know this game, please check [Wikipedia](https://en.wikipedia.org/wiki/Connect_Four). In order to communicate with this web service, provide a web page where the user can input a valid string and receive the answer from the server.

Please note that the algorithm should be scalable to other board sizes. Think how good your algorithm will react for a board with 100x100 positions instead of 7x6.

## Glossary

- "piece": this is a chip in the game of Connect Four
- "board": this is the collection of all possible positions (empty or filled)
- "position": each of the 42 spaces (i.e. holes) in the board
- "chain": a collection of pieces one after the other (horizontal, vertical, diagonal)
- "winning chain": a chain of exactly four pieces from the same Team

## Languages

The choice of framework/tools is completely up to you. Remember to document your choices!

## Expected Effort

The coding exercise is free form, this means that you are invited to take as much time as required. However, we estimate the total time required should not be bigger than 5-8h in most cases.

## Expected Documentation

The minimum documentation required is how to run the code in a different machine. Any other documentation (algorithm, testing, etc.. ) is intentionally left to you to decide.

## Preconditions

For every input (i.e. board position), there will be either 0 or 1 winner. The two teams playing the game will be called "Team A" and "Team B". We assume that "Team A" always starts playing. This means that in the board there will always be 0 or 1 more pieces from the "Team A" than pieces from the "Team B".

You should check that the string provided as input is a board position that can be encountered in a real game of Connect Four:

- Only positions that are "physically" possible are to be considered. For example: a position where a piece is "floating" on the board without other pieces underneath is not possible due to gravity.
- The number of pieces for "Team A" is either the same or exactly one more piece than the number of pieces for "Team B". For example after the third move, "Team A" has 2 pieces on the board and "Team B" only has one.
- There is *at most* one winner (no scenarios where both teams won should be considered)
- The positions in the board (i.e. number of holes in the board where the pieces are located) are always exactly 42 (which is 7 columns and 6 rows)
- Winning chains must share at least one piece. This means that there cannot be two winning chains of 4 pieces that do not share any piece since this is not possible in a real game of "Connect Four"

## Input/Output

The expected input/output is HttpRequest/HttpResponse (REST). This means the working web service should be testable with tools like [Postman](https://www.postman.com/), [VsCode REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) and similar ones. Of course, the service should also be tested from the webpage itself.

## Input

The input is a single string with 42 characters. For example the string "AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX".

There are 42 characters because the board of Connect Four has 42 positions (which is 7 columns and 6 rows).

The characters in this string can be one of the following:

- "A" = This means this position in the board belongs to a piece that the Team A has played
- "B" = This means this position in the board belongs to a piece that the Team B has played
- "X" = This means this position in the board is empty

As explained, one example of REST API call would be:

- **GET /api/connect-four/AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX**

However, which REST method to use is intentionally left undefined, so that you can explain which one was used and why.

### Unrolling the input

The input is a matrix (7x6) unrolled into a vector (42). We first copy the characters of the first column, then the second, then the third and so on...

#### Example 1

Input: "AAXXXXBBXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"

This represents the following board:

    ---------
    |       |
    |       |
    |       |
    |       |
    |AB     |
    |AB     |
    ---------

#### Example 2

Input: "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"

This represents the following board:

    ---------
    |       |
    |       |
    |       |
    |       |
    |       |
    |       |
    ---------

### Example 3

Input: "AXXXXXAXXXXXAXXXXXAXXXXXBBBXXXXXXXXXXXXXXX"

This represents the following board:

    ---------
    |       |
    |       |
    |       |
    |    B  |
    |    B  |
    |AAAAB  |
    ---------

## Goal

The goal of the program is to identify the winner (if there is one). Note that we explicitly assume that the board position is a "real" one that can happen in a game of "Connect Four" and therefore it is not necessary to consider the case where the two teams have won at the same time, because this never happens in the real game.

## Output

The output of the web service is one string with one single character. The character can be one of the following:

- A = Team A has won
- B = Team B has won
- X = The game is ongoing (i.e. there is no winner yet)

Example of scenarios with at least one winning chain

    ---------
    |       |
    |       |
    |       |
    |    B  |
    |    B  |
    |AAAAB  |
    ---------
    Expected output = "A" 
    Team A has won horizontally


    ---------
    |       |
    |       |
    |    B  |
    |    B  |
    |    B  |
    |AAA B A|
    ---------
    Expected output = "B"
    Team B has won vertically


    ---------
    |       |
    |       |
    |A      |
    |BA  B  |
    |BBA B  |
    |AAAAB  |
    ---------
    Expected output = "A"
    Team A has won diagonally


    ---------
    |       |
    |       |
    |       |
    |       |
    |BBB BBB|
    |AAAAAAA|
    ---------
    Expected output = "A"
    Team A has won horizontally, note that in this case there is a row of 7 "Team A" pieces, this is possible in a real game!

## Version Control

You will be given access to a git repository where this information is contained. This repository is to be used during the development and at the end. Please do not forget the README.md and be sure you explain how to run the code!

Once completed, please let us know so that we can check the code.

## Testing

The testing of the solution is left to you. However, some testing strings are also provided in the repository. Please note that the solution is quite simple but not trivial! The program should provide the right answer in an efficient manner!

## Final Remarks

We hope that you have fun solving this coding challenge. Please let us know if there is anything unclear in this document, so that we can improve it in the future.
