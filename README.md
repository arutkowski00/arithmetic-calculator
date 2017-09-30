# arithmetic-calculator
[![Build Status](https://travis-ci.com/arutkowski00/arithmetic-calculator.svg?token=LPWwjrTctXcG7aKHsrMK&branch=master)](https://travis-ci.com/arutkowski00/arithmetic-calculator)

## Features
- interactively solve arithmetic problems using simple infix notation
- use popular mathematic functions (sin,  log,  sqrt etc.)
- history of equations (accessible by pressing the Up Arrow, just like in any other shell) - Windows only
- Unit Tested™

## How it works
1. Application prompts the user to enter the arithmetic problem to solve in infix notation, e.g. `2 + 2 * 2` or `sin(1/2) + (sqrt(4) * 2^6)`
2. Calculator will parse the _query_, convert it to postfix notation, calculate it and display a result or an error if there was something wrong with the equation.
3. You can exit the app anytime by pressing `Ctrl-C` or by typing `exit`.
