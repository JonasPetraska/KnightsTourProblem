# Knights tour problem
Artificial intelligence course Knights tour problem program. 
Vilnius University, 2020

# Algorithm
Algorithm based on recursion and depth first search.

# Limitations
Length of board is limited to `[1, 10]`, however practical limit depends on heap size, since recursion is heavily limited by it.

# Logging
Algorithm is enhanced with advanced logging and tracing in every step.

Currently logs `Part 1` and `Part 3` to console and two files "out-short.txt" and "out-long.txt"

Part 2, detailed Trace, is stores only in "out-long.txt" since the file can get big quickly.

Example: When size of board is `7` and starting position is `(1,1)`, then generated file is about 4 gigabytes.

# Input
Program requires 3 variables to start: `length of board`, `starting X and Y positions`. 

Everything is displayed in a 1-based coordinate system.

Current method of providing variables is a console, however it can be extended to any method, since the class `KnightsTourProblem` can be exported separately.
