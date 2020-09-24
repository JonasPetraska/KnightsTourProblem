# Knights tour problem
Artificial intelligence course Knights tour problem program. 

Vilnius University, 2020

# Algorithm
Algorithm based on recursion and depth first search.

# Limitations
Length of board is limited to `[1, 10]`, however practical limit depends on heap size, since recursion is heavily limited by it.

# Logging
Algorithm is enhanced with advanced logging and tracing in every step.

Currently logs `Part 1` and `Part 3` to console and two files `out-short.txt` and `out-long.txt`

Part 2, detailed Trace, is stores only in "out-long.txt" since the file can get big quickly.

Example: When size of board is `7` and starting position is `(1,1)`, then generated file is about 4 gigabytes.

# Input
Program requires 3 variables to start: `length of board`, `starting X and Y positions`. 

Everything is displayed in a 1-based coordinate system.

Current method of providing variables is a console, however it can be extended to any method, since the class `KnightsTourProblem` can be exported separately.

# Tests
Taken from https://klevas.mif.vu.lt/~cyras/AI/ai-cyras.pdf page 16.

These tests can be used to check validity of the algorithm results.

Seven tests: 

1)N=5, X=1, Y=1. Trials=70624.

2)N=5, X=5, Y=1. Trials=236.

3)N=5, X=1, Y=5. Trials= 111981.

4)N=5,X=2, Y=1. No tour. Trials=14635368.

5)N=6,X=1,Y=1. Trials=1985212. 

6)N=7, X=1, Y=1. Trials=57209233.

7)N=4,X=1, Y=1. No tour. Trials=17784.

Print each test to two files as follows:

a) PART 1 and PART 3 – to `screen` and file `out-short.txt`;

b) all three parts – to file `out-long.txt`.
